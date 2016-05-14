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

            // load in any sprite sheets
            await ContentPipeline_Image.AddImage("explosion", @"Assets/SpriteSheets/explosion.png");

            // load in all the layers for the layer episode
            await ContentPipeline_Image.AddImage("bg_city_01", @"Assets/Background/CityLayers/layer_01_1920 x 1080.png");
            await ContentPipeline_Image.AddImage("bg_city_02", @"Assets/Background/CityLayers/layer_02_1920 x 1080.png");
            await ContentPipeline_Image.AddImage("bg_city_03", @"Assets/Background/CityLayers/layer_03_1920 x 1080.png");
            await ContentPipeline_Image.AddImage("bg_city_04", @"Assets/Background/CityLayers/layer_04_1920 x 1080.png");
            await ContentPipeline_Image.AddImage("bg_city_05", @"Assets/Background/CityLayers/layer_05_1920 x 1080.png");
            await ContentPipeline_Image.AddImage("bg_city_06", @"Assets/Background/CityLayers/layer_06_1920 x 1080.png");
            await ContentPipeline_Image.AddImage("bg_city_07", @"Assets/Background/CityLayers/layer_07_1920 x 1080.png");
            await ContentPipeline_Image.AddImage("bg_city_08", @"Assets/Background/CityLayers/layer_08_1920 x 1080.png");

            await ContentPipeline_Image.AddImage("bg_fn_01", @"Assets/Background/ForestNightLayers/layer_01_1920 x 1080.png");
            await ContentPipeline_Image.AddImage("bg_fn_02", @"Assets/Background/ForestNightLayers/layer_02_1920 x 1080.png");
            await ContentPipeline_Image.AddImage("bg_fn_03", @"Assets/Background/ForestNightLayers/layer_03_1920 x 1080.png");
            await ContentPipeline_Image.AddImage("bg_fn_04", @"Assets/Background/ForestNightLayers/layer_04_1920 x 1080.png");
            await ContentPipeline_Image.AddImage("bg_fn_05", @"Assets/Background/ForestNightLayers/layer_05_1920 x 1080.png");
            await ContentPipeline_Image.AddImage("bg_fn_06", @"Assets/Background/ForestNightLayers/layer_06_1920 x 1080.png");
            await ContentPipeline_Image.AddImage("bg_fn_07", @"Assets/Background/ForestNightLayers/layer_07_1920 x 1080.png");

            // EP2_3 : load in the rock dude
            await ContentPipeline_Image.AddImage("RD_WALK_01", @"Assets/Entity/RockPerson/MSHero_2_Walking0001.png");
            await ContentPipeline_Image.AddImage("RD_WALK_02", @"Assets/Entity/RockPerson/MSHero_2_Walking0002.png");
            await ContentPipeline_Image.AddImage("RD_WALK_03", @"Assets/Entity/RockPerson/MSHero_2_Walking0003.png");
            await ContentPipeline_Image.AddImage("RD_WALK_04", @"Assets/Entity/RockPerson/MSHero_2_Walking0004.png");
            await ContentPipeline_Image.AddImage("RD_WALK_05", @"Assets/Entity/RockPerson/MSHero_2_Walking0005.png");
            await ContentPipeline_Image.AddImage("RD_WALK_06", @"Assets/Entity/RockPerson/MSHero_2_Walking0006.png");
            await ContentPipeline_Image.AddImage("RD_WALK_07", @"Assets/Entity/RockPerson/MSHero_2_Walking0007.png");
            await ContentPipeline_Image.AddImage("RD_WALK_08", @"Assets/Entity/RockPerson/MSHero_2_Walking0008.png");
            await ContentPipeline_Image.AddImage("RD_WALK_09", @"Assets/Entity/RockPerson/MSHero_2_Walking0009.png");
            await ContentPipeline_Image.AddImage("RD_WALK_10", @"Assets/Entity/RockPerson/MSHero_2_Walking0010.png");
            await ContentPipeline_Image.AddImage("RD_WALK_11", @"Assets/Entity/RockPerson/MSHero_2_Walking0011.png");
            await ContentPipeline_Image.AddImage("RD_WALK_12", @"Assets/Entity/RockPerson/MSHero_2_Walking0012.png");

            await ContentPipeline_Image.AddImage("RD_JUMP_01", @"Assets/Entity/RockPerson/MSHero_2_Jumping0001.png");
            await ContentPipeline_Image.AddImage("RD_JUMP_02", @"Assets/Entity/RockPerson/MSHero_2_Jumping0002.png");
            await ContentPipeline_Image.AddImage("RD_JUMP_03", @"Assets/Entity/RockPerson/MSHero_2_Jumping0003.png");
            await ContentPipeline_Image.AddImage("RD_JUMP_04", @"Assets/Entity/RockPerson/MSHero_2_Jumping0004.png");
            await ContentPipeline_Image.AddImage("RD_JUMP_05", @"Assets/Entity/RockPerson/MSHero_2_Jumping0005.png");
            await ContentPipeline_Image.AddImage("RD_JUMP_06", @"Assets/Entity/RockPerson/MSHero_2_Jumping0006.png");
            await ContentPipeline_Image.AddImage("RD_JUMP_07", @"Assets/Entity/RockPerson/MSHero_2_Jumping0007.png");
            await ContentPipeline_Image.AddImage("RD_JUMP_08", @"Assets/Entity/RockPerson/MSHero_2_Jumping0008.png");
            await ContentPipeline_Image.AddImage("RD_JUMP_09", @"Assets/Entity/RockPerson/MSHero_2_Jumping0009.png");
            await ContentPipeline_Image.AddImage("RD_JUMP_10", @"Assets/Entity/RockPerson/MSHero_2_Jumping0010.png");
            await ContentPipeline_Image.AddImage("RD_JUMP_11", @"Assets/Entity/RockPerson/MSHero_2_Jumping0011.png");
            await ContentPipeline_Image.AddImage("RD_JUMP_12", @"Assets/Entity/RockPerson/MSHero_2_Jumping0012.png");

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

            CGL_Button b = new CGL_Button("my first button");            
            gs.AddObject(b);
            b.Location = new System.Numerics.Vector2(400, 400);
            BitmapSize new_size;
            new_size.Width = 250;
            new_size.Height = 50;
            b.Size = new_size;

            SceneManager.AddScene(gs);
            SceneManager.CurrentScene = gs;
            
            CanvasControl cc = sender;
            TitleScene ts = new TitleScene((int)cc.RenderSize.Width, (int)cc.RenderSize.Height);
            SceneManager.AddScene(ts);
            SceneManager.CurrentScene = ts;

            // NOTE(duan): create all the test scenes
            MainGamePlayScene mgps = new MainGamePlayScene((int)cc.RenderSize.Width, (int)cc.RenderSize.Height);
            TopScoresScene tsc = new TopScoresScene((int)cc.RenderSize.Width, (int)cc.RenderSize.Height);
            CreditsScene cs = new CreditsScene((int)cc.RenderSize.Width, (int)cc.RenderSize.Height);

            SceneManager.AddScene(mgps);
            SceneManager.AddScene(tsc);
            SceneManager.AddScene(cs);

            // Create the sample parallax scenes for the tutorial
            CityScene city = new CityScene(cc);
            ForestScene forest = new ForestScene(cc);

            SceneManager.AddScene(city);
            SceneManager.AddScene(forest);


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

        #region ----------[Mouse Event Handlers]
        /// <summary>
        /// Capture the mouse move event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myDrawingSurface_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Input.PointerPoint p = e.GetCurrentPoint((UIElement)sender);

            MouseGenericInput mgi = new MouseGenericInput((float)p.Position.X, (float)p.Position.Y);
            mgi.Name = "mouse_move";
            mgi.MouseInputType = MouseGenericInputType.MouseMove;
            mgi.IsLeftButtonPress = p.Properties.IsLeftButtonPressed;
            mgi.IsMiddleButtonPress = p.Properties.IsMiddleButtonPressed;
            mgi.IsRightButtonPress = p.Properties.IsRightButtonPressed;
            mgi.MouseDown = mgi.IsLeftButtonPress | mgi.IsMiddleButtonPress | mgi.IsRightButtonPress;

            InputManager.AddInputItem(mgi);
        }

        /// <summary>
        /// Capture the mouse press event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myDrawingSurface_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Input.PointerPoint p = e.GetCurrentPoint((UIElement)sender);

            MouseGenericInput mgi = new MouseGenericInput((float)p.Position.X, (float)p.Position.Y);
            mgi.Name = "mouse_down";
            mgi.MouseInputType = MouseGenericInputType.MousePressed;
            mgi.IsLeftButtonPress = p.Properties.IsLeftButtonPressed;
            mgi.IsMiddleButtonPress = p.Properties.IsMiddleButtonPressed;
            mgi.IsRightButtonPress = p.Properties.IsRightButtonPressed;
            mgi.MouseDown = mgi.IsLeftButtonPress | mgi.IsMiddleButtonPress | mgi.IsRightButtonPress;

            InputManager.AddInputItem(mgi);
        }

        /// <summary>
        /// Capture the mouse release event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myDrawingSurface_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Input.PointerPoint p = e.GetCurrentPoint((UIElement)sender);

            MouseGenericInput mgi = new MouseGenericInput((float)p.Position.X, (float)p.Position.Y);
            mgi.Name = "mouse_up";
            mgi.MouseInputType = MouseGenericInputType.MouseReleased;
            mgi.IsLeftButtonPress = p.Properties.IsLeftButtonPressed;
            mgi.IsMiddleButtonPress = p.Properties.IsMiddleButtonPressed;
            mgi.IsRightButtonPress = p.Properties.IsRightButtonPressed;
            mgi.MouseDown = mgi.IsLeftButtonPress | mgi.IsMiddleButtonPress | mgi.IsRightButtonPress;

            InputManager.AddInputItem(mgi);
        }

        #endregion
    }
}
