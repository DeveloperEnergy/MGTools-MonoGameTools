using MGTool.Main;


namespace MGTool.Mechanics
{
    public class MGT_Physics
    {
        //Main

        private float VelocityX = 0f, VelocityY = 0f, Gravity = 0f, JumpForce = 0f;

        //New

        public void New(float _Gravity = 1500f, float _JumpForce = 1000f)
        {
            Gravity = _Gravity;
            JumpForce = -_JumpForce;
        }

        //Set

        public float SetGravity(float _Gravity)
        {
            return Gravity = _Gravity;
        }

        public float SetJumpForce(float _JumpForce)
        {
            return JumpForce = -_JumpForce;
        }

        //Get

        public float GetVelocityX()
        {
            return VelocityX;
        }

        public float GetVelocityY()
        {
            return VelocityY;
        }

        //Others

        public float AddVelocityX()
        {
            return VelocityX += Gravity * MGT_Loader.GetDeltaTime();
        }

        public float AddVelocityY()
        {
            return VelocityY += Gravity * MGT_Loader.GetDeltaTime();
        }

        public float ApplyJumpX()
        {
            return VelocityX = JumpForce;
        }

        public float ApplyJumpY()
        {
            return VelocityY = JumpForce;
        }

        public float StopVelocityX()
        {
            return VelocityX = 0;
        }

        public float StopVelocityY()
        {
            return VelocityY = 0;
        }
    }
}