using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

// Package 读取控制器
public class PkgCtrl : Singleton<PkgCtrl>
{
	public static string RelaPath = "";
	public List<string> packages = null;
	// load default panel
	public void OnInit()
	{
		LoadPackagePre();
	}

	public void LoadPackagePre() 
	{
		UIPackage.AddPackage("UI/MainPanel");
	}

	public void LoadPackageRes(string path) 
	{
		TextAsset[] files = Resources.LoadAll<TextAsset>(path);
		packages = new List<string>();
		if (files.Length <= 0)
		{
			LogCtrl.Warning("Resources empty");
			return;
		}

		for (int i = 0; i < files.Length; i++)
		{
			if (!files[i].name.Contains("_fui"))
				continue;

			string name = files[i].name.Replace("_fui", "");
			if (name.Equals("MainPanel"))
				continue;

			string pkgPath = $"{path}/{name}";
			UIPackage.AddPackage(pkgPath);
			packages.Add(pkgPath);
		}

		LogCtrl.Success($"Load Success: {UIPackage.GetPackages().Count}");

		// save relative path
		RelaPath = path;
	}

	// 清理所有加载包
	public void ClearAll() 
	{
		// UIPackage.RemoveAllPackages();

		packages.ForEach(path => 
		{
			UIPackage.RemovePackage(path);
		});
	}
}
