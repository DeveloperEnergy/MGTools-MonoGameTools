using MGTool.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Create;

namespace MGTool.UI
{
    public class MGT_Background
    {
        //Main

        private Rectangle Background;
        private Texture2D Texture = null!;
        private float Alpha = 1f, Rotation = 0f, PositionX, PositionY;
        private Vector2 Origin;
        private Color Color;
        private MGT_Pixel Pixel = new MGT_Pixel(); 

        //New

        public void New(Vector2 _Position, Color _Color, float _Alpha = 1f)
        {
            SetPosition(new Vector2(_Position.X, _Position.Y));
            SetColor(_Color);
            SetAlpha(_Alpha);

            Pixel.New();
        }

        //Set

        public Texture2D SetTexture(Texture2D _Texture)
        {
            return Texture = _Texture;
        }

        public Vector2 SetPosition(Vector2 _Position)
        {
            PositionX = _Position.X;
            PositionY = _Position.Y;

            return _Position;
        }

        public float SetPositionX(float _PositionX)
        {
            return PositionX = _PositionX;
        }

        public float SetPositionY(float _PositionY)
        {
            return PositionY = _PositionY;
        }

        public float SetAlpha(float _Alpha)
        {
            return Alpha = _Alpha;
        }

        public float SetRotation(float _Rotation)
        {
            return Rotation = _Rotation;
        }

        public Color SetColor(Color _Color)
        {
            return Color = _Color;
        }

        //Get

        public Texture2D GetTexture()
        {
            return Texture;
        }

        public Vector2 GetPosition()
        {
            return new Vector2(PositionX, PositionY);
        }

        public float GetPositionX()
        {
            return PositionX;
        }

        public float GetPositionY()
        {
            return PositionY;
        }

        public float GetAlpha()
        {
            return Alpha;
        }

        public float GetRotation()
        {
            return Rotation;
        }

        public Color GetColor()
        {
            return Color;
        }

        //Draw

        public void Draw()
        {
        
            if (Texture == null)
            {
                Origin = new Vector2(0.5f, 0.5f);
            }
            else
            {
                Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
            }

            Background = new Rectangle(
                (int)(MGT_Loader.GetViewport().Width / 2 + PositionX),
                (int)(MGT_Loader.GetViewport().Height / 2 + PositionY),
                MGT_Loader.GetViewport().Width,
                MGT_Loader.GetViewport().Height
            );

            Texture2D currentTexture = (Texture == null) ? Pixel.GetPixel() : Texture;

            MGT_Loader.GetSpriteBatch().Draw(
                currentTexture,
                Background,
                null,
                Color * Alpha,
                Rotation,
                Origin,
                SpriteEffects.None,
                0f
            );
        }
    }
}