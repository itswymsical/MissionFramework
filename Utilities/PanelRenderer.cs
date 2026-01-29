using Terraria.GameContent;
using Terraria.UI.Chat;

namespace MissionFramework.Utilities;
/// <summary>
/// renderer for UI panels using a 9-slice technique.
/// </summary>
internal static class PanelRenderer
{

    // Updated DrawPanel method in DrawUtils.cs

    // OPTION 1: 9-slice approach (use this if your texture has borders that shouldn't stretch)
    public static void DrawPanel(SpriteBatch sb, int x, int y, int w, int h, Color c = default, Texture texture = null)
    {
        if (c == default)
            c = new Color(63, 65, 151, 255) * 0.785f;

        var value = ModContent.Request<Texture2D>($"{UI_ASSET_DIRECTORY}Dialogue/PortraitBox").Value;
        var value2 = ModContent.Request<Texture2D>($"{UI_ASSET_DIRECTORY}Dialogue/PortraitBox_Inner").Value;

        texture = texture ?? value;

        // ADJUST THESE VALUES to match your sprite's actual border sizes!
        int borderLeft = 20;   // Measure the left border in your 480x106 sprite
        int borderRight = 20;  // Measure the right border in your 480x106 sprite  
        int borderTop = 30;    // Measure the top border in your 480x106 sprite
        int borderBottom = 30; // Measure the bottom border in your 480x106 sprite

        // Ensure minimum size
        int minWidth = borderLeft + borderRight;
        int minHeight = borderTop + borderBottom;

        if (w < minWidth) w = minWidth;
        if (h < minHeight) h = minHeight;

        // Calculate source texture regions
        int srcWidth = value.Width;
        int srcHeight = value.Height;
        int srcCenterWidth = srcWidth - borderLeft - borderRight;
        int srcCenterHeight = srcHeight - borderTop - borderBottom;

        // Draw the 9 sections of the panel
        #region Inner
        // Top-left corner
        sb.Draw(value2, new Rectangle(x, y, borderLeft, borderTop),
                new Rectangle(0, 0, borderLeft, borderTop), c);

        // Top edge (stretch horizontally)
        sb.Draw(value2, new Rectangle(x + borderLeft, y, w - borderLeft - borderRight, borderTop),
                new Rectangle(borderLeft, 0, srcCenterWidth, borderTop), c);

        // Top-right corner
        sb.Draw(value2, new Rectangle(x + w - borderRight, y, borderRight, borderTop),
                new Rectangle(srcWidth - borderRight, 0, borderRight, borderTop), c);

        // Left edge (stretch vertically)
        sb.Draw(value2, new Rectangle(x, y + borderTop, borderLeft, h - borderTop - borderBottom),
                new Rectangle(0, borderTop, borderLeft, srcCenterHeight), c);

        // Center (stretch both ways)
        sb.Draw(value2, new Rectangle(x + borderLeft, y + borderTop, w - borderLeft - borderRight, h - borderTop - borderBottom),
                new Rectangle(borderLeft, borderTop, srcCenterWidth, srcCenterHeight), c);

        // Right edge (stretch vertically)
        sb.Draw(value2, new Rectangle(x + w - borderRight, y + borderTop, borderRight, h - borderTop - borderBottom),
                new Rectangle(srcWidth - borderRight, borderTop, borderRight, srcCenterHeight), c);

        // Bottom-left corner
        sb.Draw(value2, new Rectangle(x, y + h - borderBottom, borderLeft, borderBottom),
                new Rectangle(0, srcHeight - borderBottom, borderLeft, borderBottom), c);

        // Bottom edge (stretch horizontally)
        sb.Draw(value2, new Rectangle(x + borderLeft, y + h - borderBottom, w - borderLeft - borderRight, borderBottom),
                new Rectangle(borderLeft, srcHeight - borderBottom, srcCenterWidth, borderBottom), c);

        // Bottom-right corner
        sb.Draw(value2, new Rectangle(x + w - borderRight, y + h - borderBottom, borderRight, borderBottom),
                new Rectangle(srcWidth - borderRight, srcHeight - borderBottom, borderRight, borderBottom), c);
        #endregion

        #region Outline
        // Top-left corner
        sb.Draw(value, new Rectangle(x, y, borderLeft, borderTop),
                new Rectangle(0, 0, borderLeft, borderTop), Color.White);

        // Top edge (stretch horizontally)
        sb.Draw(value, new Rectangle(x + borderLeft, y, w - borderLeft - borderRight, borderTop),
                new Rectangle(borderLeft, 0, srcCenterWidth, borderTop), Color.White);

        // Top-right corner
        sb.Draw(value, new Rectangle(x + w - borderRight, y, borderRight, borderTop),
                new Rectangle(srcWidth - borderRight, 0, borderRight, borderTop), Color.White);

        // Left edge (stretch vertically)
        sb.Draw(value, new Rectangle(x, y + borderTop, borderLeft, h - borderTop - borderBottom),
                new Rectangle(0, borderTop, borderLeft, srcCenterHeight), Color.White);

        // Center (stretch both ways)
        sb.Draw(value, new Rectangle(x + borderLeft, y + borderTop, w - borderLeft - borderRight, h - borderTop - borderBottom),
                new Rectangle(borderLeft, borderTop, srcCenterWidth, srcCenterHeight), Color.White);

        // Right edge (stretch vertically)
        sb.Draw(value, new Rectangle(x + w - borderRight, y + borderTop, borderRight, h - borderTop - borderBottom),
                new Rectangle(srcWidth - borderRight, borderTop, borderRight, srcCenterHeight), Color.White);

        // Bottom-left corner
        sb.Draw(value, new Rectangle(x, y + h - borderBottom, borderLeft, borderBottom),
                new Rectangle(0, srcHeight - borderBottom, borderLeft, borderBottom), Color.White);

        // Bottom edge (stretch horizontally)
        sb.Draw(value, new Rectangle(x + borderLeft, y + h - borderBottom, w - borderLeft - borderRight, borderBottom),
                new Rectangle(borderLeft, srcHeight - borderBottom, srcCenterWidth, borderBottom), Color.White);

        // Bottom-right corner
        sb.Draw(value, new Rectangle(x + w - borderRight, y + h - borderBottom, borderRight, borderBottom),
                new Rectangle(srcWidth - borderRight, srcHeight - borderBottom, borderRight, borderBottom), Color.White);
        #endregion
    }

    public static void DrawPanel(SpriteBatch sb, Rectangle R, Color c = default, Texture texture = null) => DrawPanel(sb, R.X, R.Y, R.Width, R.Height, c);
}
