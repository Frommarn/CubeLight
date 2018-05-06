using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class ButtonConfig
    {
        public ButtonConfig(Action buttonCommand, string buttonText)
        {
            ButtonCommand = buttonCommand;
            ButtonText = buttonText;
        }

        public Action ButtonCommand { get; private set; }
        public string ButtonText { get; private set; }
    }
}
