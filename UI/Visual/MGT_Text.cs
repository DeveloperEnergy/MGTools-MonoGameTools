using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MGTool.Main;


namespace MGTool.UI
{
    public class MGT_Text
    {
        //Main

        private string Text = "";
        private SpriteFont Font = null!;
        private Vector2 Scale, Origin, Size;
        private float Rotation, Alpha, PositionX, PositionY;
        private Color Color;
        private int Margin;

        public enum AnchorX { Left, Right, Center, None }
        public enum AnchorY { Up, Down, Center, None }

        private AnchorX anchorX = AnchorX.None;
        private AnchorY anchorY = AnchorY.None;

        //New

        public void New(SpriteFont _Font, string _Text, Vector2 _Position, Color _Color, float _Alpha = 1f)
        {
            SetFont(_Font);
            SetText(_Text);
            SetPosition(new Vector2(_Position.X, _Position.Y));
            SetColor(_Color);
            SetAlpha(_Alpha);
            Scale = Vector2.One;
            Size = Font.MeasureString(Text);
        }

        //Set
        public SpriteFont SetFont(SpriteFont _Font)
        {
            return Font = _Font;
        }

        public string SetText(string _Text)
        {
            return Text = _Text;
        }

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
        public SpriteFont GetFont()
        {
            return Font;
        }

        public string GetText()
        {
            return Text;
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

        public Vector2 GetSize()
        {
            return Size;
        }

        //Others

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
                    PositionX = MGT_Loader.GetViewport().Width - ((int)(Size.X * Scale.X)) - Margin;
                    break;
                case AnchorX.Center:
                    PositionX = (MGT_Loader.GetViewport().Width / 2) - ((int)(Size.X * Scale.X) / 2);
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
                    PositionY = MGT_Loader.GetViewport().Height - ((int)(Size.Y * Scale.Y)) - Margin;
                    break;
                case AnchorY.Center:
                    PositionY = MGT_Loader.GetViewport().Height / 2f - ((int)(Size.Y * Scale.Y) / 2);
                    break;
            }

            return PositionY;
        }

        //Draw

        public void Draw()
        {
            PasteOnAnchorX();
            PasteOnAnchorY();

            Size = Font.MeasureString(Text);
            Origin = Size / 2f;

            MGT_Loader.GetSpriteBatch().DrawString(
                Font,
                Text,
                new Vector2(PositionX, PositionY),
                Color * Alpha,
                Rotation,
                Origin,
                Scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}