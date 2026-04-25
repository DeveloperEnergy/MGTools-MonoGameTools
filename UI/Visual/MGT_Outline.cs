using MGTool.Create;
using MGTool.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Create;
using System;


namespace MGTool.UI
{
    public class MGT_Outline
    {
        // Main

        private int Thickness, Margin, Width, Height;
        private float PositionX, PositionY, Alpha, Rotation;
        private Vector2 Origin;
        private Color Color;
        private MGT_Pixel pixel = new MGT_Pixel();

        public enum AnchorX { Left, Right, Center, None }
        public enum AnchorY { Up, Down, Center, None }

        private AnchorX anchorX = AnchorX.None;
        private AnchorY anchorY = AnchorY.None;

        //New

        public void New(int _Width, int _Height, Vector2 _Position, int _Thickness, Color _Color, float _Alpha = 1f)
        {
            SetSize(_Width, _Height);
            SetPosition(new Vector2(_Position.X, _Position.Y));
            SetColor(_Color);
            SetAlpha(_Alpha);
            SetThickness(_Thickness);

            pixel.New();
        }

        // Set

        public void SetAlignX(AnchorX _anchorX)
        {
            anchorX = _anchorX;
        }

        public void SetAlignY(AnchorY _anchorY)
        {
            anchorY = _anchorY;
        }

        public Vector2 SetPosition(Vector2 _Position)
        {
            PositionX = _Position.X;
            PositionY = _Position.Y;

            return _Position;
        }

        public float SetPositionX(float _PositionX)
        {
            PositionX = _PositionX;

            return PositionX;
        }

        public float SetPositionY(float _PositionY)
        {
            PositionY = _PositionY;

            return PositionY;
        }

        public void SetSize(int _Width, int _Height)
        {
            Width = _Width;
            Height = _Height;
        }

        public float SetRotation(float _Rotation)
        {
            Rotation = _Rotation;

            return Rotation;
        }

        public int SetThickness(int _Thickness)
        {
            Thickness = _Thickness;

            return Thickness;
        }

        public Color SetColor(Color _Color)
        {
            Color = _Color;

            return Color;
        }

        public float SetAlpha(float _Alpha)
        {
            Alpha = _Alpha;

            return Alpha;
        }

        public int SetMargin(int _Margin)
        {
            Margin = _Margin;

            return Margin;
        }

        // Get

        public float GetRotation()
        {
            return Rotation;
        }

        public float GetAlpha()
        {
            return Alpha;
        }

        public int GetWidth()
        {
            return Width;
        }

        public int GetHeight()
        {
            return Height;
        }

        public int GetMargin()
        {
            return Margin;
        }

        public int GetThickness()
        {
            return Thickness;
        }

        public Color GetColor()
        {
            return Color;
        }

        public Vector2 GetPosition()
        {
            PasteOnAnchorX();
            PasteOnAnchorY();

            return new Vector2(PositionX, PositionY);
        }

        // Other

        public void ConnectRectangle(object _Object)
        {
            if (_Object is MGT_Rectangle _MGT_Rectangle)
            {
                Width = _MGT_Rectangle.GetWidth();
                Height = _MGT_Rectangle.GetHeight();
                SetPosition(_MGT_Rectangle.GetPosition());
            }
            else if(_Object is MGT_Button _MGT_Button)
            {
                Width = _MGT_Button.GetWidth();
                Height = _MGT_Button.GetHeight();
                SetPosition(_MGT_Button.GetPosition());
            }
            else throw new ArgumentException("MGT_Outline метод ConnectRectangle(ТИП): Данный тип не поддерживается!");
        }

        private float PasteOnAnchorX()
        {
            switch (anchorX)
            {
                case AnchorX.Left:
                    PositionX = 0 + Margin;
                    break;

                case AnchorX.Right:
                    PositionX = MGT_Loader.GetViewport().Width - Width - Margin;
                    break;

                case AnchorX.Center:
                    PositionX = (MGT_Loader.GetViewport().Width / 2) - (Width / 2);
                    break;
            }

            return PositionX;
        }

        private float PasteOnAnchorY()
        {
            switch (anchorY)
            {
                case AnchorY.Up:
                    PositionY = 0 + Margin;
                    break;

                case AnchorY.Down:
                    PositionY = MGT_Loader.GetViewport().Height - Height - Margin;
                    break;

                case AnchorY.Center:
                    PositionY = (MGT_Loader.GetViewport().Height / 2) - (Height / 2);
                    break;
            }

            return PositionY;
        }

        // Draw

        public void Draw()
        {
            PasteOnAnchorX();
            PasteOnAnchorY();

            Color finalColor = Color * Alpha;

            Origin = new Vector2(Width / 2f, Height / 2f);

            Vector2 drawPos = new Vector2(PositionX + Origin.X, PositionY + Origin.Y);

            MGT_Loader.GetSpriteBatch().Draw(pixel.GetPixel(), drawPos, new Rectangle(0, 0, Width, Thickness), finalColor, Rotation, Origin, 1f, SpriteEffects.None, 0f);
            MGT_Loader.GetSpriteBatch().Draw(pixel.GetPixel(), drawPos, new Rectangle(0, 0, Width, Thickness), finalColor, Rotation, new Vector2(Origin.X, Origin.Y - Height + Thickness), 1f, SpriteEffects.None, 0f);
            MGT_Loader.GetSpriteBatch().Draw(pixel.GetPixel(), drawPos, new Rectangle(0, 0, Thickness, Height), finalColor, Rotation, Origin, 1f, SpriteEffects.None, 0f);
            MGT_Loader.GetSpriteBatch().Draw(pixel.GetPixel(), drawPos, new Rectangle(0, 0, Thickness, Height), finalColor, Rotation, new Vector2(Origin.X - Width + Thickness, Origin.Y), 1f, SpriteEffects.None, 0f);
        }
    }
}