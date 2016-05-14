using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Graphics.Canvas;
using System.Numerics;
using Windows.Foundation;

namespace Chubosaurus
{
    /// <summary>
    /// A GenericItem with States.
    /// </summary>
    public class GenericStateObject : GenericItem
    {
        public GenericStateObject(string name = "Generic State Object")
            : base(name)
        {
            if (_animations == null)
            {
                _animations = new Dictionary<GENERIC_STATE, GenericSpriteAnimation>();
            }
        }

        /// <summary>
        /// Updates the GenericStateObject, this means it just updates the animation that is loaded into its current state.
        /// </summary>
        /// <param name="dt">The delta time.</param>
        /// <param name="input">The input item if any.</param>
        public override void Update(TimeSpan dt, GenericInput input)
        {

            base.Update(dt, input);
            // NOTE(duan): for debugging we need to check, on release don't have to
#if DEBUG
            if (_animations.ContainsKey(this._state))
#endif
            {
                _animations[this._state].Update(dt, input);
            }
        }

        /// <summary>
        /// Draw the current State's animation.
        /// </summary>
        /// <param name="cds">The drawing surface.</param>
        public override void Draw(CanvasDrawingSession cds)
        {
            base.Draw(cds);

            // draw the animation in the state
            // NOTE(duan): for debugging we need to check, on release don't have to
#if DEBUG
            if (_animations.ContainsKey(this._state))
#endif
            {
                _animations[this._state].Location = this.Location;                
                _animations[this._state].Draw(cds);
            }
        }

        /// <summary>
        /// Load the animation into a specific GENERIC_STATE.
        /// </summary>
        /// <param name="gs">The GENERIC_STATE that the animation should be loaded to.</param>
        /// <param name="bitmap_ids">The list of bitmap_id(s) that make up the animation.</param>
        /// <param name="width">The width of the animation object.</param>
        /// <param name="height">The height of the animation object.</param>
        /// <returns>Returns the GenericSpriteAnimation that was loaded on success, else null.</returns>
        public GenericSpriteAnimation LoadStateAnimation(GENERIC_STATE gs, List<string> bitmap_ids, int width, int height)
        {
            try
            {
                // create the animation
                GenericSpriteAnimation gsa = new GenericSpriteAnimation();

                // load in each frame
                foreach (string bitmap in bitmap_ids)
                {
                    gsa.LoadFrameFromBitmap(bitmap, width, height);
                }

                gsa.IsPlaying = true;

                // add it to the library
                _animations[gs] = gsa;


                return gsa;
            }
            catch (Exception ex)
            {
                string error_message = ex.Message;
            }

            return null;
        }

        /// <summary>
        /// Resets the animation properties.
        /// </summary>
        /// <param name="gs">The GENERIC_STATE's animation to reset.</param>
        public void ResetAnimation(GENERIC_STATE gs)
        {
            // checks
            if (_animations.ContainsKey(gs))
            {
                _animations[gs].LoopCount = 0;
                _animations[gs].CurrentFrame = 0;
            }
        }

        /// <summary>
        /// Returns whether the GENERIC_STATE's animation is completed or not.
        /// </summary>
        /// <param name="gs">The GENERIC_STATE's animation to check.</param>
        /// <returns>Returns true if all frames are played, else false.</returns>
        public bool IsAnimationCompleted(GENERIC_STATE gs)
        {
            // checks
            if (_animations.ContainsKey(gs))
            {
                return (_animations[gs].LoopCount == 1);
            }

            return true;
        }

        private Dictionary<GENERIC_STATE, GenericSpriteAnimation> _animations;

        #region [Properties --------------------------------------------------]

        private GENERIC_STATE _state;
        public GENERIC_STATE State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    // TODO(duan): Implement INofity here
                }
            }
        }

        #endregion
    }

    // NOTE(duan): for now lets put the GENERIC_STATE enum here
    public enum GENERIC_STATE { IDLE, WALK, RUN, ATTACK, CROUCH, DYING, JUMP }

}
