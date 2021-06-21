/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

public partial class UI_Comp_View : GComponent
{
    public GLoader m_loader;
    public const string URL = "ui://hzhrtgcypv4e7z";

    public static UI_Comp_View CreateInstance()
    {
        return (UI_Comp_View)UIPackage.CreateObject("MainPanel", "Comp_View");
    }

    public override void ConstructFromXML(XML xml)
    {
        base.ConstructFromXML(xml);

        m_loader = (GLoader)GetChildAt(0) as GLoader;
    }
}