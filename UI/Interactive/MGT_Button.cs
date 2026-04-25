using MGTool.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Create;

namespace MGTool.UI
{
    public class MGT_Button
    {
        //Main

        private Rectangle Button;
        private Texture2D Texture = null!;
        private int Width, Height, Margin;
        private float TextAlpha = 1f, Alpha = 1f, Rotation = 0f, PositionX, PositionY;
        private Vector2 Scale;
        private Color Color;
        private MGT_Pixel pixel = new MGT_Pixel();
        private bool IsPressed = false, IsHolded = false, IsHovered = false, IsDoubleClicked = false;
        private MGT_Text ButtonText = new MGT_Text();
        private bool canClick = false;
        private float TimerHold = 0f, TimerDoubleClick = 0f;
        private int Click = 0;
        private string Text = null!;
        public enum AnchorX { Left, Right, Center, None}
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

            ButtonText.SetText("Button");
            ButtonText.SetColor(Color.White);

            pixel.New();
        }

        //Set

        public void SetText(SpriteFont _Font, string _Text, Color _Color)
        {
            Text = _Text;
            ButtonText.New(_Font, Text, new Vector2(PositionX + (Width / 2) - (ButtonText.GetSize().X / 2), PositionY + (Height / 2) - (ButtonText.GetSize().Y / 2)), _Color);
        }

        public SpriteFont SetTextFont(SpriteFont _Font)
        {
            return ButtonText.SetFont(_Font);
        }

        public string SetTextValue(string _Text)
        {
            Text = _Text;
            return ButtonText.SetText(Text);
        }

        public Color SetTextColor(Color _Color)
        {
            return ButtonText.SetColor(_Color);
        }

        public AnchorX SetAlignX(AnchorX _anchorX)
        {
            return anchorX = _anchorX;
        }

        public AnchorY SetAlignY(AnchorY _anchorY)
        {
            return anchorY = _anchorY;
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

        public float SetTextAlpha(float _Alpha)
        {
            return TextAlpha = _Alpha;
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
            return Button;
        }

        public SpriteFont GetTextFont()
        {
            return ButtonText.GetFont();
        }

        public string GetText()
        {
            return ButtonText.GetText();
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

        public float GetTextAlpha()
        {
            return TextAlpha;
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

        public bool GetPress()
        {
            return IsPressed;
        }
        public bool GetHold()
        {
            return IsHolded;
        }
        public bool GetDoubleClick()
        {
            return IsDoubleClicked;
        }
        public bool GetHover()
        {
            return IsHovered;
        }

        //Others

        public void DetectPress()
        {
            if (IsHovered && MGT_Loader.GetCurrentMouseState().LeftButton == ButtonState.Pressed &&MGT_Loader.GetPreviousMouseState().LeftButton == ButtonState.Released)
            {
                IsPressed = true;
            }
            else
            {
                IsPressed = false;
            }
        }

        public void DetectHover()
        {
            if (Button.Contains(MGT_Loader.GetCurrentMouseState().X, MGT_Loader.GetCurrentMouseState().Y))
            {
                IsHovered = true;

                if (MGT_Loader.GetCurrentMouseState().LeftButton == ButtonState.Released)
                {
                    canClick = true;
                }
            }
            else
            {
                IsHovered = false;
                canClick = false;
            }
        }

        public void DetectDoubleClick(float _DoubleClickDuration = 3f)
        {
            if (IsHovered && MGT_Loader.GetCurrentMouseState().LeftButton == ButtonState.Pressed && MGT_Loader.GetPreviousMouseState().LeftButton == ButtonState.Released)
            {
                Click += 1;
                if (Click == 1) TimerDoubleClick = 0f;
            }

            if (Click == 1)
            {
                TimerDoubleClick += MGT_Loader.GetDeltaTime();

                if (TimerDoubleClick > _DoubleClickDuration)
                {
                    Click = 0;
                    TimerDoubleClick = 0f;
                }
            }

            if (Click >= 2)
            {
                IsDoubleClicked = true;
                Click = 0;
                TimerDoubleClick = 0f;
            }
            else
            {
                IsDoubleClicked = false;
            }
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
            PasteOnAnchorX();
            PasteOnAnchorY();

            int finalWidth = (int)(Width * Scale.X);
            int finalHeight = (int)(Height * Scale.Y);

            Button = new Rectangle((int)PositionX, (int)PositionY, finalWidth, finalHeight);

            Texture2D currentTexture = (Texture == null) ? pixel.GetPixel() : Texture;

            Vector2 textureOrigin = new Vector2(currentTexture.Width / 2f, currentTexture.Height / 2f);

            Vector2 screenCenter = new Vector2(PositionX + finalWidth / 2f, PositionY + finalHeight / 2f);

            MGT_Loader.GetSpriteBatch().Draw(
                currentTexture,
                new Rectangle((int)screenCenter.X, (int)screenCenter.Y, finalWidth, finalHeight),
                null,
                Color * Alpha,
                Rotation,
                textureOrigin,
                SpriteEffects.None,
                0f
            );

            if (Text != null)
            {
                ButtonText.SetPosition(screenCenter);
                ButtonText.SetRotation(Rotation);
                ButtonText.SetAlpha(TextAlpha);

                ButtonText.SetAlignX(MGT_Text.AnchorX.None);
                ButtonText.SetAlignY(MGT_Text.AnchorY.None);

                ButtonText.Draw();
            }
        }
    }
}