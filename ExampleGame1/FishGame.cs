using WackyFishcakeToucher.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace WackyFishcakeToucher
{
    public class FishGame : Game
    {
        // Texture2D ball;
        // Texture2D ballcake2;
        // Texture2D player2;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Entity> entityList;
        private SpriteFont font;
        private int score = 0;
        UpdateContext updateContext;
        Song song;

        Dictionary<string, Texture2D> textureMap;
        List<SoundEffect> soundEffects;
        List<Message> messages;

        Player playerCharacter;

        private const double _delay = 1;
        private double _remainingDelay = _delay;

        float screenWidth;
        float screenHeight;

        public FishGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        private Random random = new Random();

        private void AddFish(Texture2D texture, double timer)
        {
            _remainingDelay -= timer;

            if (_remainingDelay <= 0)
            {
                Entity fish = new Fish(new Vector2(100, 20))
                {
                    image = texture,

                    position = new Vector2((float)random.NextDouble() * screenWidth, (float)random.NextDouble() * screenHeight),

                    size = new Vector2(100, 100)
                };

                entityList.Add(fish);
                score++;
                _remainingDelay = _delay;
            }
        }

        protected override void Initialize()
        {
            Window.Title = "Wacky Fishcake Toucher";

            screenWidth = (float)graphics.PreferredBackBufferWidth;
            screenHeight = (float)graphics.PreferredBackBufferHeight;

            graphics.GraphicsDevice.BlendState = BlendState.AlphaBlend;

            // most of the content in these objects (ideally)
            entityList = new List<Entity>();
            updateContext = new UpdateContext();
            messages = new List<Message>();
            soundEffects = new List<SoundEffect>();
            textureMap = new Dictionary<string, Texture2D>(ContentFactory.TextureFactory(graphics));

            playerCharacter = new Player
            {
                position = new Vector2(screenWidth / 2, screenHeight / 2)
            };

            entityList.Add(playerCharacter);

            messages.Add(new Message()
            {
                Text = "FISHES TO TOUCH: ",
                Appeared = TimeSpan.MaxValue,
                Position = new Vector2(100, 100)
            });

            messages.Add(new Message()
            {
                Text = "FISHES TOUCHED: ",
                Appeared = TimeSpan.MaxValue,
                Position = new Vector2(200, 200)
            });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Textures and Fonts
            // ball = ContentFactory.LoadTexture(@"..\..\..\..\Content\ball.png", graphics);
            // ballcake2 = ContentFactory.LoadTexture(@"..\..\..\..\Content\ballcake2.png", graphics);
            font = Content.Load<SpriteFont>("Score");
            // player2 = ContentFactory.LoadTexture(@"..\..\..\..\Content\player2.png", graphics);

            // Sound
            this.song = Content.Load<Song>("Terexi");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            // soundEffects.Add(Content.Load<SoundEffect>("orzo"));
            soundEffects.Add(ContentFactory.LoadSound(@"..\..\..\..\Content\orzo.wav", graphics));

            // TODO: if (_isFirstUpdate){  DoShit(); _isFirstUpdate = false; }
            entityList[0].image = textureMap["player2.png"];
        }

        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume -= 0.1f;
            MediaPlayer.Play(song);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;

            AddFish(textureMap["ballcake2.png"], timer);

            updateContext.keyboardState = Keyboard.GetState();
            updateContext.entityList = entityList;
            updateContext.soundEffects = soundEffects;
            updateContext.screenWidth = (float)graphics.PreferredBackBufferWidth;
            updateContext.screenHeight = (float)graphics.PreferredBackBufferHeight;
            updateContext.fishesUntouched = score;

            foreach (Entity entity in entityList.ToArray())
            {
                entity.Update(gameTime, updateContext);
            }

            // entityList = updateContext.entityList;
            score = updateContext.fishesUntouched;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            spriteBatch.Begin();

            foreach (Entity entity in entityList)
            {
                // Rectangle sourceRectangle = new Rectangle(50, 50, 150, 150);
                Rectangle destinationRectangle = new Rectangle((int)entity.position.X, (int)entity.position.Y, (int)entity.size.X, (int)entity.size.Y);
                spriteBatch.Draw(entity.image, destinationRectangle, null, Color.White);
            }

            spriteBatch.DrawString(font, messages[0].Text + score, messages[0].Position, Color.Black);
            spriteBatch.DrawString(font, messages[1].Text + updateContext.fishesTouched, messages[1].Position, Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
