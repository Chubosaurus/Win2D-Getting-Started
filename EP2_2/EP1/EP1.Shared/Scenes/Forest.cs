using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

using Windows.UI;
using Microsoft.Graphics.Canvas;

namespace Chubosaurus
{

    /// <summary>
    /// A Sample Parallax Scrolling scene of a Forest.
    /// </summary>
    public class ForestScene : GenericLayeredScene
    {
        public ForestScene(CanvasControl canvas = null)
            : base("Forest Scene")
        {
            if (canvas != null)
            {
                this.SetSize((uint)canvas.RenderSize.Width, (uint)canvas.RenderSize.Height);
                this._width = (int)canvas.RenderSize.Width;
                this._height = (int)canvas.RenderSize.Height;
            }
            this.SetupScene(canvas);
        }

        /// <summary>
        /// Setup our scene.
        /// </summary>
        /// <param name="canvas">The drawing surface.</param>
        protected override void SetupScene(CanvasControl canvas = null)
        {
            base.SetupScene();

            // NOTE(duan): design the scene manually, wouldn't be great if we had a tool ... lol            
            _title_label = new CGL_Label("Forest", Colors.White, (uint)(this._width * 0.60f), 100);
            _title_label.Y = 20;
            _title_label.FontSize = 50;
            CenterObject(_title_label, true, false);

            _back_button = new CGL_Button("< Back", Colors.White);
            _back_button.SetSize(100, 100);
            _back_button.Location = new Vector2(_title_label.Location.X - _back_button.Size.Width - 10, _title_label.Location.Y);

            this.AddObject(_title_label);
            this.AddObject(_back_button);

            // create the layers of the city
            for (int i = 1; i < 8; i++)
            {
                GenericTiledBackground gi = new GenericTiledBackground(canvas, string.Format("bg_fn_0{0}", i));
                gi.Velocity = i;
                gi.ZIndex = i;
                this.AddObject(gi);
            }

            // set each layers scroll speed so achive the parallax effect
            ((GenericTiledBackground)this.objects[0]).Velocity = 0.25f;
            ((GenericTiledBackground)this.objects[1]).Velocity = 1;
            ((GenericTiledBackground)this.objects[2]).Velocity = 2.5;
            ((GenericTiledBackground)this.objects[3]).Velocity = 9;
            ((GenericTiledBackground)this.objects[4]).Velocity = 18;
            ((GenericTiledBackground)this.objects[5]).Velocity = 100;
            ((GenericTiledBackground)this.objects[6]).Velocity = 130; // ground

            // event call backs
            _back_button.ButtonClick += _back_button_ButtonClick;
        }

        void _back_button_ButtonClick(object sender, CGL_Button_Event e)
        {
            // create a switch message that pop that scene_history
            Message_GoBack mgb = new Message_GoBack();
            InputManager.AddInputItem(mgb);
        }

        private CGL_Label _title_label;
        private CGL_Button _back_button;
    }
}
