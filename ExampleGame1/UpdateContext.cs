using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WackyFishcakeToucher
{
    class UpdateContext
    {
        public KeyboardState keyboardState;
        public List<Entity> entityList;
        public int fishesUntouched;
        public int fishesTouched;
        public float screenWidth;
        public float screenHeight;
        public List<SoundEffect> soundEffects;
    }
}