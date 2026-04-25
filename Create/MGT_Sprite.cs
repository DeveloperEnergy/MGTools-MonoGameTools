using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MGTool.Main;

namespace MGTool.Create
{
    public class MGT_Sprite
    {
        //Main

        private Texture2D Texture = null!;
        private int Width, Height;
        private float Alpha = 1f, Rotation = 0f, PositionX, PositionY;
        private Vector2 Origin, Scale;
        private Color Color;

        //Set

        public void NewSprite(Texture2D _Texture, int _Width, int _Height, Vector2 _Position, float _Alpha = 1f)
        {
            SetTexture(_Texture);
            SetScale(1f, 1f);
            SetSize(_Width, _Height);
            SetPosition(_Position);
            SetAlpha(_Alpha);
            SetColor(Color.White);
        }

        public Texture2D SetTexture(Texture2D _Texture)
        {
            return Texture = _Texture;
        }

        public void SetSize(int _Width, int _Height)
        {
            Width = _Width * (int)Scale.X;
            Height = _Height * (int)Scale.Y;
        }

        public int SetWidth(int _Width)
        {
            return Width = _Width * (int)Scale.X;
        }

        public int SetHeight(int _Height)
        {
            return Height = _Height * (int)Scale.Y;
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

        public Vector2 SetScale(float _ScaleX, float _ScaleY)
        {
            return Scale = new Vector2(_ScaleX, _ScaleY);
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

        public int GetWidth()
        {
            return Width;
        }

        public int GetHeight()
        {
            return Height;
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

        public Vector2 GetScale()
        {
            return Scale;
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
            if (Texture == null) return;

            Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);

            MGT_Loader.GetSpriteBatch().Draw(
                Texture,
                new Rectangle(
                    (int)PositionX + (int)(Width * Scale.X / 2),
                    (int)PositionY + (int)(Height * Scale.Y / 2),
                    (int)(Width * Scale.X),
                    (int)(Height * Scale.Y)
                ),
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
