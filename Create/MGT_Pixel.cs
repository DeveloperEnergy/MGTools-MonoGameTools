using MGTool.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MonoGame.Create
{
    public class MGT_Pixel
    {
        //Main

        private int Width, Height;
        private Texture2D Pixel = null!;

        //New

        public void New(int _Width = 1, int _Height = 1)
        {
            SetSize(_Width, _Height);
        }

        //Set
        public void SetSize(int _Width, int _Height)
        {
            Width = _Width;
            Height = _Height;
        }

        //Get
        public Texture2D GetPixel()
        {
            Pixel = new Texture2D(MGT_Loader.GetGraphicsDevice(), Width, Height);
            Pixel.SetData(new[] { Color.White });

            return Pixel;
        }

    }
}
