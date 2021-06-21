using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using System.IO;
using System.Text;

// 主界面 控制器
public class MainPanel
{
	// 主界面 UI 对象
	protected UI_p_Main view;
	public GObject View { get { return view; } }

	const string pathKey = "Path Data";

	private List<Controller> compCtrls;
	private GObject[] children;
	private GComponent curComp;

	// 初始化
	public void InitView()
	{
		view = UI_p_Main.CreateInstance();

		LogCtrl.Init(this);

		// button 
		view.m_btn_start.onClick.Add(SearchPackage);
		view.m_btn_stop.onClick.Add(ClearPackage);
		view.m_comp_console.m_btn_clearLog.onClick.Add(ClearLog);

		// input 
		view.m_input_path.m_txt_path.text = getPathData();
		view.m_input_path.m_txt_path.onChanged.Add(SavePathSetting);

		// tree
		view.m_lst_tree.m_lst_tree.treeNodeRender = renderTreeNode;
		view.m_lst_tree.m_lst_tree.onClickItem.Add(onItemClick);

		// controller
		compCtrls = new List<Controller>();
		view.m_comp_state.m_lst_selector.treeNodeRender = renderCtrlNode;
		view.m_comp_state.m_lst_selector.onClickItem.Add(switchState);

		// child
		view.m_comp_child.m_lst_selector.itemRenderer = renderChildren;
		view.m_comp_child.m_lst_selector.onClickItem.Add(onClickChild);

		OutlineCtrl ol = new OutlineCtrl();
		ol.target = view.m_btn_start.displayObject;
	}

	#region  控制台输出 Log
	public void ClearLog()
	{
		view.m_comp_console.m_lst_logs.RemoveChildrenToPool();
	}

	
	public void AddLog(string msg, LogType type)
	{
		GList lst = view.m_comp_console.m_lst_logs;
		UI_Comp_Log log = lst.GetFromPool(UI_Comp_Log.URL) as UI_Comp_Log;

		log.m_txt_log.text = msg;
		log.m_c_state.SetSelectedIndex((int)type);
		lst.AddChildAt(log, 0);
	}

	#endregion

	#region 检索按钮

	public void SearchPackage()
	{
		string path = view.m_input_path.m_txt_path.text;
		PkgCtrl.Instance.LoadPackageRes(path);
		view.m_c_state.SetSelectedIndex(1);

		initLstTree();
		initSelectorTree();
		initChildrenList();
	}

	public void ClearPackage()
	{
		view.m_c_state.SetSelectedIndex(0);
		PkgCtrl.Instance.ClearAll();
		clearContainer();
	}

	#endregion

	// init, renderNode, itemclick
	#region 控制包相关 API

	private void initLstTree() 
	{
		GTree tree = view.m_lst_tree.m_lst_tree;
		tree.RemoveChildrenToPool();

		GTreeNode rootNode = tree.rootNode;
		List<UIPackage> pkgs = UIPackage.GetPackages();
		pkgs.ForEach(pkg => 
		{
			if (pkg.name.Equals("MainPanel"))
				return;

			GTreeNode node = new GTreeNode(true);
			rootNode.AddChild(node);
			node.text = pkg.name;

			// 生成次级对象
			List<PackageItem> items = pkg.GetItems();
			foreach (PackageItem pi in items)
			{
				if (pi.type == PackageItemType.Component && pi.exported)
				{
					GTreeNode comp = new GTreeNode(false);
					TreeData data = new TreeData();
					data.PackageName = pkg.name;
					data.CompName = pi.name;
					comp.data = data;
					node.AddChild(comp);
				}
			}
		});
	}

	private void renderTreeNode(GTreeNode node, GComponent obj) 
	{
		if (obj == null)
			return;

		UI_TreeItem item = obj as UI_TreeItem;
		item.m_expanded.SetSelectedIndex(0);
		item.node = node;

		if (node.data != null) 
		{
			TreeData data = (node.data as TreeData);
			item.path = data.Path;
			item.title = data.CompName;
		}

		string iconName = !node.isFolder ? "button_round_line_theme0_5" : "folder_closed";
		item.icon = $"ui://MainPanel/{iconName}"; 
	}

