using System;
using System.Collections.Generic;
using System.Text;

using Windows.UI;

namespace Chubosaurus
{
    /// <summary>
    /// A Generic Label.
    /// </summary>
    public class CGL_Label : GenericItem
    {
        /// <summary>
        /// CGL_Label constructor.
        /// </summary>
        /// <param name="text">The text to show in the middle of the button.</param>
        /// <param name="foreground_color">The color of the text.</param>
        /// <param name="width">The default width.</param>
        /// <param name="height">the default height.</param>
        public CGL_Label(string text = "", Color foreground_color = default(Color), uint width = 100, uint height = 50)
            : base()
        {
            this.Text = text;
            this.ForegroundColor = foreground_color;
            this.BorderColor = Colors.White;
            this.SetSize(width, height);
            this.FontSize = 18;
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="dt">The delta time.</param>
        /// <param name="gi">The input item if any.</param>
        public override void Update(TimeSpan dt, GenericInput gi)
        {
            base.Update(dt, gi);
        }

        /// <summary>
        /// Draw.
        /// </summary>
        /// <param name="cds">The surface to draw on.</param>
        public override void Draw(Microsoft.Graphics.Canvas.CanvasDrawingSession cds)
        {
            // test rectangle
            Windows.Foundation.Rect r = new Windows.Foundation.Rect(Location.X, Location.Y, Size.Width, Size.Height);

            cds.DrawRectangle(r, BorderColor);
            
            Microsoft.Graphics.Canvas.CanvasTextFormat ctf = new Microsoft.Graphics.Canvas.CanvasTextFormat();
            ctf.VerticalAlignment = Microsoft.Graphics.Canvas.CanvasVerticalAlignment.Center;
            ctf.ParagraphAlignment = Windows.UI.Text.ParagraphAlignment.Center;
            ctf.FontSize = FontSize;

            // draw the text
            cds.DrawText(Text, r, ForegroundColor, ctf);
        }

        #region [Properties --------------------------------------------------]

        public Color ForegroundColor { get; set; }

        public Color BorderColor { get; set; }

        public string Text { get; set; }

        public float FontSize { get; set; }

        #endregion
    }
}
