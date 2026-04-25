using Microsoft.Xna.Framework.Graphics;

namespace MGTool.Main
{
    public static class MGT_GameWindow
    {
        //Private Others

        private static void ApplySettings(string _Title, int _Width, int _Height, bool _Fullscreen, bool _Borderless, bool _AllowResize, bool _AutoSize)
        {
            var Display = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            MGT_Loader.GetGameWindow().Title = _Title;
            MGT_Loader.GetGameWindow().IsBorderless = _Borderless;
            MGT_Loader.GetGameWindow().AllowUserResizing = _AllowResize;

            if (_AutoSize)
            {
                MGT_Loader.GetGraphicsDeviceManager().PreferredBackBufferWidth = Display.Width;
                MGT_Loader.GetGraphicsDeviceManager().PreferredBackBufferHeight = Display.Height;
            }
            else
            {
                MGT_Loader.GetGraphicsDeviceManager().PreferredBackBufferWidth = _Width;
                MGT_Loader.GetGraphicsDeviceManager().PreferredBackBufferHeight = _Height;
            }

            MGT_Loader.GetGraphicsDeviceManager().IsFullScreen = _Fullscreen;

            MGT_Loader.GetGraphicsDeviceManager().ApplyChanges();
            MGT_Loader.UpdateViewport();
        }

        //Others

        public static void Windowed(string _Title = "Windowed", int _Width = 800, int _Height = 600, bool _Resize = true, bool _AutoSize = false)
        {
            ApplySettings(_Title, _Width, _Height, false, false, _Resize, _AutoSize);
        }

        public static void Fullscreen(string _Title = "Full Screen", int _Width = 800, int _Height = 600, bool _Resize = true, bool _AutoSize = false)
        {
            ApplySettings(_Title, _Width, _Height, true, false, _Resize, _AutoSize);
        }

        public static void Borderless(string _Title = "Borderless fullscreen", int _Width = 800, int _Height = 600, bool _Fullscreen = false, bool _Resize = true, bool _AutoSize = false)
        {
            ApplySettings(_Title, _Width, _Height, _Fullscreen, true, _Resize, _AutoSize);
        }

        public static void Custom(string _Title = "My Game Window", int _Width = 800, int height = 600, bool _Fullscreen = false, bool _Borderless = false, bool _Resize = true, bool _AutoSize = false)
        {
            ApplySettings(_Title, _Width, height, _Fullscreen, _Borderless, _Resize, _AutoSize);
        }

        public static void SetVSync(bool _Enabled = true)
        {
            MGT_Loader.GetGraphicsDeviceManager().SynchronizeWithVerticalRetrace = _Enabled;
            MGT_Loader.GetGraphicsDeviceManager().ApplyChanges();
        }
    }
}