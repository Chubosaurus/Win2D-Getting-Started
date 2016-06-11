using System;
using System.Collections.Generic;
using System.Text;

namespace Chubosaurus
{
    public class Message_Translate : GenericInput
    {
        public Message_Translate()
            : base("TRANSLATE_X")
        {
            this.VelocityFactor = 1.0f;
        }

        private float _velocity_factor;
        public float VelocityFactor
        {
            get { return _velocity_factor; }
            set
            {
                if (_velocity_factor != value)
                {
                    _velocity_factor = value;
                }
            }
        }
    }
    
    // NOTE(duan): should be in another file
    // NOTE(duan): this is so ghetto we need to use a flyweight
    public class Message_StopTranslate : GenericInput
    {
        public Message_StopTranslate()
            : base("TRANSLATE_X")
        {
            this.VelocityFactor = 1.0f;
        }

        private float _velocity_factor;
        public float VelocityFactor
        {
            get { return _velocity_factor; }
            set
            {
                if (_velocity_factor != value)
                {
                    _velocity_factor = value;
                }
            }
        }
    }
}
