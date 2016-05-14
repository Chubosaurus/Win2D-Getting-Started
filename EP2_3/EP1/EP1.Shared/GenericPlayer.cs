using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Graphics.Canvas;
using System.Numerics;
using Windows.Foundation;

namespace Chubosaurus
{
    /// <summary>
    /// An Example Generic Player.
    /// </summary>
    public class GenericPlayer : GenericStateObject
    {
        public GenericPlayer()
            : base("Generic Player")
        {
            this.State = GENERIC_STATE.IDLE;
        }

        /// <summary>
        /// Manual (long-hand) Example of how to handle different STATES.
        /// We will introduced game design patterns in another episodes.  
        /// </summary>
        public override void Update(TimeSpan dt, GenericInput input)
        {
            base.Update(dt, input);

            switch (this.State)
            {
                case GENERIC_STATE.JUMP:
                    // check to see if we are done jumping, if so set back to default state
                    if(this.IsAnimationCompleted(this.State))
                    {
                        this.Y += 45;
                        this.State = GENERIC_STATE.WALK;
                        this.ResetAnimation(this.State);
                        break;
                    }
                    break;
            }

            if (input is MouseGenericInput)
            {
                MouseGenericInput mgi = input as MouseGenericInput;

                switch (mgi.MouseInputType)
                {
                    case MouseGenericInputType.MouseClick:
                        {                            
                            if (this.State != GENERIC_STATE.JUMP)
                            {
                                // NOTE(duan): manually do this by hand we go over state-animation-trees
                                // in a later episode
                                this.Y -= 45;
                                this.State = GENERIC_STATE.JUMP;
                                this.ResetAnimation(this.State);
                            }
                        }
                        break;
                }
            }

        }

        // NOTE(duan) we will put this in a seperate JSON player file in the future
        // we're going to do this manually to let the viewer know how to
        public void Setup()
        {
            List<string> ani = new List<string>();
            
            // load in the walk animation
            for (int i = 1; i <= 12; i++)
            {
                ani.Add(string.Format("RD_WALK_{0}", i.ToString().PadLeft(2, '0')));
            }

            GenericSpriteAnimation gsa = this.LoadStateAnimation(GENERIC_STATE.WALK, ani, 128, 128);
            gsa.AnimationSpeed = 9;

            ani.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ani.Add(string.Format("RD_JUMP_{0}", i.ToString().PadLeft(2, '0')));
            }

            gsa = this.LoadStateAnimation(GENERIC_STATE.JUMP, ani, 128, 175);
            gsa.LoopForever = false;
            gsa.AnimationSpeed = 15;
        }
    }
}
