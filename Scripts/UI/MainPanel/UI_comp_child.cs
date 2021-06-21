/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

public partial class UI_comp_child : GComponent
{
    public GList m_lst_selector;
    public GTextField m_title;
    public const string URL = "ui://hzhrtgcyuzrh81";

    public static UI_comp_child CreateInstance()
    {
        return (UI_comp_child)UIPackage.CreateObject("MainPanel", "comp_child");
    }

    public override void ConstructFromXML(XML xml)
    {
        base.ConstructFromXML(xml);

        m_lst_selector = (GList)GetChildAt(2) as GList;
        m_title = (GTextField)GetChildAt(3) as GTextField;
    }
}