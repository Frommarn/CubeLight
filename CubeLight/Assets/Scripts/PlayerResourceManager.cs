using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceManager : MonoBehaviour {

    public int _playerResource = 0;

    // Use this for initialization
    void Start ()
	{
    }

    // Update is called once per frame
    void Update ()
	{
		
	}

    public void AddResource(int resource)
    {
        _playerResource += resource;
    }

    public bool UseResource(int amountToUse)
    {
        if (amountToUse < _playerResource)
        {
            _playerResource -= amountToUse;
            return true;
        }
        return false;
    }
}
