using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using System.Text;

public class MainCtrl : Singleton<MainCtrl>
{
	// custom 
	public static bool isListener = false;

	// tempory 临时放到这里，其实不该 effect/outline prop 设置
	public OutlineProperties OutlineProp;

	// 初始化 uisystem
	private void Awake()
	{	
		UIConfig.defaultFont = "Microsoft YaHei";
		PkgCtrl.Instance.OnInit();
		UISystem.Instance.OnInit();
	}

	// Start is called before the first frame update
	void Start()
    {
		UISystem.Instance.ShowUI("MainPanel", "p_Main", UILevel.ONE);
    }

	#region 点击事件

	// 给 UI 资源添加点击输出事件
	// GImage GTextField 基础组件不包含 container （GComponent | CreateDisplayObject 函数）因此无法被监听到
	public static void AddClickListener(GObject obj) 
	{
		if (MainCtrl.isListener)
		{
			obj.touchable = true;
			obj.onClick.Set((EventContext context) =>
			{
				if (obj.resourceURL == null)
					return;

				PackageItem item = UIPackage.GetItemByURL(obj.resourceURL);
				StringBuilder path = new StringBuilder(item.name);

				path.Insert(0, "/");
				path.Insert(0, item.owner.assetPath.Replace(PkgCtrl.RelaPath, ""));

				LogCtrl.Warning(string.Format("{0}/{1}", path.ToString(), obj.name));
				context.StopPropagation();
			});
		}
	}

	#endregion
}
