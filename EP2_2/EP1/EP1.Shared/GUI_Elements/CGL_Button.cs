using System;
using System.Collections.Generic;
using System.Text;


using Windows.UI;

namespace Chubosaurus
{
    /// <summary>
    /// A Generic Button that responds the OnClick event.
    /// </summary>
    public class CGL_Button : CGL_Label
    {
        /// <summary>
        /// CGL_Button constructor.
        /// </summary>
        /// <param name="text">The text to show in the middle of the button.</param>
        /// <param name="foreground_color">The color of the text.</param>
        /// <param name="width">The default width.</param>
        /// <param name="height">the default height.</param>
        public CGL_Button(string text = "", Color foreground_color = default(Color), uint width = 100, uint height = 50)
            : base(text, foreground_color, width, height)
        {
            this.HighlightBorderColor = Colors.Lime;
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="dt">The delta time.</param>
        /// <param name="gi">The input item if any.</param>
        public override void Update(TimeSpan dt, GenericInput gi)
        {
            if(gi is MouseGenericInput)
            {
                MouseGenericInput mgi = (MouseGenericInput)gi;
                switch (mgi.MouseInputType)
                {
                    case MouseGenericInputType.MouseMove:
                        {
                            // do hit test
                            if (mgi.X > this.Location.X && mgi.X < this.Location.X + this.Size.Width &&
                                mgi.Y > this.Location.Y && mgi.Y < this.Location.Y + this.Size.Height)
                            {
                                IsHover = true;
                            }
                            else
                            {
                                IsHover = false;
                            }
                        }
                        break;

                    case MouseGenericInputType.MouseClick:
                        {
                            if (IsHover)
                            {
                                // raise the event
                                OnButtonClicked(null);
                            }                            
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Draw.
        /// </summary>
        /// <param name="cds">The surface to draw on.</param>
        public override void Draw(Microsoft.Graphics.Canvas.CanvasDrawingSession cds)
        {
            // the bounding rectangle
            Windows.Foundation.Rect r = new Windows.Foundation.Rect(Location.X, Location.Y, Size.Width, Size.Height);

            // draw the border
            if (IsHover)
            {
                cds.DrawRectangle(r, HighlightBorderColor);
            }
            else
            {
                cds.DrawRectangle(r, BorderColor);
            }
            
            // create the text description
            Microsoft.Graphics.Canvas.CanvasTextFormat ctf = new Microsoft.Graphics.Canvas.CanvasTextFormat();
            ctf.VerticalAlignment = Microsoft.Graphics.Canvas.CanvasVerticalAlignment.Center;
            ctf.ParagraphAlignment = Windows.UI.Text.ParagraphAlignment.Center;
            ctf.FontSize = this.FontSize;

            // draw the text
            cds.DrawText(Text, r, ForegroundColor, ctf);
        }

        /// <summary>
        /// Reset the button.
        /// </summary>
        public override void Reset()
        {
            this.IsHover = false;
        }


        #region [Properties --------------------------------------------------]

        public Color HighlightBorderColor { get; set; }

        private bool IsHover { get; set; }

        #endregion


        #region [Events ------------------------------------------------------]

        public delegate void CGL_Button_Event_Handler(object sender, CGL_Button_Event e);

        private event CGL_Button_Event_Handler _button_click;
        public event CGL_Button_Event_Handler ButtonClick
        {
            add { _button_click += value; }
            remove { _button_click -= value; }
        }
        protected virtual void OnButtonClicked(CGL_Button_Event e)
        {
            // TODO(duan): custom actions

            // NOTE(duan): do call back
            if (_button_click != null)
            {
                _button_click.Invoke(this, e);
            }
        }

        #endregion
    }

    /// <summary>
    /// Alias for the Button Event.
    /// </summary>
    public class CGL_Button_Event : EventArgs
    {
    }
}
