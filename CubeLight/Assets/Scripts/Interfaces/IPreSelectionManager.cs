using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    internal interface IPreSelectionManager
    {
        void ClearPreSelectedGameObjects();
        void AddGameObjectToPreSelection(GameObject unit);
        void RemoveGameObjectFromPreSelection(GameObject unit);
        void MovePreSelectionToSelectedGameObjects();
    }
}