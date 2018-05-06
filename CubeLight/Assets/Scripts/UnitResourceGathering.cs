using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitResourceGathering : MonoBehaviour, ICommandButtons {

    public float _HarvestSpeed = 2.0f;
    public int _HarvestedLight = 0;
    public List<GameObject> _TilesTouching;
    private Vector3 _LastPosition;
    private bool _IsHarvesting;
    private float _AccumulativeHarvestTime;

    private ButtonConfig _Button1;
    private ButtonConfig _Button2;
    private ButtonConfig _Button3;
    private ButtonConfig _Button4;

    ButtonConfig ICommandButtons.Button1 { get { return _Button1; } }
    ButtonConfig ICommandButtons.Button2 { get { return _Button2; } }
    ButtonConfig ICommandButtons.Button3 { get { return _Button3; } }
    ButtonConfig ICommandButtons.Button4 { get { return _Button4; } }

    private void Awake()
    {
        _Button1 = new ButtonConfig(HarvestLightWhenStill, "Start\nGathering");
        _Button2 = new ButtonConfig(DoNotHarvestLightWhenStill, "Stop\nGathering");
        _Button3 = null;
        _Button4 = null;
    }

    // Use this for initialization
    void Start ()
	{
        _TilesTouching = new List<GameObject>();
        _LastPosition = gameObject.transform.position;
        _IsHarvesting = false;
        _AccumulativeHarvestTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (_IsHarvesting && _LastPosition == gameObject.transform.position)
        {
            HarvestLightFromTiles();
        }
        //else if (_LastPosition == gameObject.transform.position)
        //{
        //    _IsHarvesting = true;
        //}
        else
        {
            _LastPosition = gameObject.transform.position;
        }
	}

    void HarvestLightWhenStill()
    {
        _IsHarvesting = true;
    }

    void DoNotHarvestLightWhenStill()
    {
        _IsHarvesting = false;
    }

    private void HarvestLightFromTiles()
    {
        _AccumulativeHarvestTime += Time.deltaTime;
        if (_AccumulativeHarvestTime >= _HarvestSpeed)
        {
            _AccumulativeHarvestTime -= _HarvestSpeed;
            foreach (var tile in _TilesTouching)
            {
                HarvestLightFromTile(tile);
            }
        }
    }

    private void HarvestLightFromTile(GameObject tile)
    {
        TileData script = tile.GetComponent<TileData>();
        int lightGathered = script.GatherLightFromTile(1);
        if (lightGathered == 0)
        {
            // Stop gathering, no light left in Tile.
            _IsHarvesting = false;
        }
        else
        {
            _HarvestedLight += lightGathered;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            _TilesTouching.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            _TilesTouching.Remove(collision.gameObject);
        }
    }
}
