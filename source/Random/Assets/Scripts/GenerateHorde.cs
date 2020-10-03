using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateHorde : MonoBehaviour
{    /// <summary>
     /// Will Dickinson
     /// Generates gameobjects with with non-uniform random positioning to simulate a horde effect
     /// </summary>

    public GameObject horde;
    [Range(30, 200)]
    public int numberOfObjects = 50;
    public float offset = 0.5f;

    //Bounds of the horde
    public int xLowerBound;
    public int xUpperBound;
    public int zLowerBound;
    public int zUpperBound;

    private TerrainData myTerrainData;
    private Terrain terrain;
    /// <summary>
    /// The width of the segmented density areas along the x-axis
    /// </summary>
    private int xLength;

    // Use this for initialization
    void Start()
    {
        myTerrainData = gameObject.GetComponent<TerrainCollider>().terrainData;
        terrain = gameObject.GetComponent<Terrain>();

        xLength = (xUpperBound - xLowerBound) / 4;

        Generate();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Generates random objects with positions that are non-uniform randomly sectioned into different density compartments along the x-axis
    /// </summary>
    void Generate()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            int xMin;
            int xMax;
            float r = Random.Range(0f, 1f);

            if (r < 0.5)
            {
                xMin = xLowerBound;
                xMax = xMin + xLength;
            }
            else if(r < 0.8)
            {
                xMin = xLowerBound + xLength;
                xMax = xMin + xLength;
            }
            else if(r < 0.95)
            {
                xMin = xLowerBound + (2 * xLength);
                xMax = xMin + xLength;
            }
            else
            {
                xMin = xLowerBound + (3 * xLength);
                xMax = xUpperBound;
            }


            int xPos = Random.Range(xMin, xMax);
            int zPos = Random.Range(zLowerBound, zUpperBound);
            float yPos = terrain.SampleHeight(new Vector3(xPos, 0, zPos));

            Vector3 pos = new Vector3(xPos, yPos + offset, zPos);

            Instantiate(horde, pos, Quaternion.identity);
        }
    }
}