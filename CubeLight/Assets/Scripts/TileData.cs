using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour {
    // Permanently exposed
    public int _MinTileLightIntensity = 0;
    public int _MaxTileLightIntensity = 100;
    public int _LightGenerationThreshold = 10;
    public int _LightGenerationSpeed = 10;

    // Exposed for debugging purposes
    public int _CreationX;
    public int _CreationZ;
    public int _TileLightIntensity;
    public float _LightGenerationBuildUp = 0;

    private Color _OriginalColor;
    private bool _IsTileAwake;

    // Use this for initialization
    void Start ()
	{
        _OriginalColor = GetComponent<Renderer>().material.color;
        _IsTileAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_IsTileAwake)
        {
            if (_LightGenerationThreshold < _TileLightIntensity && _TileLightIntensity < _MaxTileLightIntensity)
            {
                GenerateLight();
            }
        }
    }
    
    /// <summary>
    /// Gather light from Tile. Returns the actual amount gathered.
    /// </summary>
    /// <param name="amountToGather">The amount to gather.</param>
    /// <returns>The amount gathered.</returns>
    public int GatherLightFromTile(int amountToGather)
    {
        // Tile has enough light left
        if (_TileLightIntensity >= amountToGather)
        {
            _TileLightIntensity -= amountToGather;
            return amountToGather;
        }
        // Tile doesn't have enough light left, gather everything
        else
        {
            int lightGathered = _TileLightIntensity;
            _TileLightIntensity = 0;
            return lightGathered;
        }
    }

    /// <summary>
    /// Infuse light into Tile. Returns the excess light (if any).
    /// </summary>
    /// <param name="amountToInfuse">The amount to infuse.</param>
    /// <returns>The excess amount.</returns>
    public int InfuseLightIntoTile(int amountToInfuse)
    {
        // Tile has room for the entire amount
        if (_MaxTileLightIntensity >= (_TileLightIntensity + amountToInfuse))
        {
            _TileLightIntensity += amountToInfuse;
            WakeTileIfPossible();
            return 0;
        }
        // Tile doesn't have room for the entire amount
        else
        {
            int excessLight = (_TileLightIntensity + amountToInfuse) - _MaxTileLightIntensity;
            _TileLightIntensity = _MaxTileLightIntensity;
            WakeTileIfPossible();
            return excessLight;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Tile" && !_IsTileAwake)
        {
            WakeTileIfPossible();
        }
    }

    private void WakeTileIfPossible()
    {
        if (_TileLightIntensity >= _LightGenerationThreshold)
        {
            _IsTileAwake = true;
        }
    }

    private void GenerateLight()
    {
        _LightGenerationBuildUp += Time.deltaTime;
        if (_LightGenerationBuildUp >= _LightGenerationSpeed)
        {
            _TileLightIntensity++;
            _LightGenerationBuildUp -= _LightGenerationSpeed;

            UpdateTileColor();
        }
    }

    private void UpdateTileColor()
    {
        //_MinTileLightIntensity == _TileLightIntensity => should give 0 in equation
        //_MaxTileLightIntensity == _TileLightIntensity => should give 1 in equation
        // Adjust the span so the scale starts at '0' and then just divide with the scale's max to get percentage.
        float percentToInterpolate = (float)(_TileLightIntensity - _MinTileLightIntensity) / (_MaxTileLightIntensity - _MinTileLightIntensity);
        Color newColor = Color.Lerp(_OriginalColor, Color.white, percentToInterpolate);
        GetComponent<Renderer>().material.color = newColor;
    }

    /// <summary>
    /// Initialize the Tile with its x- & z-coordinates.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public void Init(int x, int z)
    {
        Init(x, z, 0);
    }
	
    /// <summary>
    /// Initializes the Tile with its x- & z-coordinates as well
    /// as sets the Tiles light intensity.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <param name="tileLightIntensity"></param>
    public void Init(int x, int z, int tileLightIntensity)
    {
        _CreationX = x;
        _CreationZ = z;
        _TileLightIntensity = tileLightIntensity;
    }
    
}
