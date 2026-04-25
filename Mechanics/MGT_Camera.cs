using Microsoft.Xna.Framework;
using MGTool.Main;


namespace MGTool.Mechanics
{
    public class MGT_Camera()
    {
        //Main

        private Vector2 Position;
        private float Scale, Rotation;

        //New
        
        public void New()
        {
            Position = new Vector2(MGT_Loader.GetViewport().Width * 0.5f, MGT_Loader.GetViewport().Height * 0.5f);
            SetRotation(0f);
            SetScale(1f);
        }

        //Set

        public float SetScale(float _Scale)
        {
            return Scale = _Scale;
        }

        public float SetRotation(float _Rotation)
        {
            return Rotation = _Rotation;
        }

        //Get

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

        public void FollowObject(Vector2 _ObjectPosition, float _ObjectWidth, float _ObjectHeight)
        {
            Vector2 target = new Vector2(_ObjectPosition.X + _ObjectWidth / 2, _ObjectPosition.Y + _ObjectHeight / 2);
            Position = Vector2.Lerp(Position, target, 0.1f);
        }

        public Matrix WorldToScreen()
        {
            return Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(Scale) *
                Matrix.CreateTranslation(new Vector3(MGT_Loader.GetViewport().Width * 0.5f, MGT_Loader.GetViewport().Height * 0.5f, 0));
        }

        public Matrix ScreenToWorld()
        {
            return Matrix.Invert(WorldToScreen());
        }
    }
}