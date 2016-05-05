using System;
using System.Collections.Generic;
using System.Text;


using Windows.UI;

namespace Chubosaurus
{
    public class CGL_Button : GenericItem
    {
        public CGL_Button(string text = "")
            : base()
        {
            this.Text = text;
            this.BorderColor = Colors.White;
            this.HighlightBorderColor = Colors.Lime;
        }

        public override void Update(TimeSpan dt, GenericInput gi)
        {
            // TODO(duan): respond to hover event item
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
                            this.Text = "Click";
                        }
                        break;
                }
            }



            // TODO(duan): respond to mouse click event
        }

        public override void Draw(Microsoft.Graphics.Canvas.CanvasDrawingSession cds)
        {
            // test rectangle
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
            
            Microsoft.Graphics.Canvas.CanvasTextFormat ctf = new Microsoft.Graphics.Canvas.CanvasTextFormat();
            ctf.VerticalAlignment = Microsoft.Graphics.Canvas.CanvasVerticalAlignment.Center;
            ctf.ParagraphAlignment = Windows.UI.Text.ParagraphAlignment.Center;

            // draw the text
            cds.DrawText(Text, r, Colors.Red, ctf);
        }


        public Color BorderColor { get; set; }
        public Color HighlightBorderColor { get; set; }

        public string Text { get; set; }

        private bool IsHover { get; set; }
    }
}
