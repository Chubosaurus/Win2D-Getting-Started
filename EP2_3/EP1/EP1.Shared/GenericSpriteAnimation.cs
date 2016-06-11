using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Graphics.Canvas;
using System.Numerics;
using Windows.Foundation;

namespace Chubosaurus
{
    /// <summary>
    /// A Generic SpriteAnimation class.
    /// </summary>
    public class GenericSpriteAnimation : GenericItem
    {
        public GenericSpriteAnimation()
            : base("Generic Sprite Animation")
        {
            this.AnimationSpeed = 24.0f;                 // default to 24 frame per second
            this.IsPlaying = false;
            this.LoopCount = 0;
            this.LoopForever = true;
            this.CurrentFrame = 0;
            this.ElapsedTime = 0;
        }

        /// <summary>
        /// Update the sprite sheet to the next frame.
        /// </summary>
        /// <param name="dt">The delta time that has pass.</param>
        /// <param name="input">The input item if any.</param>
        public override void Update(TimeSpan dt, GenericInput input)
        {
            base.Update(dt, input);

            // check to see if we are playing the animation, if not just exit
            if (!IsPlaying)
                return;

            ElapsedTime += dt.Milliseconds;                     // add the time

            // check to see we need to move the frame forward
            if (ElapsedTime > this.DeltaMsBetweenFrames)
            {
                // frame adjustment based on the time that has pass
                while (ElapsedTime > this.DeltaMsBetweenFrames)
                {
                    ElapsedTime -= this.DeltaMsBetweenFrames;

                    if(this.CurrentFrame < this.TotalFrameCount - 1)
                    {
                        this.CurrentFrame++;
                    }
                    else
                    {
                        if (LoopForever)
                        {
                            this.CurrentFrame = 0;
                            this.LoopCount++;
                        }
                        else
                        {
                            this.LoopCount++;
                        }
                    }
                }

                // NOTE(duan): at this point, the ElapsedTime should contain the offset
                // TODO(duan): debugged this, I think my math is correct.. but who knows it's 5AM.
                // ElapsedTime = 0;
            }
        }

        /// <summary>
        /// Draw each frame.
        /// </summary>
        /// <param name="cds">The surface to draw to.</param>
        public override void Draw(CanvasDrawingSession cds)
        {           
            // draw the current frame of the animation
            cds.DrawImage(Frames[CurrentFrame], this.X, this.Y);
        }

        /// <summary>
        /// Start the animation.
        /// </summary>
        public void Play()
        {
            this.IsPlaying = true;
        }

        /// <summary>
        /// Stop the animation.
        /// </summary>
        public void Stop()
        {
            this.IsPlaying = false;
        }

        /// <summary>
        /// Cut the sprite sheet into rectangles and insert them to the frame buffer.
        /// The flow-direction is hard coded to Top-Left to Bottom-Right.
        /// </summary>
        /// <param name="bitmap_id">The bitmap from the ContentPipeline to cut.</param>
        /// <param name="frame_width">The width of each frame.</param>
        /// <param name="frame_height">The height of each frame.</param>
        public void FramesFromSpriteSheet(string bitmap_id, int frame_width, int frame_height)
        {
            int frame_counter = 0;

            CanvasBitmap cb = null;

            // grap the bitmap from the ContentPipeLine
            if (ContentPipeline_Image.ImageDictionary.TryGetValue(bitmap_id, out cb))
            {
                // slice the image as ghetto as we can
                int start_x = 0;
                int start_y = 0;

                // start cutting until we're at the outter bottom right edge
                while (start_x < cb.SizeInPixels.Width && start_y < cb.SizeInPixels.Height)
                {
                    // create the frame
                    CanvasRenderTarget crt = new CanvasRenderTarget(ContentPipeline_Image.ParentCanvas, frame_width, frame_height);

                    // draw the slice on to the backbuffer
                    using (CanvasDrawingSession cds = crt.CreateDrawingSession())
                    {                        
                        cds.DrawImage(cb,
                            new Rect(0, 0, frame_width, frame_height),
                            new Rect(start_x, start_y, frame_width, frame_height)
                            );
                    }

                    // next frame
                    start_x += frame_width;

                    // check to see if we are at the overlap
                    if (start_x >= cb.SizeInPixels.Width)
                    {
                        start_x = 0;                    // reset back to beginning
                        start_y += frame_height;        // get the next set of image by increasing to the next rect
                    }

                    // add the frame into the frame list
                    this.Frames.Add(crt);
                    frame_counter++;
                }
            }

            this.TotalFrameCount = frame_counter;
        }

        /// <summary>
        /// Load a bitmap into a frame of the animation manually.
        /// </summary>
        /// <param name="bitmap_id">The bitmap from the ContentPipeline to load.</param>
        /// <param name="frame_width">The width of frame.</param>
        /// <param name="frame_height">The height of frame</param>
        public void LoadFrameFromBitmap(string bitmap_id, int frame_width, int frame_height)
        {
            // check to see if we have loaded the image
            if (ContentPipeline_Image.ImageDictionary.ContainsKey(bitmap_id))
            {
                CanvasBitmap cb = null;

                // create the backbuffer
                CanvasRenderTarget crt = new CanvasRenderTarget(ContentPipeline_Image.ParentCanvas, frame_width, frame_height);

                ContentPipeline_Image.ImageDictionary.TryGetValue(bitmap_id, out cb);

                // draw it
                using (CanvasDrawingSession cds = crt.CreateDrawingSession())
                {
                    cds.DrawImage(cb,
                        new Rect(0, 0, frame_width, frame_height),
                        new Rect(0, 0, frame_width, frame_height)
                        );
                }

                // insert the frame
                this.Frames.Add(crt);
                this.TotalFrameCount = this.Frames.Count;
            }
        }

        #region [Properties --------------------------------------------------]

        public double AnimationSpeed { get; set; }      // the number of pages to flip per second
        public bool IsPlaying { get; set; }             // if the animation is playing or not
        public bool LoopForever { get; set; }           // continous playing
        public int LoopCount { get; set; }              // the number of loops since playing has began
        public int TotalFrameCount { get; set; }        // the number of frames in the animation

        public int CurrentFrame { get; set; }          // the current frame

        private int DeltaMsBetweenFrames { get { return (int)(1000.0f / (float)AnimationSpeed); } }

        private float ElapsedTime { get; set; }         // the time that has already pass
        
        // TODO(duan): we need to optimize this so objects of the same animation share the
        // same backbuffer, and not have each object with its own backbuffer, this will
        // save a lot of space.

        // NOTE(duan): the animation is just a list of backbuffers
        // NOTE(duan): http://microsoft.github.io/Win2D/html/M_Microsoft_Graphics_Canvas_CanvasRenderTarget__ctor_3.htm
        // TODO(duan): find out if they actually update to another faster backbuffer in newer version of Win2D
        public List<CanvasRenderTarget> Frames = new List<CanvasRenderTarget>();

        #endregion
    }
}
