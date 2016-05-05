using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.Graphics.Canvas;
using System.Numerics;
using Windows.Foundation;


namespace Chubosaurus
{
    public class GenericItem
    {
        public GenericItem(string name = "")
        {
            this.Name = name;

            this.DrawBoundingRectangle = false;
        }

        /// <summary>
        /// Update the GenericItem.
        /// </summary>
        /// <param name="dt">A delta time since the last update was called.</param>
        public virtual void Update(TimeSpan dt)
        {
            // test to see if the GameLoop works
            this.Location = new Vector2(this.Location.X + 1, this.Location.Y + 1);
        }

        /// <summary>
        /// Draw the GenericItem.
        /// </summary>
        /// <param name="cds">A surface to draw on.</param>
        public virtual void Draw(CanvasDrawingSession cds)
        {
            if (Bitmap == null)
                return;

            cds.DrawImage(Bitmap, Location);

            // draw bounding rectangle
            if(DrawBoundingRectangle)
                cds.DrawRectangle(BoundingRectangle, Windows.UI.Colors.Purple);
        }

        public bool SetBitmapFromImageDictionary(string key)
        {
            CanvasBitmap cb = null;
            if (ContentPipeline_Image.ImageDictionary.TryGetValue(key, out cb))
            {
                this.Bitmap = cb;
                this.Size = this.Bitmap.SizeInPixels;
                return true;
            }

            return false;
        }

        // Properties
        public string Name { get; set; }
        public Vector2 Location { get; set; }
        public CanvasBitmap Bitmap { get; set; }
        public BitmapSize Size { get; set; }
        public Rect BoundingRectangle{ get { return new Rect(Location.X, Location.Y, Size.Width - 1, Size.Height - 1); } }
        public bool DrawBoundingRectangle { get; set; }
    }
}
