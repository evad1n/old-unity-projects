using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour {
    /// <summary>
    /// Will Dickinson
    /// Generates a heightmap for a terrain using Perlin Noise
    /// </summary>
    /// 
    private TerrainData myTerrainData;

    public Vector3 worldSize = new Vector3(200, 50, 200);
    public int resolution = 129;
    public float scale = 2;

	// Use this for initialization
	void Start () {
        myTerrainData = gameObject.GetComponent<TerrainCollider>().terrainData;
        myTerrainData.size = worldSize;
        myTerrainData.heightmapResolution = resolution;

        GenerateTerrain();
    }
	
	// Update is called once per frame
	void Update () {

    }

    /// <summary>
    /// Creates a heightmap for a terrain using Perlin Noise
    /// </summary>
    private void GenerateTerrain()
    {
        float[,] heightArray = new float[resolution, resolution];

        for(int i = 0; i < resolution; i++)
        {
            for(int j = 0; j < resolution; j++)
            {
                heightArray[i, j] = Mathf.PerlinNoise((float)i/(float)resolution * scale, (float)j/(float)resolution * scale);
            }
        }

        myTerrainData.SetHeights(0, 0, heightArray);
    }
}
