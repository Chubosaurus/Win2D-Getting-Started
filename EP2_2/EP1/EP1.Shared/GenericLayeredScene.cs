using System;
using System.Collections.Generic;
using System.Text;

using System.Numerics;
using Microsoft.Graphics.Canvas;

namespace Chubosaurus
{
    /// <summary>
    /// A GenericLayeredScene, all objects in the scene graph will respect the ordering
    /// based off the object's ZIndex value.
    /// </summary>
    public class GenericLayeredScene : GenericScene 
    {
        public GenericLayeredScene()
            : base("Generic Layered Scene")
        {
        }

        public GenericLayeredScene(string name = "")
            : base(name)
        {
        }

        protected virtual void SetupScene(CanvasControl canvas = null)
        {
            if (canvas == null)
            {
                base.SetupScene();
            }
        }

        /// <summary>
        /// Add an Generic Item to the scene, sorting it based on the ZIndex of the object
        /// </summary>
        /// <param name="gi">The GenericItem to add.</param>
        /// <returns>Returns true on success, else false.</returns>
        public override bool AddObject(GenericItem gi)
        {
            bool ret = base.AddObject(gi);

            if (ret)
            {
                // sort the item based on the z-index
                this.objects.Sort((x, y) => y.ZIndex.CompareTo(x.ZIndex));
            }

            return ret;
        }
    }
}
