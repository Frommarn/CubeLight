using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitResourceGathering : MonoBehaviour, IButtonCommands {

    public float _HarvestSpeed = 2.0f;
    public int _HarvestedLight = 0;
    public List<GameObject> _TilesTouching;
    private Vector3 _LastPosition;
    private bool _IsHarvesting;
    private float _AccumulativeHarvestTime;

    private List<ButtonConfig> _Buttons;

    IEnumerable<ButtonConfig> IButtonCommands.Buttons { get { return _Buttons; } }

    private void Awake()
    {
        _Buttons = new List<ButtonConfig>()
        {
            new ButtonConfig(HarvestLightWhenStill, "Start\nGathering", 0),
            new ButtonConfig(DoNotHarvestLightWhenStill, "Stop\nGathering", 1)
        };
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
