using Assets.Scripts.Interfaces;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelectedUnitsOnRightClick : MonoBehaviour {

    public GameObject moveEffectObject;

    private ISelectionManager _SelectionManager;

    private void Start()
    {
        //gameObjectSelectionManager = GameObject.FindGameObjectWithTag("PlayerUnitManager").GetComponent<PlayerGameObjectSelectionManager>();
        _SelectionManager = Managers._PlayerSelectionManager.GetComponents<ISelectionManager>().ThrowIfMoreThanOne();
    }

    void RightClicked(Vector3 clickPosition)
    {
        foreach (GameObject unit in _SelectionManager.GetSelectedGameObjects())
        {
            unit.SendMessage("MoveOrder", clickPosition, SendMessageOptions.DontRequireReceiver);
        }
        if (_SelectionManager.GetSelectedGameObjects().Count > 0)
        {
            Instantiate(moveEffectObject, clickPosition, moveEffectObject.transform.rotation);
        }
    }
}
