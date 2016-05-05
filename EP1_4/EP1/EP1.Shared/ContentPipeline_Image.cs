using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Graphics.Canvas;
using System.Threading.Tasks;

namespace Chubosaurus
{
    /// <summary>
    /// The ContentPipeline for all Bitmap Images.  This provides a path to access any images that have been loaded.
    /// </summary>
    public static class ContentPipeline_Image
    {
        // our image dictionary
        public static Dictionary<string, CanvasBitmap> ImageDictionary = new Dictionary<string, CanvasBitmap>();

        /// <summary>
        /// Loads a bitmap from a file and adds it to the ImageDictionary.
        /// </summary>
        /// <param name="key">A unique id to give to bitmap.</param>
        /// <param name="file_path">Path to the file to load.</param>
        /// <returns>Returns true if the image has been loaded and added to the ImageDictionary.  Else false.</returns>
        public static async Task<bool> AddImage(string key, string file_path)
        {
            // null check
            if (ParentCanvas == null)
                return false;

            // load the bitmap from file
            CanvasBitmap cb = await CanvasBitmap.LoadAsync(ParentCanvas, file_path);

            // null check
            if (cb == null)
                return false;

            // check size
            int size_before_add = ImageDictionary.Count;
            // add
            ImageDictionary.Add(key, cb);
            // check size again
            int size_after_add = ImageDictionary.Count;

            // check
            if (size_after_add > size_before_add)
                return true;

            return false;
        }

        // TODO (duan) : RemoveImage

        public static  CanvasControl ParentCanvas = null;
    }
}
