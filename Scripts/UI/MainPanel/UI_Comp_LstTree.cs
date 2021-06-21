/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

public partial class UI_Comp_LstTree : GComponent
{
    public GTree m_lst_tree;
    public const string URL = "ui://hzhrtgcyopu14h";

    public static UI_Comp_LstTree CreateInstance()
    {
        return (UI_Comp_LstTree)UIPackage.CreateObject("MainPanel", "Comp_LstTree");
    }

    public override void ConstructFromXML(XML xml)
    {
        base.ConstructFromXML(xml);

        m_lst_tree = (GTree)GetChildAt(1) as GTree;
    }
}