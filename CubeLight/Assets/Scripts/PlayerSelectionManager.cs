using Assets.Scripts.Interfaces;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionManager : MonoBehaviour, ISelectionManager, IPreSelectionManager {

    public List<GameObject> _SelectedGameObjects;
    public List<GameObject> _PreSelectedGameObjects;
    private SelectionCommandPanel _SelectionCommandPanel;   // So it can update the UI for the player when (de)selection happens

    // Use this for initialization
    void Start ()
	{
        _SelectedGameObjects = new List<GameObject>();
        _PreSelectedGameObjects = new List<GameObject>();
        _SelectionCommandPanel = Managers._UIManager.GetComponent<SelectionCommandPanel>();
    }

    bool ISelectionManager.IsSelected(GameObject unit)
    {
        return _SelectedGameObjects.Contains(unit);
    }

    void ISelectionManager.SelectSingleGameObject(GameObject unit)
    {
        _SelectedGameObjects.Clear();
        _SelectedGameObjects.Add(unit);
        ICommandButtons commandButtons = unit.GetComponents<ICommandButtons>().ThrowIfMoreThanOne();
        if (commandButtons != null)
        {
            _SelectionCommandPanel.ShowPanel();
            _SelectionCommandPanel.PopulateCommandButtons(commandButtons);
        }
        Debug.Log(_SelectedGameObjects);
    }

    void ISelectionManager.SelectAdditionalGameObject(GameObject unit)
    {
        if (!_SelectedGameObjects.Contains(unit))
        {
            _SelectedGameObjects.Add(unit);
            // TODO Add method to Update the CommandPanel when selecting additional units
            Debug.Log(_SelectedGameObjects);
        }
    }

    void ISelectionManager.DeselectAllSelectedGameObjects()
    {
        _SelectedGameObjects.Clear();
        _SelectionCommandPanel.HidePanel();
    }

    List<GameObject> ISelectionManager.GetSelectedGameObjects()
    {
        return _SelectedGameObjects;
    }

    void IPreSelectionManager.ClearPreSelectedGameObjects()
    {
        _PreSelectedGameObjects.Clear();
    }

    void IPreSelectionManager.AddGameObjectToPreSelection(GameObject unit)
    {
        _PreSelectedGameObjects.Add(unit);
    }

    void IPreSelectionManager.RemoveGameObjectFromPreSelection(GameObject unit)
    {
        if (_PreSelectedGameObjects.Contains(unit))
        {
            _PreSelectedGameObjects.Remove(unit);
        }
    }

    void IPreSelectionManager.MovePreSelectionToSelectedGameObjects()
    {
        foreach (var item in _PreSelectedGameObjects)
        {
            (this as ISelectionManager).SelectAdditionalGameObject(item);
        }
        _PreSelectedGameObjects.Clear();
    }
}
