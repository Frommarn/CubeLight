using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    internal interface ISelectionManager
    {
        bool IsSelected(GameObject unit);
        void SelectSingleGameObject(GameObject unit);
        void SelectAdditionalGameObject(GameObject unit);
        void DeselectAllSelectedGameObjects();
        List<GameObject> GetSelectedGameObjects();
        void DeselectSingleGameObject(GameObject gameObject);
    }
}