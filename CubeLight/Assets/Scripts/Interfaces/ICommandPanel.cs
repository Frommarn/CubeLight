namespace Assets.Scripts.Interfaces
{
    internal interface ICommandPanel
    {
        void AddButtonCommands(IButtonCommands commandButtons);
        void RemoveButtonCommands(IButtonCommands commandButtons);
        void ShowPanel();
        void HidePanel();
    }
}