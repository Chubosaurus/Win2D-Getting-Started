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

        public override void Draw(CanvasDrawingSession cds)
        {
            base.Draw(cds);

            // NOTE(duan): draw the extra state for debugging
            cds.DrawText("STATE:" + RockPerson.State, new Vector2(10, 220), Windows.UI.Colors.White);
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
            _back_button.Location = new Vector2(_title_label.Location.X - _back_button.Size.Width - 10, _title_label.Location.Y);
            _back_button.SetSize(100, 100);

            _helper_label = new CGL_Label("Tap ANY Button to execute STATE change.", Colors.White);
            _helper_label.BackgroundColor = Colors.DarkMagenta;
            _helper_label.FontSize = 24;
            _helper_label.SetSize(500, 100);
            CenterObject(_helper_label);
            _helper_label.Y = (int)(_helper_label.Y * 0.50f);

            _jump_button = new CGL_Button("Jump", Colors.White);
            _jump_button.BackgroundColor = Colors.DarkSlateGray;
            _jump_button.HighlightBorderColor = Colors.OrangeRed;
            _jump_button.FontSize = 24;
            _jump_button.Location = new Vector2(400, 325);
            _jump_button.SetSize(200, 50);

            _die_button = new CGL_Button("Die", Colors.White);
            _die_button.BackgroundColor = Colors.DarkRed;
            _die_button.HighlightBorderColor = Colors.OrangeRed;
            _die_button.FontSize = 24;
            _die_button.Location = new Vector2(400, 400);
            _die_button.SetSize(200, 50);

            _attack_button = new CGL_Button("Attack", Colors.White);
            _attack_button.BackgroundColor = Colors.DarkSlateBlue;
            _attack_button.HighlightBorderColor = Colors.OrangeRed;
            _attack_button.FontSize = 24;
            _attack_button.Location = new Vector2(400, 475);
            _attack_button.SetSize(200, 50);

            _run_button = new CGL_Button("Run", Colors.White);
            _run_button.BackgroundColor = Colors.DarkSeaGreen;
            _run_button.HighlightBorderColor = Colors.OrangeRed;
            _run_button.FontSize = 24;
            _run_button.Location = new Vector2(400, 550);
            _run_button.SetSize(200, 50);

            _crouch_button = new CGL_Button("Crouch", Colors.White);
            _crouch_button.BackgroundColor = Colors.DarkOrchid;
            _crouch_button.HighlightBorderColor = Colors.OrangeRed;
            _crouch_button.FontSize = 24;
            _crouch_button.Location = new Vector2(400, 625);
            _crouch_button.SetSize(200, 50);

            _walk_right_button = new CGL_Button("Walk Right >>", Colors.White);
            _walk_right_button.BackgroundColor = Colors.Transparent;
            _walk_right_button.HighlightBorderColor = Colors.OrangeRed;
            _walk_right_button.FontSize = 24;
            _walk_right_button.SetSize(200, 50);
            _walk_right_button.Location = new Vector2(this._width - _walk_right_button.Size.Width - 10, 725);
            CenterObject(_walk_right_button, false, true);

            _walk_left_button = new CGL_Button("<< Walk Left", Colors.White);
            _walk_left_button.BackgroundColor = Colors.Transparent; ;
            _walk_left_button.HighlightBorderColor = Colors.OrangeRed;
            _walk_left_button.FontSize = 24;
            _walk_left_button.SetSize(200, 50);
            _walk_left_button.Location = new Vector2(10, 725);
            CenterObject(_walk_left_button, false, true);


            this.AddObject(_title_label);
            this.AddObject(_back_button);
            this.AddObject(_helper_label);
            this.AddObject(_attack_button);
            this.AddObject(_jump_button);
            this.AddObject(_die_button);
            this.AddObject(_run_button);
            this.AddObject(_crouch_button);

            this.AddObject(_walk_right_button);
            this.AddObject(_walk_left_button);

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

            //((GenericTiledBackground)this.objects[0]).Velocity = 0f;
            //((GenericTiledBackground)this.objects[1]).Velocity = 0f;
            //((GenericTiledBackground)this.objects[2]).Velocity = 0f;
            //((GenericTiledBackground)this.objects[3]).Velocity = 0f;
            //((GenericTiledBackground)this.objects[4]).Velocity = 0f;
            //((GenericTiledBackground)this.objects[5]).Velocity = 0f;
            //((GenericTiledBackground)this.objects[6]).Velocity = 0f; // ground


            // load RockPerson into the forest scene
            RockPerson = new GenericPlayer();
            RockPerson.Setup();
            //RockPerson.State = GENERIC_STATE.WALK;            
            RockPerson.State = GENERIC_STATE.IDLE;
            RockPerson.X = 1980/2 - 128/2;
            RockPerson.Y = 650;
            this.AddObject(RockPerson);

            // event call backs
            _back_button.ButtonClick += _back_button_ButtonClick;
            _jump_button.ButtonClick += _jump_button_ButtonClick;
            _die_button.ButtonClick += _die_button_ButtonClick;
            _attack_button.ButtonClick += _attack_button_ButtonClick;
            _run_button.ButtonClick += _run_button_ButtonClick;
            _crouch_button.ButtonClick += _crouch_button_ButtonClick;
            _walk_right_button.ButtonClick += _walk_right_button_ButtonClick;
            _walk_left_button.ButtonClick += _walk_left_button_ButtonClick;


        }

        void _walk_left_button_ButtonClick(object sender, CGL_Button_Event e)
        {
            if (RockPerson.IsAnimationCompleted(RockPerson.State) && RockPerson.State != GENERIC_STATE.WALK)
            {
                RockPerson.IsMirrored = true;
                RockPerson.State = GENERIC_STATE.WALK;
                RockPerson.ResetAnimation(RockPerson.State);
                Message_Translate msg_trans = new Message_Translate();
                msg_trans.VelocityFactor = -1.0f;
                InputManager.AddInputItem(msg_trans);
            }            
        }

        void _walk_right_button_ButtonClick(object sender, CGL_Button_Event e)
        {
            if (RockPerson.IsAnimationCompleted(RockPerson.State) && RockPerson.State != GENERIC_STATE.WALK)
            {
                RockPerson.IsMirrored = false;
                RockPerson.State = GENERIC_STATE.WALK;
                RockPerson.ResetAnimation(RockPerson.State);
                Message_Translate msg_trans = new Message_Translate();
                InputManager.AddInputItem(msg_trans);
            }
        }

        void _crouch_button_ButtonClick(object sender, CGL_Button_Event e)
        {
            if (
                (RockPerson.IsAnimationCompleted(RockPerson.State) && RockPerson.State != GENERIC_STATE.CROUCH) || RockPerson.State == GENERIC_STATE.IDLE)
            {
                RockPerson.State = GENERIC_STATE.CROUCH;
                RockPerson.ResetAnimation(RockPerson.State);
            }            
        }

        void _run_button_ButtonClick(object sender, CGL_Button_Event e)
        {
            if (
                (RockPerson.IsAnimationCompleted(RockPerson.State) && RockPerson.State != GENERIC_STATE.RUN) || RockPerson.State == GENERIC_STATE.IDLE)
            {
                RockPerson.State = GENERIC_STATE.RUN;
                RockPerson.ResetAnimation(RockPerson.State);
                Message_Translate msg_trans = new Message_Translate();
                msg_trans.VelocityFactor = 3.0f;
                if (RockPerson.IsMirrored)
                {
                    msg_trans.VelocityFactor *= -1;
                }
                InputManager.AddInputItem(msg_trans);
            }            
        }

        void _attack_button_ButtonClick(object sender, CGL_Button_Event e)
        {
            if (
                (RockPerson.IsAnimationCompleted(RockPerson.State) && RockPerson.State != GENERIC_STATE.ATTACK) || RockPerson.State == GENERIC_STATE.IDLE)
            {
                RockPerson.State = GENERIC_STATE.ATTACK;
                RockPerson.ResetAnimation(RockPerson.State);
            }
        }

        void _jump_button_ButtonClick(object sender, CGL_Button_Event e)
        {
            if ((RockPerson.IsAnimationCompleted(RockPerson.State) && RockPerson.State != GENERIC_STATE.JUMP) || RockPerson.State == GENERIC_STATE.IDLE)
            {
                RockPerson.Y -= 45;
                RockPerson.State = GENERIC_STATE.JUMP;
                RockPerson.ResetAnimation(RockPerson.State);
            }
        }

        void _die_button_ButtonClick(object sender, CGL_Button_Event e)
        {
            if ((RockPerson.IsAnimationCompleted(RockPerson.State) && RockPerson.State != GENERIC_STATE.DYING) || RockPerson.State == GENERIC_STATE.IDLE)
            {
                RockPerson.State = GENERIC_STATE.DYING;
                RockPerson.ResetAnimation(RockPerson.State);
            }            
        }

        void _back_button_ButtonClick(object sender, CGL_Button_Event e)
        {
            // create a switch message that pop that scene_history
            Message_GoBack mgb = new Message_GoBack();
            InputManager.AddInputItem(mgb);
        }

        private CGL_Label _title_label;
        private CGL_Button _back_button;
        
        // NOTE(duan): testing out state
        private CGL_Button _jump_button;
        private CGL_Button _attack_button;
        private CGL_Button _die_button;
        private CGL_Button _run_button;
        private CGL_Button _crouch_button;

        private CGL_Button _walk_right_button;
        private CGL_Button _walk_left_button;
                       
        private CGL_Label _helper_label;
        
        private GenericPlayer RockPerson;
    }
}
