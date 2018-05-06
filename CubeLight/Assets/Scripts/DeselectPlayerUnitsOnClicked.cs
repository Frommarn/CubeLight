using Assets.Scripts.Interfaces;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeselectPlayerUnitsOnClicked : MonoBehaviour {

    private ISelectionManager _SelectionManager;

    private void Start()
    {
        _SelectionManager = Managers.GetPlayerSelectionManager().GetComponents<ISelectionManager>().ThrowIfMoreThanOne();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _SelectionManager.DeselectAllSelectedGameObjects();
        }
    }

    void Clicked()
    {
        if (!(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            _SelectionManager.DeselectAllSelectedGameObjects();
        }
    }
}
