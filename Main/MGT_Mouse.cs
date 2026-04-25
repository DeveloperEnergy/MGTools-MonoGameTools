using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MGTool.Main
{
    public class MGT_Mouse
    {
        //Main

        private bool IsLB_Pressed = false, IsRB_Pressed = false, IsLB_Holded = false, IsRB_Holded = false, IsLB_DoubleClick = false, IsRB_DoubleClick = false;
        private float TimerLB_DoubleClick = 0f, TimerRB_DoubleClick = 0f, TimerLB_Hold = 0f, TimerRB_Hold = 0f;
        private int ClickLB = 0, ClickRB = 0;

        //Get
        public bool GetLB_Pressed()
        {
            return IsLB_Pressed;
        }

        public bool GetRB_Pressed()
        {
            return IsRB_Pressed;
        }

        public bool GetLB_Hold()
        {
            return IsLB_Holded;
        }

        public bool GetRB_Hold()
        {
            return IsRB_Holded;
        }

        public bool GetLB_DoubleClick()
        {
            return IsLB_DoubleClick;
        }

        public bool GetRB_DoubleClick()
        {
            return IsRB_DoubleClick;
        }

        public int GetPositionX()
        {
            return MGT_Loader.GetCurrentMouseState().Position.X;
        }

        public int GetPositionY()
        {
            return MGT_Loader.GetCurrentMouseState().Position.Y;
        }

        public Vector2 GetMouseWorldPosition(Matrix _InvertMatrix)
        {
            Vector2 screenPos = new Vector2(MGT_Loader.GetCurrentMouseState().X, MGT_Loader.GetCurrentMouseState().Y);

            return Vector2.Transform(screenPos, _InvertMatrix);
        }

        //Others

        public void DetectLB_Pressed()
        {
            if (MGT_Loader.GetCurrentMouseState().LeftButton == ButtonState.Pressed && MGT_Loader.GetPreviousMouseState().LeftButton == ButtonState.Released) IsLB_Pressed = true;
            else IsLB_Pressed = false;
        }

        public void DetectRB_Pressed()
        {
            if (MGT_Loader.GetCurrentMouseState().RightButton == ButtonState.Pressed && MGT_Loader.GetPreviousMouseState().RightButton == ButtonState.Released) IsRB_Pressed = true;
            else IsRB_Pressed = false;
        }

        public void DetectLB_Hold(float _HoldDuration = 2f)
        {
            if (MGT_Loader.GetCurrentMouseState().LeftButton == ButtonState.Pressed)
            {
                TimerLB_Hold += MGT_Loader.GetDeltaTime();
                if (TimerLB_Hold > _HoldDuration) IsLB_Holded = true;
            }
            else
            {
                IsLB_Holded = false;
                TimerLB_Hold = 0f;
            }
        }

        public void DetectRB_Hold(float _HoldDuration = 2f)
        {
            if (MGT_Loader.GetCurrentMouseState().RightButton == ButtonState.Pressed)
            {
                TimerRB_Hold += MGT_Loader.GetDeltaTime();
                if (TimerRB_Hold > _HoldDuration) IsRB_Holded = true;
            }
            else
            {
                IsRB_Holded = false;
                TimerRB_Hold = 0f;
            }
        }

        public void DetectLB_DoubleClick(float _DoubleClickDuration = 3f)
        {
            if (MGT_Loader.GetCurrentMouseState().LeftButton == ButtonState.Pressed && MGT_Loader.GetPreviousMouseState().LeftButton == ButtonState.Released)
            {
                ClickLB += 1;
                if (ClickLB == 1) TimerLB_DoubleClick = 0f;
            }

            if (ClickLB == 1)
            {
                TimerLB_DoubleClick += MGT_Loader.GetDeltaTime();

                if (TimerLB_DoubleClick > _DoubleClickDuration)
                {
                    ClickLB = 0;
                    TimerLB_DoubleClick = 0f;
                }
            }

            if (ClickLB >= 2)
            {
                IsLB_DoubleClick = true;
                ClickLB = 0;
                TimerLB_DoubleClick = 0f;
            }
            else
            {
                IsLB_DoubleClick = false;
            }
        }

        public void DetectRB_DoubleClick(float _DoubleClickDuration = 3f)
        {
            if (MGT_Loader.GetCurrentMouseState().RightButton == ButtonState.Pressed && MGT_Loader.GetPreviousMouseState().RightButton == ButtonState.Released)
            {
                ClickRB += 1;
                if (ClickRB == 1) TimerRB_DoubleClick = 0f;
            }

            if (ClickRB == 1)
            {
                TimerRB_DoubleClick += MGT_Loader.GetDeltaTime();

                if (TimerRB_DoubleClick > _DoubleClickDuration)
                {
                    ClickRB = 0;
                    TimerRB_DoubleClick = 0f;
                }
            }

            if (ClickRB >= 2)
            {
                IsRB_DoubleClick = true;
                ClickRB = 0;
                TimerRB_DoubleClick = 0f;
            }
            else
            {
                IsRB_DoubleClick = false;
            }
        }
    }
}