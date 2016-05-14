using System;
using System.Collections.Generic;
using System.Text;

using System.Numerics;

using Windows.UI;

using Microsoft.Graphics.Canvas;

namespace Chubosaurus
{
    /// <summary>
    /// A sample title scene.
    /// </summary>
    public class TitleScene : GenericScene
    {
        public TitleScene(int width, int height) : base("Generic Title Scene")
        {
            this._width = width;
            this._height = height;
            SetupScene();
        }

        public override void Update(TimeSpan dt, GenericInput input)
        {
            base.Update(dt, input);
        }

        public override void Draw(CanvasDrawingSession cds)
        {
            base.Draw(cds);
        }

        protected override void SetupScene()
        {
            // NOTE(duan): design the scene manually, wouldn't be great if we had a tool ... lol            
            _title_label = new CGL_Label("Generic Title Scene", Colors.White, (uint)(this._width * 0.90f), 100);            
            _title_label.Y = 20;
            _title_label.FontSize = 50;
            CenterObject(_title_label, true, false);

            _start_button = new CGL_Button("Start A New Game", Colors.White, 350, 50);            
            _top_score_button = new CGL_Button("View The Current Top Scores", Colors.White, 350, 50);
            _credits_button = new CGL_Button("Credits", Colors.White, 350, 50);
            _parallax_city_button = new CGL_Button("See Parallax Scenes (City)", Color.FromArgb(0xFF,0x00,0xFF,0x00), 350, 50);
            _parallax_forest_button = new CGL_Button("The Forest (CLICK HERE)", Colors.Magenta, 350, 50);

            CenterObject(_start_button);
            _top_score_button.Location = new Vector2(_start_button.Location.X, _start_button.Location.Y + _start_button.Size.Height + 10);
            _credits_button.Location = new Vector2(_top_score_button.Location.X, _top_score_button.Location.Y + _top_score_button.Size.Height + 10);
            _parallax_city_button.Location = new Vector2(_credits_button.Location.X, _credits_button.Location.Y + _credits_button.Size.Height + 10);           
            //_parallax_forest_button.Location = new Vector2(_parallax_city_button.X, _parallax_city_button.Location.Y + _parallax_city_button.Size.Height + 10);
            _parallax_forest_button.Location = new Vector2(_credits_button.Location.X, _credits_button.Location.Y + _credits_button.Size.Height + 10);

            this.AddObject(_title_label);
            this.AddObject(_start_button);
            this.AddObject(_top_score_button);
            this.AddObject(_credits_button);
            //this.AddObject(_parallax_city_button);
            this.AddObject(_parallax_forest_button);

            // event callbacks
            _start_button.ButtonClick += _start_button_ButtonClick;
            _top_score_button.ButtonClick += _top_score_button_ButtonClick;
            _credits_button.ButtonClick += _credits_button_ButtonClick;
            _parallax_city_button.ButtonClick += _parallax_city_button_ButtonClick;
            _parallax_forest_button.ButtonClick += _parallax_forest_button_ButtonClick;
        }

        void _parallax_forest_button_ButtonClick(object sender, CGL_Button_Event e)
        {
            // create the scene switch message to switch the current scene to the Forest scene
            Message_SceneSwitch mss = new Message_SceneSwitch("Forest Scene");
            InputManager.AddInputItem(mss);            
        }

        void _parallax_city_button_ButtonClick(object sender, CGL_Button_Event e)
        {
            // create the scene switch message to switch the current scene to the City scene
            Message_SceneSwitch mss = new Message_SceneSwitch("City Scene");
            InputManager.AddInputItem(mss);            
        }

        void _credits_button_ButtonClick(object sender, CGL_Button_Event e)
        {
            // create the scene switch message to switch the current scene to the credits scene
            Message_SceneSwitch mss = new Message_SceneSwitch("Credits Scene");
            InputManager.AddInputItem(mss);            
        }
        void _top_score_button_ButtonClick(object sender, CGL_Button_Event e)
        {
            // create the scene switch message to switch the current scene to the top score scene
            Message_SceneSwitch mss = new Message_SceneSwitch("Top Score Scene");
            InputManager.AddInputItem(mss);            
        }
        private void _start_button_ButtonClick(object sender, CGL_Button_Event e)
        {
            // create the scene switch message to switch the current scene to the main game scene
            Message_SceneSwitch mss = new Message_SceneSwitch("Main Game Scene");
            InputManager.AddInputItem(mss);            
        }

        private CGL_Label _title_label;
        private CGL_Button _start_button;
        private CGL_Button _top_score_button;
        private CGL_Button _credits_button;
        private CGL_Button _parallax_city_button;
        private CGL_Button _parallax_forest_button;
    }
}
