using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WackyFishcakeToucher
{
    class Message
    {
        public string Text { get; set; }
        public TimeSpan Appeared { get; set; }
        public Vector2 Position { get; set; }
        public Boolean IsActive { get; set; }
    }
}