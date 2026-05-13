using MGTool.Main;
using Microsoft.Xna.Framework;
using System;
using MGTool.Systems;
using MGTool.Create;

namespace MGTool.Mechanics
{
    public class MGT_Camera
    {
        //Main
        private float LerpSpeed;
        private Vector2 Position;
        private float Scale, Rotation;
        public enum cameraType { Dynamic, Static }
        private cameraType Type;

        private float shakeMagnitude = 0f;
        private float shakeDuration = 0f;
        private Random random = new Random();

        private float minX = float.MinValue, maxX = float.MaxValue;
        private float minY = float.MinValue, maxY = float.MaxValue;
        private bool useRestrictions = false;

        //New
        public void New(cameraType type = cameraType.Static)
        {
            Position = new Vector2(MGT_Loader.GetViewport().Width * 0.5f, MGT_Loader.GetViewport().Height * 0.5f);
            SetRotation(0f);
            SetScale(1f);
            Type = type;
            LerpSpeed = 0.1f;
        }

        //Set
        public float SetLerpSpeed(float Speed)
        {
            LerpSpeed = Speed;
            return LerpSpeed;
        }

        public float SetScale(float _Scale)
        {
            return Scale = _Scale;
        }

        public float SetRotation(float _Rotation)
        {
            return Rotation = _Rotation;
        }

        public void Shake(float magnitude, float duration)
        {
            shakeMagnitude = magnitude;
            shakeDuration = duration;
        }

        public void SetRestrictions(float _minX, float _maxX, float _minY, float _maxY)
        {
            minX = -_minX;
            maxX = _maxX;
            minY = -_minY;
            maxY = _maxY;
            useRestrictions = true;
        }

        //Get
        public float GetLerpSpeed()
        {
            return LerpSpeed;
        }

        public Vector2 GetPosition()
        {
            return Position;
        }

        public float GetScale()
        {
            return Scale;
        }

        public float GetRotation()
        {
            return Rotation;
        }

        //Others
        private Vector2 ApplyRestrictions(Vector2 targetPos)
        {
            if (!useRestrictions) return targetPos;

            float viewWidth = MGT_Loader.GetViewport().Width / Scale;
            float viewHeight = MGT_Loader.GetViewport().Height / Scale;

            float limitLeft = minX + (viewWidth / 2f);
            float limitRight = maxX - (viewWidth / 2f);
            float limitTop = minY + (viewHeight / 2f);
            float limitBottom = maxY - (viewHeight / 2f);

            float finalX = (limitRight < limitLeft) ? (minX + maxX) / 2f : MathHelper.Clamp(targetPos.X, limitLeft, limitRight);
            float finalY = (limitBottom < limitTop) ? (minY + maxY) / 2f : MathHelper.Clamp(targetPos.Y, limitTop, limitBottom);

            return new Vector2(finalX, finalY);
        }

        private void CameraFocus(float width, float height, Vector2 Pos)
        {
            Vector2 target = new Vector2(Pos.X + width / 2, Pos.Y + height / 2);

            target = ApplyRestrictions(target);

            if (Type == cameraType.Static)
            {
                Position = target;
            }
            else if (Type == cameraType.Dynamic)
            {
                Position = Vector2.Lerp(Position, target, LerpSpeed);
            }

            if (shakeDuration > 0)
            {
                Position += new Vector2((float)random.NextDouble() * 2 - 1, (float)random.NextDouble() * 2 - 1) * shakeMagnitude;
                shakeDuration -= MGT_Loader.GetDeltaTime();
            }
        }

        public void FollowObject(object Object)
        {
            var MGT_HitBox = MGT_System.FindInside<MGT_HitBox>(Object);
            var MGT_Rectangle = MGT_System.FindInside<MGT_Rectangle>(Object);
            var Rectangle = MGT_System.FindInside<Rectangle>(Object);

            if (MGT_HitBox != null)
            {
                CameraFocus(MGT_HitBox.GetWidth(), MGT_HitBox.GetHeight(), MGT_HitBox.GetPosition());
            }
            else if (MGT_Rectangle != null)
            {
                CameraFocus(MGT_Rectangle.GetWidth(), MGT_Rectangle.GetHeight(), MGT_Rectangle.GetPosition());
            }
            else if (Rectangle != Rectangle.Empty)
            {
                CameraFocus(Rectangle.Width, Rectangle.Height, new Vector2(Rectangle.X, Rectangle.Y));
            }
            else throw new ArgumentException("Не нашлось MonoGame.Rectangle, MGT_HitBox, MGT_Rectangle из заданного класса!");
        }

        public void RestrictionsX(int min, int max) { minX = -min; maxX = max; useRestrictions = true; }
        public void RestrictionsY(int min, int max) { minY = -min; maxY = max; useRestrictions = true; }

        public Matrix WorldToScreen()
        {
            int posX = (int)Math.Round(Position.X);
            int posY = (int)Math.Round(Position.Y);

            return Matrix.CreateTranslation(new Vector3(-posX, -posY, 0)) *
                   Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(Scale) *
                   Matrix.CreateTranslation(new Vector3(MGT_Loader.GetViewport().Width * 0.5f, MGT_Loader.GetViewport().Height * 0.5f, 0));
        }

        public Matrix ScreenToWorld()
        {
            return Matrix.Invert(WorldToScreen());
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, ScreenToWorld());
        }
    }
}