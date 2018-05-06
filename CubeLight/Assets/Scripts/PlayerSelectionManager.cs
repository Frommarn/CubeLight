using Assets.Scripts.Interfaces;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionManager : MonoBehaviour, ISelectionManager, IPreSelectionManager {

    public List<GameObject> _SelectedGameObjects;
    public List<GameObject> _PreSelectedGameObjects;
    private ICommandPanel _CommandPanel;   // So it can update the UI for the player when (de)selection happens

    // Use this for initialization
    void Start ()
	{
        _SelectedGameObjects = new List<GameObject>();
        _PreSelectedGameObjects = new List<GameObject>();
        _CommandPanel = Managers._UIManager.GetComponents<ICommandPanel>().ThrowIfMoreThanOne();
    }

    bool ISelectionManager.IsSelected(GameObject unit)
    {
        return _SelectedGameObjects.Contains(unit);
    }

    void ISelectionManager.SelectSingleGameObject(GameObject unit)
    {
        _SelectedGameObjects.Clear();
        _SelectedGameObjects.Add(unit);
        AddGameObjectButtonCommands(unit);
        Debug.Log(_SelectedGameObjects);
    }

    void ISelectionManager.SelectAdditionalGameObject(GameObject unit)
    {
        if (!_SelectedGameObjects.Contains(unit))
        {
            _SelectedGameObjects.Add(unit);
            AddGameObjectButtonCommands(unit);
            Debug.Log(_SelectedGameObjects);
        }
    }

    void ISelectionManager.DeselectSingleGameObject(GameObject unit)
    {
        if (_SelectedGameObjects.Contains(unit))
        {
            _SelectedGameObjects.Remove(unit);
            RemoveGameObjectButtonCommands(unit);
            if (_SelectedGameObjects.Count <= 0)
            {
                _CommandPanel.HidePanel();
            }
        }
    }

    void ISelectionManager.DeselectAllSelectedGameObjects()
    {
        _SelectedGameObjects.Clear();
        _CommandPanel.HidePanel();
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

    private void AddGameObjectButtonCommands(GameObject unit)
    {
        IButtonCommands buttonCommands = unit.GetComponents<IButtonCommands>().ThrowIfMoreThanOne();
        if (buttonCommands != null)
        {
            _CommandPanel.ShowPanel();
            _CommandPanel.AddButtonCommands(buttonCommands);
        }
    }

    private void RemoveGameObjectButtonCommands(GameObject unit)
    {
        IButtonCommands buttonCommands = unit.GetComponents<IButtonCommands>().ThrowIfMoreThanOne();
        if (buttonCommands != null)
        {
            _CommandPanel.RemoveButtonCommands(buttonCommands);
        }
    }
}
