using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

using Windows.UI;
using Microsoft.Graphics.Canvas;

namespace Chubosaurus
{
    /// <summary>
    /// A Generic Credits Scene.
    /// </summary>
    public class CreditsScene : GenericScene
    {
        public CreditsScene(int width, int height)
            : base("Credits Scene")
        {
            this._width = width;
            this._height = height;
            this.SetupScene();
        }

        protected override void SetupScene()
        {
            // NOTE(duan): design the scene manually, wouldn't be great if we had a tool ... lol            
            _title_label = new CGL_Label("Generic Credits Scene", Colors.White, (uint)(this._width * 0.60f), 100);
            _title_label.Y = 20;
            _title_label.FontSize = 50;
            CenterObject(_title_label, true, false);

            _back_button = new CGL_Button("< Back", Colors.White);
            _back_button.SetSize(100, 100);
            _back_button.Location = new Vector2(_title_label.Location.X - _back_button.Size.Width - 10, _title_label.Location.Y);

            this.AddObject(_title_label);
            this.AddObject(_back_button);

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
