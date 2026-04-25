using MGTool.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Create;

namespace MGTool.Create
{
    public class MGT_Rectangle
    {
        //Main

        private Rectangle Rectangle;
        private Texture2D Texture = null!;
        private int Width, Height, Margin;
        private float Alpha = 1f, Rotation = 0f, PositionX, PositionY;
        private Vector2 Origin, Scale;
        private Color Color;
        private MGT_Pixel pixel = new MGT_Pixel();
        private bool IsHovered;

        public enum AnchorX { Left, Right, Center, None }
        public enum AnchorY { Up, Down, Center, None }

        private AnchorX anchorX = AnchorX.None;
        private AnchorY anchorY = AnchorY.None;

        //New

        public void New(int _Width, int _Height, Vector2 _Position, Color _Color, float _Alpha = 1f)
        {
            Scale = Vector2.One;

            SetSize(_Width, _Height);
            SetPosition(new Vector2(_Position.X, _Position.Y));
            SetColor(_Color);
            SetAlpha(_Alpha);

            pixel.New();
        }

        //Set

        public void SetAlignX(AnchorX _anchorX)
        {
            anchorX = _anchorX;
        }

        public void SetAlignY(AnchorY _anchorY)
        {
            anchorY = _anchorY;
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

        public int SetMargin(int _Margin)
        {
            return Margin = _Margin;
        }

        //Get

        public Rectangle GetRectangle()
        {
            return Rectangle;
        }

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
            PasteOnAnchorX();
            PasteOnAnchorY();

            return new Vector2(PositionX, PositionY);
        }

        public float GetPositionX()
        {
            PasteOnAnchorX();

            return PositionX;
        }

        public float GetPositionY()
        {
            PasteOnAnchorY();

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

        public int GetMargin()
        {
            return Margin;
        }

        public bool GetHover()
        {
            return IsHovered;
        }

        //Others

        public void DetectHover()
        {
            if (Rectangle.Contains(MGT_Loader.GetCurrentMouseState().X, MGT_Loader.GetCurrentMouseState().Y))
            {
                IsHovered = true;
            }
            else
            {
                IsHovered = false;
            }
        }

        private float PasteOnAnchorX()
        {
            float positionAnchorX = PositionX;

            switch (anchorX)
            {

                case AnchorX.None:
                    break;
                case AnchorX.Left:
                    PositionX = 0 + Margin;
                    break;
                case AnchorX.Right:
                    PositionX = MGT_Loader.GetViewport().Width - ((int)(Width * Scale.X)) - Margin;
                    break;
                case AnchorX.Center:
                    PositionX = (MGT_Loader.GetViewport().Width / 2) - ((int)(Width * Scale.X) / 2);
                    break;
            }

            return PositionX;
        }

        private float PasteOnAnchorY()
        {
            float positionAnchorX = PositionX;

            switch (anchorY)
            {
                case AnchorY.None:
                    break;
                case AnchorY.Up:
                    PositionY = 0 + Margin;
                    break;
                case AnchorY.Down:
                    PositionY = MGT_Loader.GetViewport().Height - ((int)(Height * Scale.Y)) - Margin;
                    break;
                case AnchorY.Center:
                    PositionY = MGT_Loader.GetViewport().Height / 2f - ((int)(Height * Scale.Y) / 2);
                    break;
            }

            return PositionY;
        }

        //Draw

        public void Draw()
        {

            Origin = (Texture == null) ? new Vector2(0.5f, 0.5f) : new Vector2(Texture.Width / 2f, Texture.Height / 2f);

            PasteOnAnchorX();
            PasteOnAnchorY();

            int finalWidth = (int)(Width * Scale.X);
            int finalHeight = (int)(Height * Scale.Y);

            Rectangle = new Rectangle(
                (int)PositionX,
                (int)PositionY,
                finalWidth,
                finalHeight
            );

            Texture2D currentTexture = (Texture == null) ? pixel.GetPixel() : Texture;

            MGT_Loader.GetSpriteBatch().Draw(currentTexture, new Rectangle(Rectangle.X + (finalWidth / 2), Rectangle.Y + (finalHeight / 2), finalWidth, finalHeight), null, Color * Alpha, Rotation, Origin, SpriteEffects.None, 0f);
        }
    }
}