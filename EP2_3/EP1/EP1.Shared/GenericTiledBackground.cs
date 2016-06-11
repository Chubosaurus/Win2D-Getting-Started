using System;
using System.Collections.Generic;
using System.Text;

using System.Numerics;
using Microsoft.Graphics.Canvas;

namespace Chubosaurus
{
    /// <summary>
    /// A GenericItem that tiles the Image onto its drawing surface.
    /// It can also parallax scrolled if the Velocity is set to anything but 0.
    /// </summary>
    public class GenericTiledBackground : GenericItem
    {
        public GenericTiledBackground(CanvasControl canvas = null, string bitmap_id = "") : base("test")
        {
            if (canvas != null)
            {
                this.SetSize((uint)canvas.RenderSize.Width, (uint)canvas.RenderSize.Height);
            }

            if (this.CIBrush == null)
            {
                this.CIBrush = new CanvasImageBrush(canvas);
                this.CIBrush.Image = ContentPipeline_Image.ImageDictionary[bitmap_id];
                this.CIBrush.ExtendX = CanvasEdgeBehavior.Wrap;
                this.CIBrush.ExtendY = CanvasEdgeBehavior.Clamp;

                // NOTE(duan): NearestNeighbor causes jitter effect
                //this.CIBrush.Interpolation = CanvasImageInterpolation.NearestNeighbor;        
                // NOTE(duan): Anisotropic causes blur effect
                //this.CIBrush.Interpolation = CanvasImageInterpolation.Anisotropic;

                // NOTE(duan): Linear - no jitter and no blue effect, but a tad slower
                // NOTE(duan): we'll settle with the Linear resizing for now
                this.CIBrush.Interpolation = CanvasImageInterpolation.Linear;
            }

            this.VelocityFactor = 1.0f;
            this.Velocity = 0;            
            this.DoTranslate = false;
        }

        public override void Update(TimeSpan dt, GenericInput input)
        {
            base.Update(dt, input);

            // NOTE(duan): Observer design pattern in future eps.
            if (input is Message_Translate)
            {
                this.DoTranslate = true;
                this.VelocityFactor = ((Message_Translate)input).VelocityFactor;
            }
            else if(input is Message_StopTranslate)
            {
                this.DoTranslate = false;
                this.VelocityFactor = ((Message_StopTranslate)input).VelocityFactor;
            }

            if (this.Velocity != 0 && this.DoTranslate)
            {
                // update the xoffset based on the velocity
                this.XOffset -= (Velocity * ((double)dt.Milliseconds / 1000.0f)) * (VelocityFactor);
                // set the new position
                this.CIBrush.Transform = this.CIBrush.Transform = Matrix3x2.CreateTranslation(new Vector2((float)this.XOffset, 0));
            }           
        }

        /// <summary>
        /// Draw the background and tiled it.
        /// </summary>
        /// <param name="cds"></param>
        public override void Draw(CanvasDrawingSession cds)
        {
            base.Draw(cds);

            // checks
            if (this.CIBrush != null)
            {
                cds.FillRectangle(new Windows.Foundation.Rect(0, 0, Size.Width, Size.Height), CIBrush);
            }
        }


        #region [Properties --------------------------------------------------]

        public CanvasImageBrush CIBrush { get; set; }               // our image brush, this is use to paint the tile background

        // NOTE(duan): lets just do the X direction for now
        private double _xoffset;
        public double XOffset
        {
            get { return _xoffset; }
            set
            {
                if (_xoffset != value)
                {
                    _xoffset = value;
                }
            }
        }

        public bool DoTranslate { get; set; }
        public float VelocityFactor { get; set; }
        public double Velocity { get; set; }

        #endregion
    }
}
