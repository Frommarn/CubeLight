namespace Assets.Scripts.Interfaces
{
    internal interface ICommandPanel
    {
        void ClearCommandButtons();
        void AddButtonCommands(IButtonCommands commandButtons);
        void RemoveButtonCommands(IButtonCommands commandButtons);
        void ShowPanel();
        void HidePanel();
    }
}