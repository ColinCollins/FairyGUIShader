/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

public partial class UI_Comp_Console : GComponent
{
    public GList m_lst_logs;
    public GButton m_btn_clearLog;
    public const string URL = "ui://hzhrtgcykans4s";

    public static UI_Comp_Console CreateInstance()
    {
        return (UI_Comp_Console)UIPackage.CreateObject("MainPanel", "Comp_Console");
    }

    public override void ConstructFromXML(XML xml)
    {
        base.ConstructFromXML(xml);

        m_lst_logs = (GList)GetChildAt(1) as GList;
        m_btn_clearLog = (GButton)GetChildAt(2) as GButton;
    }
}