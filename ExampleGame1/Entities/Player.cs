using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WackyFishcakeToucher.Entities
{
    class Player : Entity
    {
        public Player()
        {
            this.name = "player";
            this.moveSpeed = 300f;
            this.size = new Vector2(100, 90);
            this.layer = 1;
        }

        public override void Update(GameTime gameTime, UpdateContext updateContext)
        {
            foreach (Entity entity in updateContext.entityList.ToArray()) {
                if ("fish".Equals(entity.name))
                {
                    if (this.position.X < entity.position.X + entity.size.X &&
                        this.position.X + this.size.X > entity.position.X &&
                        this.position.Y < entity.position.Y + entity.size.Y &&
                        this.position.Y + this.size.Y > entity.position.Y)
                    {
                        updateContext.entityList.Remove(entity);
                        updateContext.fishesTouched++;
                        updateContext.fishesUntouched--;

                        var instance = updateContext.soundEffects[0].CreateInstance();
                        instance.IsLooped = false;
                        instance.Pitch = 0.5f;
                        instance.Play();
                    }
                }
            }

            if (updateContext.keyboardState.IsKeyDown(Keys.Up))
                this.position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (updateContext.keyboardState.IsKeyDown(Keys.Down))
                this.position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (updateContext.keyboardState.IsKeyDown(Keys.Left))
                this.position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (updateContext.keyboardState.IsKeyDown(Keys.Right))
                this.position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.position.X = Math.Min(Math.Max(0, this.position.X), updateContext.screenWidth - this.size.X);
            this.position.Y = Math.Min(Math.Max(0, this.position.Y), updateContext.screenHeight - this.size.Y);
        }
    }
}
