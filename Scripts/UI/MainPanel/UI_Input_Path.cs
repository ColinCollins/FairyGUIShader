/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

public partial class UI_Input_Path : GComponent
{
    public GTextInput m_txt_path;
    public const string URL = "ui://hzhrtgcyso0f4a";

    public static UI_Input_Path CreateInstance()
    {
        return (UI_Input_Path)UIPackage.CreateObject("MainPanel", "Input_Path");
    }

    public override void ConstructFromXML(XML xml)
    {
        base.ConstructFromXML(xml);

        m_txt_path = (GTextInput)GetChildAt(1) as GTextInput;
    }
}