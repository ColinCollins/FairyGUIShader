/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

public partial class UI_comp_state : GComponent
{
    public GTree m_lst_selector;
    public GTextField m_title;
    public const string URL = "ui://hzhrtgcyuzrh80";

    public static UI_comp_state CreateInstance()
    {
        return (UI_comp_state)UIPackage.CreateObject("MainPanel", "comp_state");
    }

    public override void ConstructFromXML(XML xml)
    {
        base.ConstructFromXML(xml);

        m_lst_selector = (GTree)GetChildAt(2) as GTree;
        m_title = (GTextField)GetChildAt(3) as GTextField;
    }
}