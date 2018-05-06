using System;

namespace Assets.Scripts
{
    public class ButtonConfig
    {
        public ButtonConfig(Action buttonCommand, string buttonText, int buttonIndex)
        {
            ButtonCommand = buttonCommand;
            ButtonText = buttonText;
            ButtonIndex = buttonIndex;
        }

        public Action ButtonCommand { get; private set; }
        public string ButtonText { get; private set; }
        public int ButtonIndex { get; private set; }
    }
}
