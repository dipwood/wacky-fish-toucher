using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace WackyFishcakeToucher.Entities
{
    class Fish : Entity
    {

        public Vector2 velocity;
        private const double _delay = 1;
        private double _remainingDelay = _delay;

        public Fish(Vector2 velocityInput)
        {
            this.name = "fish";
            this.layer = 0;
            velocity = velocityInput;
        }

        public override void Update(GameTime gameTime, UpdateContext updateContext)
        {
            this.position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _remainingDelay -= timer;

            if (_remainingDelay <= 0)
            {
                Fish newFish = new Fish(new Vector2(130, 330))
                {
                    position = position,
                    image = image,
                    size = size / 2f
                };

                updateContext.entityList.Add(newFish);
                updateContext.fishesUntouched++;
                _remainingDelay = _delay;
            }

            if (this.position.X > updateContext.screenWidth)
            {
                updateContext.entityList.Remove(this);
                updateContext.fishesUntouched--;
            }
        }
    }
}
