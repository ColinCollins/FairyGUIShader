/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

public partial class UI_p_Main : GComponent
{
    public Controller m_c_state;
    public UI_Comp_View m_comp_container;
    public UI_Input_Path m_input_path;
    public UI_Comp_LstTree m_lst_tree;
    public GButton m_btn_stop;
    public UI_Comp_Console m_comp_console;
    public GButton m_btn_start;
    public UI_comp_child m_comp_child;
    public UI_comp_state m_comp_state;
    public const string URL = "ui://hzhrtgcytheb0";

    public static UI_p_Main CreateInstance()
    {
        return (UI_p_Main)UIPackage.CreateObject("MainPanel", "p_Main");
    }

    public override void ConstructFromXML(XML xml)
    {
        base.ConstructFromXML(xml);

        m_c_state = GetControllerAt(0);
        m_comp_container = (UI_Comp_View)GetChildAt(1) as UI_Comp_View;
        m_input_path = (UI_Input_Path)GetChildAt(2) as UI_Input_Path;
        m_lst_tree = (UI_Comp_LstTree)GetChildAt(3) as UI_Comp_LstTree;
        m_btn_stop = (GButton)GetChildAt(4) as GButton;
        m_comp_console = (UI_Comp_Console)GetChildAt(5) as UI_Comp_Console;
        m_btn_start = (GButton)GetChildAt(6) as GButton;
        m_comp_child = (UI_comp_child)GetChildAt(7) as UI_comp_child;
        m_comp_state = (UI_comp_state)GetChildAt(8) as UI_comp_state;
    }
}