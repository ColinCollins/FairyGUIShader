using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UILevel
{
    // main panel
    ZERO = 0, 
    // preview 
    ONE,
    // alert
    TWO,
    // count
    END
}

public class UISystem : Singleton<UISystem>
{
    List<GComponent> levels = new List<GComponent>();

    // ---------tempoary 

    public MainPanel main;

    public void OnInit()
    {
        Application.targetFrameRate = 60;

        for (int i = 0; i < (int)UILevel.END; i++) 
        {
			GComponent comp = new GComponent();
            comp.gameObjectName = $"level{i}";
            // comp.sortingOrder = i;
            GRoot.inst.AddChild(comp);
            levels.Add(comp);
        }

        MainPanelBinder.BindAll();
    }

    // 创建 UI，-------------- unfinished
    public UIPanel ShowUI(string packageName, string compName, UILevel level) 
    {
        main = new MainPanel();
        main.InitView();
        levels[0].AddChild(main.View);

        main.View.visible = true;

        return null;
	}
}
