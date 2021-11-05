using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownGame
{
    /// <summary>
    /// sets up Game1 by inheriting from its parent preset "Game" class
    /// Also loads up some defaults for spritebatch, soundplayer and spritefont that will be used everywhere else
    /// Also will call mainmenu
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private static Game1 _newGame;
        private SpriteBatch _spriteBatch;
        private MainMenu _mainMenu;
        private SoundPlayer _soundPlayer;
        private SpriteFont _spriteFont;

        /// <summary>
        /// Load graphicdevicemaager. Sets game screen height and width, sets root directory of content to be content folder (to save time and folder looking). Then made sure mousr was visible
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            //to set height and width of screen
            _graphics.PreferredBackBufferHeight = Global.screenHeight;
            _graphics.PreferredBackBufferWidth = Global.screenWidth;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        /// <summary>
        /// let itself get initilize using its parent inbuilt "Game' class
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// loads a spritebatch, soundplayer, spritefont that will be used later on. Also loads all mainmenu object by calling that class
        /// (note: this part can be updated by using gamestate design + a dictionary instead)
        /// </summary>
        protected override void LoadContent()
        {            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _soundPlayer = new SoundPlayer(this);
            _mainMenu = new MainMenu(this);
            _spriteFont = this.Content.Load<SpriteFont>("pic\\Arial16");
        }

        /// <summary>
        /// update itself and game
        /// </summary>
        /// <param name="gameTime">pass the gametime to help mainmenu update itself</param>
        protected override void Update(GameTime gameTime)
        {            
            _mainMenu.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Clear screen and Set background color as black, draw all sprites in spritebatch using begin and end
        /// (note here just calling the mainmenu's things)
        /// </summary>
        /// <param name="gameTime"> to let game draw itself using gametime and its parent class</param>
        /// ref that helped me for spritebatch:https://csharp.hotexamples.com/examples/Microsoft.Xna.Framework.Graphics/SpriteBatch/Begin/php-spritebatch-begin-method-examples.html
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //open spritebatch

            //used sortmode deferred to ensure it doesnt change the order at which sprites are being drawn
            //reference: https://stackoverflow.com/questions/16258200/spritesortmode-texture-sometimes-draw-order-is-wrong
           //used alphablend here mainly cause it sounded cool. it basically blends the colour using alpha chaneela nd a certian equation given in the website link below
            //reference: http://glasnost.itcarlow.ie/~powerk/technology/xna/blending/blending.html
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            //stuff that will be drawn
            _mainMenu.Draw();

            //close sprite batch
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Property to let me access spritebatch from where ever needed
        /// </summary>
        public SpriteBatch SpriteBatch 
        {
            get 
            { 
                return _spriteBatch; 
            } 
        }

        /// <summary>
        /// Property to let me access SoundPlayer from where ever needed
        /// </summary>
        public SoundPlayer SoundPlayer 
        { 
            get 
            { 
                return _soundPlayer; 
            } 
        }

        /// <summary>
        /// Property to let me access SpriteFont from where ever needed
        /// </summary>
        public SpriteFont SpriteFont 
        { 
            get 
            { 
                return _spriteFont; 
            } 
        }

        /// <summary>
        /// used to make the singleton class
        /// </summary>
        /// <returns>instance of Game1</returns>
        public static Game1 GetInstance()
        {
            if (_newGame == null)
            {
                _newGame = new Game1();
            }
            return _newGame;
        }
    }
}
