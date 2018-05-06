using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour {

    public GameObject mTilePrefab;
    public int mSizeX = 3;
    public int mSizeZ = 3;

	// Use this for initialization
	void Start ()
	{
        Vector3 prefabOriginScale;
        for (int x = 0; x < mSizeX; x++)
        {
            for (int z = 0; z < mSizeZ; z++)
            {
                prefabOriginScale = mTilePrefab.transform.localScale;
                float xPos = (x - mSizeX / 2 + (mSizeX % 2 == 0 ? 0.5f : 0f)) * prefabOriginScale.x; // - (mSizeX / 2) * (prefabOriginScale.x / 2);
                float zPos = (z - mSizeZ / 2 + (mSizeZ % 2 == 0 ? 0.5f : 0f)) * prefabOriginScale.z; // - (mSizeX / 2) * (prefabOriginScale.z / 2);
                GameObject createdPrefab = Instantiate(mTilePrefab, new Vector3(xPos, mTilePrefab.transform.position.y, zPos), Quaternion.identity);
                createdPrefab.transform.SetParent(gameObject.transform);
                var prefabScript = createdPrefab.GetComponent<TileData>();
                if (x == 0 && z == 0)
                {
                    prefabScript.Init(x, z, 20);
                }
                else
                {
                    prefabScript.Init(x, z);
                }
            }
        }
	}
}
