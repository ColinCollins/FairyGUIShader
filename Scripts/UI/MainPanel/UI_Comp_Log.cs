/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

public partial class UI_Comp_Log : GComponent
{
    public Controller m_c_state;
    public GTextInput m_txt_log;
    public const string URL = "ui://hzhrtgcykans4t";

    public static UI_Comp_Log CreateInstance()
    {
        return (UI_Comp_Log)UIPackage.CreateObject("MainPanel", "Comp_Log");
    }

    public override void ConstructFromXML(XML xml)
    {
        base.ConstructFromXML(xml);

        m_c_state = GetControllerAt(0);
        m_txt_log = (GTextInput)GetChildAt(1) as GTextInput;
    }
}