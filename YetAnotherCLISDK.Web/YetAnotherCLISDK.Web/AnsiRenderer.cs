using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;

namespace YetAnotherCLISDK.Web;

/// <summary>
/// Converts ANSI-escaped output from IRenderable.GetLines() into safe HTML MarkupStrings.
/// </summary>
public static partial class AnsiRenderer
{
    [GeneratedRegex(@"\x1b\[([0-9;]*)m")]
    private static partial Regex AnsiEscapeRegex();

    /// <summary>
    /// Renders all lines from an <c>IRenderable</c> as an HTML <c>&lt;pre&gt;</c> block,
    /// converting ANSI escape sequences to inline CSS styles.
    /// </summary>
    /// <param name="lines">Lines returned by <c>IRenderable.GetLines()</c>.</param>
    /// <param name="extraCssClass">Optional additional CSS class appended to the <c>cli-output</c> element.</param>
    public static MarkupString Render(IEnumerable<string> lines, string? extraCssClass = null)
    {
        var cls = extraCssClass is { Length: > 0 } ? $" {extraCssClass}" : "";
        var html = new StringBuilder();
        html.Append($"<pre class=\"cli-output{cls}\">");

        bool first = true;
        foreach (var line in lines)
        {
            if (!first) html.Append('\n');
            first = false;
            html.Append(ConvertLineToHtml(line));
        }

        html.Append("</pre>");
        return new MarkupString(html.ToString());
    }

    private static string ConvertLineToHtml(string ansiLine)
    {
        var result = new StringBuilder();
        // Split keeps capturing groups, so even indexes = text, odd indexes = ANSI param string
        var parts = AnsiEscapeRegex().Split(ansiLine);
        var state = AnsiState.Default;

        for (int i = 0; i < parts.Length; i++)
        {
            if (i % 2 == 0)
            {
                var text = WebUtility.HtmlEncode(parts[i]);
                if (text.Length == 0) continue;

                if (state.HasStyle)
                    result.Append($"<span style=\"{state.ToCss()}\">{text}</span>");
                else
                    result.Append(text);
            }
            else
            {
                state = ApplyAnsiCode(state, parts[i]);
            }
        }

        return result.ToString();
    }

    private static AnsiState ApplyAnsiCode(AnsiState current, string codes)
    {
        if (string.IsNullOrEmpty(codes) || codes == "0")
            return AnsiState.Default;

        var nums = codes.Split(';')
                        .Select(s => int.TryParse(s, out var n) ? n : -1)
                        .ToArray();
        var state = current;

        for (int i = 0; i < nums.Length; i++)
        {
            switch (nums[i])
            {
                case 0:  state = AnsiState.Default; break;
                case 1:  state = state with { Bold = true }; break;
                case 2:  state = state with { Dim = true }; break;
                case 3:  state = state with { Italic = true }; break;
                case 4:  state = state with { Underline = true }; break;
                case 5:  state = state with { Blink = true }; break;
                case 7:  state = state with { Inverse = true }; break;
                case 9:  state = state with { Strikethrough = true }; break;
                case 38 when i + 4 < nums.Length && nums[i + 1] == 2:
                    state = state with { Fg = $"rgb({nums[i + 2]},{nums[i + 3]},{nums[i + 4]})" };
                    i += 4;
                    break;
                case 48 when i + 4 < nums.Length && nums[i + 1] == 2:
                    state = state with { Bg = $"rgb({nums[i + 2]},{nums[i + 3]},{nums[i + 4]})" };
                    i += 4;
                    break;
            }
        }
        return state;
    }

    private record AnsiState
    {
        public static readonly AnsiState Default = new();

        public string? Fg { get; init; }
        public string? Bg { get; init; }
        public bool Bold { get; init; }
        public bool Italic { get; init; }
        public bool Underline { get; init; }
        public bool Strikethrough { get; init; }
        public bool Dim { get; init; }
        public bool Blink { get; init; }
        public bool Inverse { get; init; }

        public bool HasStyle =>
            Fg != null || Bg != null || Bold || Italic || Underline ||
            Strikethrough || Dim || Blink || Inverse;

        public string ToCss()
        {
            var sb = new StringBuilder();
            var fg = Fg;
            var bg = Bg;
            if (Inverse) (fg, bg) = (bg ?? "var(--cli-bg)", fg ?? "var(--cli-fg)");

            if (fg != null) sb.Append($"color:{fg};");
            if (bg != null) sb.Append($"background-color:{bg};");
            if (Bold)   sb.Append("font-weight:bold;");
            if (Italic) sb.Append("font-style:italic;");
            if (Dim)    sb.Append("opacity:0.6;");
            if (Blink)  sb.Append("animation:cli-blink 1s step-end infinite;");

            var decorations = new List<string>(2);
            if (Underline)     decorations.Add("underline");
            if (Strikethrough) decorations.Add("line-through");
            if (decorations.Count > 0)
                sb.Append($"text-decoration:{string.Join(' ', decorations)};");

            return sb.ToString();
        }
    }
}
