using Assets.Scripts.Interfaces;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreSelectWhenTrigger : MonoBehaviour {
    private IPreSelectionManager _PreSelectionManager;

    private void Start()
    {
        _PreSelectionManager = Managers.GetPlayerSelectionManager().GetComponents<IPreSelectionManager>().ThrowIfMoreThanOne();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Unit")
        {
            _PreSelectionManager.AddGameObjectToPreSelection(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Unit")
        {
            _PreSelectionManager.RemoveGameObjectFromPreSelection(other.gameObject);
        }
    }
}
