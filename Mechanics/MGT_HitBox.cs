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
        private bool GravityAdded = false, isOnGround = false;
        MGT_Physics phy = new MGT_Physics();
        float curX, curY;
        List<MGT_HitBox> Targets = new List<MGT_HitBox>();

        //New

        public void New(int _Width, int _Height, Vector2 _Position, Color _Color, float _Alpha = 1f)
        {
            Scale = Vector2.One;

            SetSize(_Width, _Height);
            PositionX = _Position.X;
            PositionY = _Position.Y;
            curX = PositionX;
            curY = PositionY;
            SetColor(_Color);
            SetAlpha(_Alpha);

            HitBoxText.SetText("Button");
            HitBoxText.SetColor(Color.White);

            Targets.Clear();

            pixel.New();
            phy.New();
            UpdateHitBox();
        }

        //Set

        public void SetRootPosition(Vector2 RootPosition)
        {
            curX = RootPosition.X;
            curY = RootPosition.Y;
            PositionX = curX;
            PositionY = curY;
            UpdateHitBox();
        }

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
            Width = _Width;
            Height = _Height;
            UpdateHitBox();
        }

        public int SetWidth(int _Width)
        {
            Width = _Width;
            UpdateHitBox();
            return Width;
        }

        public int SetHeight(int _Height)
        {
            Height = _Height;
            UpdateHitBox();
            return Height;
        }

        public Vector2 SetScale(float _ScaleX, float _ScaleY)
        {
            Scale = new Vector2(_ScaleX, _ScaleY);
            UpdateHitBox();
            return Scale;
        }

        public float SetAlpha(float _Alpha)
        {
            return Alpha = _Alpha;
        }

        public Color SetColor(Color _Color)
        {
            return Color = _Color;
        }

        public void SetGravity(bool state)
        {
            GravityAdded = state;
            if (state) phy.AddVelocityY();
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
            return Width * Scale.X;
        }

        public float GetHeight()
        {
            return Height * Scale.Y;
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

        public MGT_Physics GetGravity()
        {
            return phy;
        }

        public Vector2 GetHitBoxCollisonPosition()
        {
            return new Vector2(curX, curY);
        }

        //Others

        public void AddNewTarget(MGT_HitBox _Target)
        {
            Targets.Add(_Target);
        }

        public void RemoveTarget(MGT_HitBox _Target)
        {
            Targets.Remove(_Target);
        }

        public void FollowObject(Vector2 _ObjectPosition)
        {
            curX = _ObjectPosition.X;
            curY = _ObjectPosition.Y;
            PositionX = curX;
            PositionY = curY;
            UpdateHitBox();
        }

        private bool IsSpaceFree(float _tx, float _ty)
        {
            Rectangle futureRect = GetRectAt(_tx, _ty);
            foreach (var target in Targets)
            {
                if (futureRect.Intersects(target.GetHitBoxRect()))
                {
                    return false;
                }
            }
            return true;
        }

        private void UpdateHitBox()
        {
            HitBox = new Rectangle((int)PositionX, (int)PositionY, (int)(Width * Scale.X), (int)(Height * Scale.Y));
        }

        public void SetDynamicCollison(float dx = 0f, float dy = 0f)
        {
            float dt = MGT_Loader.GetDeltaTime();

            if (!GravityAdded)
            {
                if (IsSpaceFree(curX + dx, curY)) curX += dx;
                if (IsSpaceFree(curX, curY + dy)) curY += dy;
            }
            else
            {
                phy.AddVelocityY();

                if (IsSpaceFree(curX + dx, curY)) curX += dx;

                float finalMoveY = phy.GetVelocityY() * dt;

                if (IsSpaceFree(curX, curY + finalMoveY))
                {
                    curY += finalMoveY;
                    isOnGround = false;
                }
                else
                {
                    if (finalMoveY > 0)
                    {
                        isOnGround = true;
                        phy.StopVelocityY();
                        while (IsSpaceFree(curX, curY + 0.1f)) curY += 0.1f;
                    }
                    else if (finalMoveY < 0)
                    {
                        phy.StopVelocityY();
                    }
                }
            }

            PositionX = curX;
            PositionY = curY;
            UpdateHitBox();
        }

        public bool DetectGravityGround()
        {
            isOnGround = !IsSpaceFree(curX, curY + 1f);
            return isOnGround;
        }

        public void ConnectSprite(MGT_Sprite _Sprite)
        {
            Width = _Sprite.GetWidth();
            Height = _Sprite.GetHeight();
            FollowObject(_Sprite.GetPosition());
        }

        //Draw
        public void Draw()
        {
            MGT_Loader.GetSpriteBatch().Draw(pixel.GetPixel(), HitBox, Color * Alpha);

            if (Text != null)
            {
                Vector2 textPos = new Vector2(
                    HitBox.X + (HitBox.Width / 2) - (HitBoxText.GetSize().X / 2),
                    HitBox.Y + (HitBox.Height / 2) - (HitBoxText.GetSize().Y / 2)
                );
                HitBoxText.SetPosition(textPos);
                HitBoxText.Draw();
            }
        }
    }
}