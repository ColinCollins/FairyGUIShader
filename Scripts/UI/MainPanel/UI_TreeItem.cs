/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

public partial class UI_TreeItem : GButton
{
    public Controller m_expanded;
    public Controller m_leaf;
    public GGraph m_indent;
    public GButton m_expandButton;
    public const string URL = "ui://hzhrtgcykra54k";

    public static UI_TreeItem CreateInstance()
    {
        return (UI_TreeItem)UIPackage.CreateObject("MainPanel", "TreeItem");
    }

    public override void ConstructFromXML(XML xml)
    {
        base.ConstructFromXML(xml);

        m_expanded = GetControllerAt(1);
        m_leaf = GetControllerAt(2);
        m_indent = (GGraph)GetChildAt(2) as GGraph;
        m_expandButton = (GButton)GetChildAt(4) as GButton;
    }
}