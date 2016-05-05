using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238


using Microsoft.Graphics.Canvas;
using Chubosaurus;

namespace EP1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public bool IsAllImagesLoaded = false;

        

        

        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// CreateResources is where we load in assets.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void myDrawingSurface_CreateResources(Microsoft.Graphics.Canvas.CanvasControl sender, Microsoft.Graphics.Canvas.CanvasCreateResourcesEventArgs args)
        {
            // TODO: load in assets
            // set parent canvas
            ContentPipeline_Image.ParentCanvas = sender;
            await ContentPipeline_Image.AddImage("cannon", @"Assets/tm_cannon.png");
            await ContentPipeline_Image.AddImage("clone", @"Assets/tm_clone.png");


            // setup the scene (this should really be done in another function)
            GenericScene gs = new GenericScene("test");
            
            Random r = new Random();
            for (int i = 0; i < 1; i++)
            {
                
                GenericItem gi = new GenericItem("test");
                gi.Location = new System.Numerics.Vector2(r.Next(0, 1000), r.Next(0, 800));
                gi.SetBitmapFromImageDictionary("cannon");
                gs.AddObject(gi);
            }

            SceneManager.AddScene(gs);
            SceneManager.CurrentScene = gs;

            IsAllImagesLoaded = true;

            GameTimer gt = new GameTimer(sender, 60, 30);
            
            
            //sender.Invalidate();
        }

        /// <summary>
        /// Our Draw function.
        /// </summary>
        /// <param name="sender">A Canvas Control.</param>
        /// <param name="args">Drawing Arguments.</param>
        private void myDrawingSurface_Draw(Microsoft.Graphics.Canvas.CanvasControl sender, Microsoft.Graphics.Canvas.CanvasDrawEventArgs args)
        {
            // get the drawing session
            CanvasDrawingSession cds = args.DrawingSession;

            // wait until all images are loaded before drawing
            if (IsAllImagesLoaded)
            {
                if (SceneManager.CurrentScene != null)
                {
                    SceneManager.CurrentScene.Draw(cds);
                }
            }
        }
    }
}
