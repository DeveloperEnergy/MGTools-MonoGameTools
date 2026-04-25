using MGTool.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Create;

namespace MGTool.UI
{
    public class MGT_CheckBox
    {
        //Main

        private Rectangle Box;
        private Rectangle Toggle;
        private Texture2D BoxTexture = null!;
        private Texture2D ToggleTexture = null!;
        private int Width = 1, Height = 1, Margin;
        private float Alpha = 1f, Rotation = 0f, PositionX, PositionY;
        private Vector2 Scale = Vector2.One;
        private Color BoxColor;
        private Color ToggleColor;
        private MGT_Pixel pixel = new MGT_Pixel();
        private MGT_Mouse mouse = new MGT_Mouse();
        private bool IsPressed = false, IsHovered = false;

        public enum AnchorX { Left, Right, Center, None }
        public enum AnchorY { Up, Down, Center, None }

        private AnchorX anchorX = AnchorX.None;
        private AnchorY anchorY = AnchorY.None;

        //New

        public void New(float _Scale, Vector2 _Position, Color _BoxColor, Color _ToggleColor, float _Alpha = 1f)
        {
            SetScale(_Scale, _Scale);
            SetPosition(new Vector2(_Position.X, _Position.Y));
            SetBoxColor(_BoxColor);
            SetToggleColor(_ToggleColor);
            SetAlpha(_Alpha);

            Width = 1;
            Height = 1;
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

        public Texture2D SetBoxTexture(Texture2D _BoxTexture)
        {
            return BoxTexture = _BoxTexture;
        }

        public Texture2D SetToggleTexture(Texture2D _ToggleTexture)
        {
            return ToggleTexture = _ToggleTexture;
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

        public Color SetBoxColor(Color _BoxColor)
        {
            return BoxColor = _BoxColor;
        }

        public Color SetToggleColor(Color _ToggleColor)
        {
            return ToggleColor = _ToggleColor;
        }

        public int SetMargin(int _Margin)
        {
            return Margin = _Margin;
        }

        //Get

        public Texture2D GetBoxTexture()
        {
            return BoxTexture;
        }

        public Texture2D GetToggleTexture()
        {
            return ToggleTexture;
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

        public Color GetBoxColor()
        {
            return BoxColor;
        }

        public Color GetToggleColor()
        {
            return ToggleColor;
        }

        public int GetMargin()
        {
            return Margin;
        }

        public bool DetectPress()
        {
            return IsPressed;
        }

        public bool DetectHover()
        {
            return IsHovered;
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
            int finalBoxWidth = (int)(Width * Scale.X);
            int finalBoxHeight = (int)(Height * Scale.Y);

            PasteOnAnchorX();
            PasteOnAnchorY();

            Box = new Rectangle((int)PositionX, (int)PositionY, finalBoxWidth, finalBoxHeight);

            mouse.DetectLB_Pressed();

            if (Box.Contains(mouse.GetPositionX(), mouse.GetPositionY()))
            {
                IsHovered = true;
                if (mouse.GetLB_Pressed())
                    IsPressed = !IsPressed;
            }
            else
            {
                IsHovered = false;
            }


            Toggle = new Rectangle(Box.X + (Box.Width / 2) - ((Box.Width - 8) / 2), Box.Y + (Box.Height / 2) - ((Box.Height - 8) / 2), Box.Width - 8,  Box.Height - 8);

            Texture2D currentBoxTex = BoxTexture ?? pixel.GetPixel();
            Texture2D currentToggleTex = ToggleTexture ?? pixel.GetPixel();

            Vector2 BoxOrigin = new Vector2(currentBoxTex.Width / 2f, currentBoxTex.Height / 2f);
            Vector2 ToggleOrigin = new Vector2(currentToggleTex.Width / 2f, currentToggleTex.Height / 2f);


            MGT_Loader.GetSpriteBatch().Draw(currentBoxTex, new Rectangle(Box.X + Box.Width / 2, Box.Y + Box.Height / 2, Box.Width, Box.Height), null, BoxColor * Alpha, Rotation, BoxOrigin, SpriteEffects.None, 0f);
            if(IsPressed) MGT_Loader.GetSpriteBatch().Draw(currentToggleTex, new Rectangle(Toggle.X + Toggle.Width / 2, Toggle.Y + Toggle.Height / 2, Toggle.Width, Toggle.Height), null, ToggleColor * Alpha, Rotation, ToggleOrigin, SpriteEffects.None, 0f);
        }
    }
}