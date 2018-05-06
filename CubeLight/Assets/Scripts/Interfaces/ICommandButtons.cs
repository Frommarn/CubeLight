using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Interfaces
{
    public interface ICommandButtons
    {
        ButtonConfig Button1 { get; }
        ButtonConfig Button2 { get; }
        ButtonConfig Button3 { get; }
        ButtonConfig Button4 { get; }
    }
}
