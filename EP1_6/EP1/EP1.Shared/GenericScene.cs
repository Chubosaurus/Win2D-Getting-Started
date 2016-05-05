using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.Graphics.Canvas;

namespace Chubosaurus
{
    /// <summary>
    /// A GenericScene (Generally a collection of GenericIems)
    /// </summary>
    public class GenericScene : GenericItem
    {
        /// <summary>
        /// Create a GenericScene.
        /// </summary>
        /// <param name="name">The name of the scene if any.</param>
        public GenericScene(string name) : base(name)
        {
        }

        /// <summary>
        /// Update the scene.
        /// </summary>
        /// <param name="dt">A delta time since the last update.</param>
        /// <param name="gi">A GenericInput to process.</param>
        public override void Update(TimeSpan dt, GenericInput input)
        {
            // update each GenericItem inside the object list
            foreach (GenericItem gi in objects)
            {
                gi.Update(dt, input);
            }
        }

        /// <summary>
        /// Draw the scene onto a surface.
        /// </summary>
        /// <param name="cds">A surface to draw the scene on.</param>
        public override void Draw(CanvasDrawingSession cds)
        {
            // draw each GenericItem inside the object list
            foreach (GenericItem gi in objects)
            {
                gi.Draw(cds);
            }
        }

        /// <summary>
        /// Add a GenericItem into the object list.
        /// </summary>
        /// <param name="gi">A GenericItem to add.</param>
        /// <returns>Returns true if the object was successfully inserted.  Else false.</returns>
        public bool AddObject(GenericItem gi)
        {
            if (objects == null)
                return false;

            int size_before_add = objects.Count;
            objects.Add(gi);
            int size_after_add = objects.Count;

            if (size_after_add > size_before_add)
                return true;

            return false;
        }

        /// <summary>
        /// Remove a GenericItem from the object list.
        /// </summary>
        /// <param name="gi">A GenericItem to remove.</param>
        /// <returns>Returns true if the object was successfully removed.  Else false.</returns>
        public bool RemoveObject(GenericItem gi)
        {
            return objects.Remove(gi);
        }

        /// <summary>
        /// Remove a GenericItem from the object list.
        /// </summary>
        /// <param name="index">The index of the GenericItem to remove.</param>
        /// <returns>Returns true if the object was successfully removed.  Else false.</returns>
        public bool RemoveObject(int index)
        {
            int size_before_rem = objects.Count;
            objects.RemoveAt(index);
            int size_after_rem = objects.Count;

            if (size_after_rem < size_before_rem)
                return true;

            return false;
        }

        // Properties
        protected List<GenericItem> objects = new List<GenericItem>();
    }
}
