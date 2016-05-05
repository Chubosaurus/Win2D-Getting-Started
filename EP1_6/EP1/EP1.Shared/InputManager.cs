using System;
using System.Collections.Generic;
using System.Text;

namespace Chubosaurus
{

    #region ----------[These are GenericInput Types, you should create your own in another class/file.]
    public class GenericInput
    {
        public GenericInput(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }
        public string TargetScene { get; set; }
        public string TargetItem { get; set; }
    }
    #endregion

    /// <summary>
    /// A Manager that handles all inputs.
    /// </summary>
    public static class InputManager
    {
        // our locking object to make it thread safe
        private static object input_queue_lock = new object();

        /// <summary>
        /// Returns the current GenericInput for processing.
        /// </summary>
        /// <returns>A GenericInput that needs to be process, null if the InputQueque is empty.</returns>
        public static GenericInput Update()
        {
            lock (input_queue_lock)
            {
                if (InputQueue.Count == 0)
                    return null;
                else
                    return InputQueue.Dequeue();                
            }
        }

        /// <summary>
        /// Add a GenericInput into the InputQueque.
        /// </summary>
        /// <param name="gi">A GenericInput to add to the Queque.</param>
        /// <returns>Returns true if the GenericInput has been inserted successfully, else false.</returns>
        public static bool AddInputItem(GenericInput gi)
        {
            lock (input_queue_lock)
            {
                int count_before_add = InputQueue.Count;
                InputQueue.Enqueue(gi);

                int count_after_add = InputQueue.Count;

                if (count_after_add > count_before_add)
                    return true;
                else
                    return false;
            }
        }

        private static Queue<GenericInput> InputQueue = new Queue<GenericInput>();          //our queque
    }
}
