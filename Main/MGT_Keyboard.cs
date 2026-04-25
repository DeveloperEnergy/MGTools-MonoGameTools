using Microsoft.Xna.Framework.Input;

namespace MGTool.Main
{
    public class MGT_Keyboard
    {
        //Main

        private bool IsPressed = false, IsHolded = false, IsDoubleClick = false;
        private float TimerDoubleClick = 0f, TimerHold = 0f;
        private Keys Key;
        private int Click = 0;


        //New

        public void New(Keys _Key)
        {
            SetKey(_Key);
        }

        //Set
        public Keys SetKey(Keys _Key)
        {
            return Key = _Key;
        }

        //Get
        public bool GetPressed()
        {
            return IsPressed;
        }

        public bool GetHold()
        {
            return IsHolded;
        }

        public bool GetDoubleClick()
        {
            return IsDoubleClick;
        }

        //Others

        public bool DetectPressed()
        {
            if (MGT_Loader.GetCurrentKeyboardState().IsKeyDown(Key) && MGT_Loader.GetPreviousKeyboardState().IsKeyUp(Key)) IsPressed = true;
            else IsPressed = false;

            return IsPressed;
        }

        public bool DetectHold(float _HoldDuration)
        {
            if (MGT_Loader.GetCurrentKeyboardState().IsKeyDown(Key))
            {
                TimerHold += MGT_Loader.GetDeltaTime();
                if (TimerHold > _HoldDuration) IsHolded = true;
            }
            else
            {
                IsHolded = false;
                TimerHold = 0f;
            }

            return IsHolded;
        }

        public bool DetectDoubleClick(float _DoubleClickDuration)
        {
            if (MGT_Loader.GetCurrentKeyboardState().IsKeyDown(Key) && MGT_Loader.GetPreviousKeyboardState().IsKeyUp(Key))
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
                IsDoubleClick = true;
                Click = 0;
                TimerDoubleClick = 0f;
            }
            else
            {
                IsDoubleClick = false;
            }

            return IsDoubleClick;
        }

    }
}