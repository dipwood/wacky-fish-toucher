using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WackyFishcakeToucher
{
    abstract class Entity
    {
        public Vector2 position;
        public Vector2 size;
        public Texture2D image;
        public String name;
        public float moveSpeed;
        public int layer;

        public abstract void Update(GameTime gameTime, UpdateContext updateVo);
    }
}
