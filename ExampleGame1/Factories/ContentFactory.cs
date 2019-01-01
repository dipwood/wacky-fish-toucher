using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WackyFishcakeToucher
{
    class ContentFactory
    {
        private const string path = @"..\..\..\..\Content\textures";
        public static Dictionary<string, Texture2D> TextureFactory(GraphicsDeviceManager graphics)
        {
            Dictionary<string, Texture2D> textureMap = new Dictionary<string, Texture2D>();
            FileInfo[] texturesFromDirectory = new DirectoryInfo(path).GetFiles();

            foreach (FileInfo texture in texturesFromDirectory)
            {
                textureMap.Add(texture.Name, LoadTexture2(texture, graphics));
            }

            return textureMap;
        }

        public static Texture2D LoadTexture2(FileInfo file, GraphicsDeviceManager graphics)
        {
            return Texture2D.FromStream(graphics.GraphicsDevice, file.OpenRead());
        }

        public static Texture2D LoadTexture(string fileName, GraphicsDeviceManager graphics)
        {
            using (FileStream s = new FileStream(fileName, FileMode.Open))
            {
                return Texture2D.FromStream(graphics.GraphicsDevice, s);
            }
        }

        public static SoundEffect LoadSound(string filename, GraphicsDeviceManager graphics)
        {
            using (FileStream f = new FileStream(filename, FileMode.Open))
            {
                return SoundEffect.FromStream(f);
            }
        }
    }
}
