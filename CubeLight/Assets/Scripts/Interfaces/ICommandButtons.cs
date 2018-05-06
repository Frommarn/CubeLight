using System.Collections.Generic;

namespace Assets.Scripts.Interfaces
{
    public interface IButtonCommands
    {
        IEnumerable<ButtonConfig> Buttons { get; }
    }
}
