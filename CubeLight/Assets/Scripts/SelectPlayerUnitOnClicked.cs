using Assets.Scripts.Interfaces;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayerUnitOnClicked : MonoBehaviour {

    private ISelectionManager _SelectionManager;

    private void Start()
    {
        //gameObjectSelectionManager = GameObject.FindGameObjectWithTag("PlayerUnitManager").GetComponent<PlayerGameObjectSelectionManager>();
        _SelectionManager = Managers._PlayerSelectionManager.GetComponents<ISelectionManager>().ThrowIfMoreThanOne();
    }

    void Clicked()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            // Tell the Player Unit Manager to select this object also
            _SelectionManager.SelectAdditionalGameObject(gameObject);
        }
        else
        {
            // Tell the Player Unit Manager to select this object
            _SelectionManager.SelectSingleGameObject(gameObject);
        }
    }

}
