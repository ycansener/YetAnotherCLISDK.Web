namespace YetAnotherCLISDK.Web;

/// <summary>A node in a <c>CliTreeView</c> component.</summary>
public class CliTreeNode
{
    /// <summary>The display label for this node.</summary>
    public string Label { get; set; } = "";
    /// <summary>Child nodes nested beneath this node.</summary>
    public List<CliTreeNode> Children { get; set; } = [];
}

/// <summary>A bar entry for a <c>CliBarChart</c> component.</summary>
public class CliBar
{
    /// <summary>The label displayed next to the bar.</summary>
    public string Label { get; set; } = "";
    /// <summary>The numeric value that determines the bar length.</summary>
    public double Value { get; set; }
    /// <summary>Optional color override for this bar.</summary>
    public YetAnotherCLISDK.Core.Color? Color { get; set; }
}

/// <summary>An option in a <c>CliSelectionMenu</c> or <c>CliMultiSelectMenu</c> component.</summary>
public class CliMenuOption
{
    /// <summary>The text displayed for this option.</summary>
    public string Label { get; set; } = "";
    /// <summary>Optional CSS color string (e.g. <c>"rgb(78,201,176)"</c> or <c>"#4ec9b0"</c>).</summary>
    public string? CssColor { get; set; }
}
