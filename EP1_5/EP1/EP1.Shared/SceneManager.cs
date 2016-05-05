using System;
using System.Collections.Generic;
using System.Text;

namespace Chubosaurus
{

    /// <summary>
    /// SceneManager class.  This class contains all the scenes inside the application.  This class also dictates what the CurrentScene is.
    /// </summary>
    public static class SceneManager
    {
        // The Dictionary (list) of scenes inside the SceneManager
        public static Dictionary<string, GenericScene> SceneDictionary = new Dictionary<string, GenericScene>();

        /// <summary>
        /// Add a scene into the SceneManger.  Applies a key for easy access.
        /// </summary>
        /// <param name="key">A unique name for the scene.</param>
        /// <param name="scene">The scene to add to the SceneDictionary.</param>
        /// <returns>Returns true on success.  False otherwise.</returns>
        public static bool AddScene(GenericScene gs)
        {
            int size_before_add = SceneDictionary.Count;
            SceneDictionary.Add(gs.Name, gs);
            int size_after_add = SceneDictionary.Count;

            if (size_after_add > size_before_add)
                return true;

            return false;
        }

        /// <summary>
        /// The CurrentScene.
        /// </summary>
        public static GenericScene CurrentScene { get; set; }

        // TODO:
        // Remove Scene
    }
}
