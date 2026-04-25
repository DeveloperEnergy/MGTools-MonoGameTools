using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace MGTool.Main
{
    public static class MGT_Loader
    {
        //Main

        private static float DeltaTime;
        private static KeyboardState CurrentKey;
        private static KeyboardState PreviousKey;
        private static MouseState CurrentMS;
        private static MouseState PreviousMS;

        //System
        private static GraphicsDevice graphicsDevice = null!;
        private static Viewport viewport;
        private static GraphicsDeviceManager graphicsDeviceManager = null!;
        private static GameWindow Window = null!;
        private static SpriteBatch spriteBatch = null!;

        //Contents
        private static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, Texture2D> Backgrounds = new Dictionary<string, Texture2D>();
        private static Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();
        private static Dictionary<string, SoundEffect> Sounds = new Dictionary<string, SoundEffect>();
        private static Dictionary<string, Song> Musics = new Dictionary<string, Song>();


        //Set

        //Sytems
        public static void SetSystems(GraphicsDevice _graphicsDevice, Viewport _viewport, GraphicsDeviceManager _graphicsDeviceManager, GameWindow _Window)
        {
            graphicsDevice = _graphicsDevice;
            viewport = _viewport;
            graphicsDeviceManager = _graphicsDeviceManager;
            Window = _Window;
        }

        public static void SetGraphicsDevice(GraphicsDevice _graphicsDevice)
        {
            graphicsDevice = _graphicsDevice;
        }

        public static void SetViewport(Viewport _viewport)
        {
            viewport = _viewport;
        }

        public static void SetGraphicsDeviceManager(GraphicsDeviceManager _graphicsDeviceManager)
        {
            graphicsDeviceManager = _graphicsDeviceManager;
        }

        public static void SetGameWindow(GameWindow _Window)
        {
            Window = _Window;
        }

        public static void SetSpriteBatch(SpriteBatch _spriteBatch)
        {
            spriteBatch = _spriteBatch;

            Textures.Clear();
            Backgrounds.Clear();
            Fonts.Clear();
            Sounds.Clear();
            Musics.Clear();

        }

        //Get

        public static float GetDeltaTime()
        {
            return DeltaTime;
        }

        public static KeyboardState GetCurrentKeyboardState()
        {
            return CurrentKey;
        }

        public static KeyboardState GetPreviousKeyboardState()
        {
            return PreviousKey;
        }

        public static MouseState GetCurrentMouseState()
        {
            return CurrentMS;
        }

        public static MouseState GetPreviousMouseState()
        {
            return PreviousMS;
        }
        public static GraphicsDevice GetGraphicsDevice()
        {
            return graphicsDevice;
        }

        public static Viewport GetViewport()
        {
            return viewport;
        }

        public static GraphicsDeviceManager GetGraphicsDeviceManager()
        {
            return graphicsDeviceManager;
        }

        public static GameWindow GetGameWindow()
        {
            return Window;
        }

        public static SpriteBatch GetSpriteBatch()
        {
            return spriteBatch;
        }

        //Others

        public static void MGT_Update(GameTime gameTime)
        {
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateViewport();

            PreviousMS = CurrentMS;
            PreviousKey = CurrentKey;

            CurrentMS = Mouse.GetState();
            CurrentKey = Keyboard.GetState();
        }

        //Contents

        public static void AddNew_Texture(string _ID, Texture2D _Texture)
        {
            if (!Textures.ContainsKey(_ID))
                Textures.Add(_ID, _Texture);
        }

        public static void AddNew_Background(string _ID, Texture2D _Texture)
        {
            if (!Backgrounds.ContainsKey(_ID))
                Backgrounds.Add(_ID, _Texture);
        }

        public static void AddNew_Fonts(string _ID, SpriteFont _Fonts)
        {
            if (!Fonts.ContainsKey(_ID))
                Fonts.Add(_ID, _Fonts);
        }

        public static void AddNew_Sound(string _ID, SoundEffect _Sound)
        {
            if (!Sounds.ContainsKey(_ID))
                Sounds.Add(_ID, _Sound);
        }

        public static void AddNew_Music(string _ID, Song _Musics)
        {
            if (!Musics.ContainsKey(_ID))
                Musics.Add(_ID, _Musics);
        }

        public static Viewport UpdateViewport()
        {
            return viewport = graphicsDevice.Viewport;
        }

        //Get

        public static Texture2D GetTexture(string ID)
        {
            if(Textures.TryGetValue(ID, out Texture2D? result))
            {
                return result;
            }
            else
            {
                return null!;
            }
        }

        public static Texture2D GetBackground(string ID)
        {
            if (Backgrounds.TryGetValue(ID, out Texture2D? result))
            {
                return result;
            }
            else
            {
                return null!;
            }
        }

        public static SpriteFont GetFont(string ID)
        {
            if (Fonts.TryGetValue(ID, out SpriteFont? result))
            {
                return result;
            }
            else
            {
                return null!;
            }
        }

        public static SoundEffect GetSound(string ID)
        {
            if (Sounds.TryGetValue(ID, out SoundEffect? result))
            {
                return result;
            }
            else
            {
                return null!;
            }
        }

        public static Song GetMusic(string ID)
        {
            if (Musics.TryGetValue(ID, out Song? result))
            {
                return result;
            }
            else
            {
                return null!;
            }
        }

    }
}