	// tree node 被点击时触发
	private void onItemClick(EventContext ent) 
	{
		UI_TreeItem node = ent.data as UI_TreeItem;
		clearContainer();

		if (node.node.isFolder)
			return;

		MainCtrl.isListener = true;

		// 加载对应 component
		TreeData data = node.node.data as TreeData;
		GLoader loader = view.m_comp_container.m_loader;
		loader.icon = data.Path;

		MainCtrl.isListener = false;

		// ------------- temporary
		if (loader.Content2 == null)
			return;

		curComp = loader.Content2;
		compCtrls = curComp.Controllers;
		children = curComp.GetChildren();

		initSelectorTree();
		initChildrenList();
	}

	// 清空展示容器
	private void clearContainer() 
	{
		view.m_comp_container.m_loader.icon = "";
		clearController();
	}

	private void clearController() 
	{
		curComp = null;
		compCtrls.Clear();
		view.m_comp_state.m_lst_selector.RemoveChildrenToPool();
	}

	#endregion

	#region 控制器列表

	private void initSelectorTree() 
	{
		GTree tree = view.m_comp_state.m_lst_selector;
		tree.RemoveChildrenToPool();

		GTreeNode rootNode = tree.rootNode;

		if (compCtrls == null || compCtrls.Count <= 0)
			return;

		compCtrls.ForEach(ctrl =>
		{
			GTreeNode node = new GTreeNode(true);
			rootNode.AddChild(node);
			node.text = ctrl.name;

			// 生成次级对象
			for (int i = 0; i < ctrl.pageCount; i++) 
			{
				string id = ctrl.GetPageId(i);
				GTreeNode sub = new GTreeNode(false);
				ControllerData data = new ControllerData();
				data.index = i;
				data.Ctrl = ctrl;
				sub.data = data;
				sub.text = id;
				node.AddChild(sub);
			}
		});
	}

	private void renderCtrlNode(GTreeNode node, GComponent obj)
	{
		if (obj == null)
			return;

		UI_TreeItem item = obj as UI_TreeItem;
		item.m_expanded.SetSelectedIndex(0);
		item.node = node;

		if (node.data != null)
		{
			ControllerData data = (node.data as ControllerData);
			item.title = data.GetPageName();
		}

		// ------------------------- switch icon
		//string iconName = !node.isFolder ? "button_round_line_theme0_5" : "folder_closed";
		//item.icon = $"ui://MainPanel/{iconName}";
	}


	private void switchState(EventContext ent) 
	{
		UI_TreeItem node = ent.data as UI_TreeItem;
		ControllerData data = node.node.data as ControllerData;

		if (node.node.isFolder)
			return;

		data.SwitchState();
	}

	#endregion

	#region 控制子节点对象列表

	private void initChildrenList() 
	{
		view.m_comp_child.m_lst_selector.numItems = children == null ? 0 : children.Length;
	}

	private void renderChildren(int index, GObject obj) 
	{
		GButton btn = obj as GButton;
		btn.title = children[index].name;
	}

	private void onClickChild(EventContext ent) 
	{
		// log child package name data
		GButton btn = ent.data as GButton;

		PackageItem item = UIPackage.GetItemByURL(curComp.resourceURL);
		StringBuilder path = new StringBuilder(item.name);

		path.Insert(0, "/");
		path.Insert(0, item.owner.assetPath.Replace(PkgCtrl.RelaPath, ""));

		LogCtrl.Warning(string.Format("{0}/{1}", path.ToString(), btn.title));
	}
	
	#endregion

	#region PlayerPrabs Model 存储模块，保存相对路径信息

	private string getPathData() 
	{
		return PlayerPrefs.GetString(pathKey);
	}

	public void SavePathSetting() 
	{
		string path = view.m_input_path.m_txt_path.text;
		PlayerPrefs.SetString(pathKey, path);
	}

	#endregion 
}


// Tree node data 持有
public class TreeData 
{
	public string PackageName;
	public string CompName;

	public string Path
	{
		get 
		{
			return string.Format("ui://{0}/{1}", PackageName, CompName);
		}
	}
}

public class ControllerData 
{
	public int index;
	public Controller Ctrl;

	public void SwitchState() 
	{
		Ctrl.SetSelectedIndex(index);
	}

	public string GetPageName() 
	{
		return string.Format("{0}_{1}", index, Ctrl.GetPageName(index));
	}
}