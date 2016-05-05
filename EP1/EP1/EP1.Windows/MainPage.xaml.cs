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

namespace EP1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// CreateResources is where we load in assets.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void myDrawingSurface_CreateResources(Microsoft.Graphics.Canvas.CanvasControl sender, Microsoft.Graphics.Canvas.CanvasCreateResourcesEventArgs args)
        {

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

            // draw a white line from (0, 0) to (100, 100)
            // check out the overloads
            cds.DrawLine(0, 0, 100, 100, Windows.UI.Colors.White);

            // draw a Yellow circle at (200, 200) with a radius of 50
            // check out the overloads
            cds.DrawCircle(200, 200, 50, Windows.UI.Colors.Yellow);

            // draw a Pink circle at (300, 300) with a radius of 50
            // bucket fill this circle with Pink
            // check out the overloads
            cds.FillCircle(300, 300, 50, Windows.UI.Colors.Pink);

            // draw a Green Rectangle at (200, 200) with a width of 300 and a height of 300
            // check out the overloads
            cds.DrawRectangle(200, 200, 300, 300, Windows.UI.Colors.Green);

            // draw a LimeGreen Rectangle at (400, 400) with a width of 100 and a height of 100
            // bucket fill this rectangle with LimeGreen
            // check out the overloads
            cds.FillRectangle(400, 400, 100, 100, Windows.UI.Colors.LimeGreen);

            // draw the string "Hello World" in LightBlue at (150, 50)
            // check out the overloads
            cds.DrawText("Hello World", 150, 50, Windows.UI.Colors.LightBlue);

            // other drawing functions not covered in this tutorial
            // cds.DrawEllipse
            // cds.FillEllipse
            // cds.DrawRoundedRectangle
            // cds.FillRoundedRectangle
            // cds.DrawImage - we will cover this later
        }
    }
}
