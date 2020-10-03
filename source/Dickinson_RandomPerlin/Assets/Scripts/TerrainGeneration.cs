using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour {

    private TerrainData myTerrainData;

    public Vector3 worldSize = new Vector3(200, 50, 200);
    public int resolution = 129;
    public float timestep = 0.05f;

	// Use this for initialization
	void Start () {
        myTerrainData = gameObject.GetComponent<TerrainCollider>().terrainData;
        myTerrainData.size = worldSize;
        myTerrainData.heightmapResolution = resolution;

        GenerateTerrain();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            timestep+=0.05f;
            GenerateTerrain();
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            timestep-=0.05f;
            GenerateTerrain();
        }
    }

    private void GenerateTerrain()
    {
        float[,] heightArray = new float[resolution, resolution];

        for(int i = 0; i < resolution; i++)
        {
            for(int j = 0; j < resolution; j++)
            {
                heightArray[i, j] = Mathf.PerlinNoise((float)i/(float)resolution * timestep, (float)j/(float)resolution * timestep);
            }
        }

        myTerrainData.SetHeights(0, 0, heightArray);
    }

    private void OnGUI()
    {
        GUI.color = Color.black;

        GUI.skin.box.fontSize = 20;

        GUI.Box(new Rect(200, 100, 200, 120), "Hold the '1' key to increase the timestep and hold the '2' key to decrease the timestep");

        GUI.skin.box.wordWrap = true;
    }
}
