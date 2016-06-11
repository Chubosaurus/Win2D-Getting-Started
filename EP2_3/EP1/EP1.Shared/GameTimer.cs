using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Graphics.Canvas;        // CanvasControl (Win2D)
using Windows.UI.Xaml;                  // DispatchTimer

namespace Chubosaurus
{
    /// <summary>
    /// The timer that dictates the Game Loop.
    /// </summary>
    public class GameTimer
    {
        /// <summary>
        /// GameTimer constructor, sets default values.
        /// </summary>
        public GameTimer(CanvasControl surface = null, int UPS = 60, int FPS = 45)
        {
            UpdateTimer = new DispatcherTimer();
            UpdateTimer.Interval = TimeSpan.FromMilliseconds(1000 / UPS);        // 60 UPS
            UpdateTimer.Tick += UpdateTimer_Tick;

            // set FPS
            TimeBetweenDraw = TimeSpan.FromMilliseconds(1000 / FPS);             // default 30 fps

            LastUpdateTime = DateTime.Now;
            LastDrawTime = DateTime.Now;
            TotalAppTime = TimeSpan.FromMilliseconds(0);

            // set the canvas control
            ParentCanvas = surface;

            UpdateTimer.Start();
        }

        /// <summary>
        /// Update timer for the game, this should run 60 times a second (default).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UpdateTimer_Tick(object sender, object e)
        {
            if (!IsUpating)
            {
                IsUpating = true;

                // compute the delta time
                TimeSpan dt = DateTime.Now - LastUpdateTime;

                // update the last time the program has updated
                LastUpdateTime = DateTime.Now;

                // update the total app time
                TotalAppTime += dt;

                // get the current item from the InputManager
                GenericInput gi = InputManager.Update();

                // NOTE(duan): we need to update the STORYBOARD
                if (SceneManager.Update(dt, gi))
                {
                    // NOTE(duan): do nothing in the game loop since we're switching scenes or going back in the history stack.                    
                }
                else
                {
                    // update the current scene
                    if (SceneManager.CurrentScene != null)
                    {
                        SceneManager.CurrentScene.Update(dt, gi);
                        gi = InputManager.PeekAndTake(typeof(MouseGenericInput));
                        while (gi is MouseGenericInput)
                        {
                            SceneManager.CurrentScene.Update(TimeSpan.Zero, gi);
                            gi = InputManager.PeekAndTake(typeof(MouseGenericInput));
                        }
                    }
                }

                // figure out if we need to draw/refresh the screen
                TimeSpan dt_draw = DateTime.Now - LastDrawTime;
                if(dt_draw > TimeBetweenDraw)
                {
                    if (ParentCanvas != null)
                    {
                        ParentCanvas.Invalidate();
                        LastDrawTime = DateTime.Now;
                    }
                }

                IsUpating = false;
            }
        }

        // Properties

        /// <summary>
        /// The UpdateTimer (for now it is a DispatchTimer)
        /// </summary>
        private DispatcherTimer UpdateTimer;

        /// <summary>
        /// Update() flag.  If this is set the Update() function is running.  This is to prevent another call to Update() while one is already running.
        /// </summary>
        public bool IsUpating = false;

        /// <summary>
        /// Total running time of the Application.
        /// </summary>
        public TimeSpan TotalAppTime { get; set; }

        /// <summary>
        /// The last time the Update was call.
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// The last time the Draw was call.
        /// </summary>
        public DateTime LastDrawTime { get; set; }

        /// <summary>
        /// The delta time between Draw calls.  (This is basically your FPS)
        /// </summary>
        public TimeSpan TimeBetweenDraw { get; set; }

        /// <summary>
        /// Pointer to the current CanvasControl.  This is needed so we can Invalidate() the surface to cause a Redraw.
        /// </summary>
        public CanvasControl ParentCanvas { get; set; }        
    }
}
