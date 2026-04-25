using MGTool.Create;
using MGTool.Main;
using MGTool.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Create;
using System.Collections.Generic;

namespace MGTool.Mechanics
{
    public class MGT_HitBox
    {
        //Main

        private Rectangle HitBox;
        private int Width, Height;
        private float Alpha = 1f, PositionX, PositionY;
        private Vector2 Scale;
        private Color Color;
        private MGT_Pixel pixel = new MGT_Pixel();
        private MGT_Text HitBoxText = new MGT_Text();
        private string Text = null!;

        //New

        public void New(int _Width, int _Height, Vector2 _Position, Color _Color, float _Alpha = 1f)
        {
            Scale = Vector2.One;

            SetSize(_Width, _Height);
            PositionX = _Position.X;
            PositionY = _Position.Y;
            SetColor(_Color);
            SetAlpha(_Alpha);

            HitBoxText.SetText("Button");
            HitBoxText.SetColor(Color.White);

            pixel.New();
        }

        //Set

        public void SetText(SpriteFont _Font, string _Text, Color _Color)
        {
            HitBoxText.New(_Font, _Text, new Vector2((Width / 2) - (HitBoxText.GetSize().X / 2), (Height / 2) - (HitBoxText.GetSize().Y / 2)), _Color);
        }

        public SpriteFont SetTextFont(SpriteFont _Font)
        {
            return HitBoxText.SetFont(_Font);
        }

        public string SetTextValue(string _Text)
        {
            Text = _Text;
            return HitBoxText.SetText(Text);
        }

        public Color SetTextColor(Color _Color)
        {
            return HitBoxText.SetColor(_Color);
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

        public Vector2 SetScale(float _ScaleX, float _ScaleY)
        {
            return Scale = new Vector2(_ScaleX, _ScaleY);
        }

        public float SetAlpha(float _Alpha)
        {
            return Alpha = _Alpha;
        }

        public Color SetColor(Color _Color)
        {
            return Color = _Color;
        }

        //Get

        public Rectangle GetHitBoxRect()
        {
            return HitBox;
        }

        public SpriteFont GetTextFont()
        {
            return HitBoxText.GetFont();
        }

        public string GetText()
        {
            return HitBoxText.GetText();
        }

        public float GetWidth()
        {
            return Width;
        }

        public float GetHeight()
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

        public Color GetColor()
        {
            return Color;
        }

        public Rectangle GetRectAt(float _targetX, float _targetY)
        {
            return new Rectangle(
                (int)_targetX,
                (int)_targetY,
                (int)(Width * Scale.X),
                (int)(Height * Scale.Y)
            );
        }

        //Others

        public void FollowObject(Vector2 _ObjectPosition)
        {
            PositionX = _ObjectPosition.X;
            PositionY = _ObjectPosition.Y;
        }

        public bool CanMoveTo(float _TargetX, float _TargetY, MGT_HitBox _Target)
        {
            Rectangle FutureRect = GetRectAt(_TargetX, _TargetY);

            if (FutureRect.Intersects(_Target.GetHitBoxRect()))
                return false;

            return true;
        }

        public void ConnectSprite(MGT_Sprite _Sprite)
        {
            Width = _Sprite.GetWidth();
            Height = _Sprite.GetHeight();
            FollowObject(_Sprite.GetPosition());

            int finalWidth = (int)(Width * Scale.X);
            int finalHeight = (int)(Height * Scale.Y);

            HitBox = new Rectangle(
                (int)PositionX + (finalWidth / 2),
                (int)PositionY + (finalHeight / 2),
                finalWidth,
                finalHeight
            );
        }

        //Draw
        public void Draw()
        {

            int finalWidth = (int)(Width * Scale.X);
            int finalHeight = (int)(Height * Scale.Y);

            HitBox = new Rectangle((int)PositionX, (int)PositionY, finalWidth, finalHeight);

            MGT_Loader.GetSpriteBatch().Draw(pixel.GetPixel(), HitBox, Color * Alpha);
        }
    }
}