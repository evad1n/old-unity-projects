using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLeaders : MonoBehaviour {
    /// <summary>
    /// Will Dickinson
    /// Generates gameobjects with varying scales using Gaussian-normal distribution
    /// </summary>

    public GameObject leader;
    [Range(8, 12)]
    public int numberOfObjects = 10;
    public float offset = 0.5f;
    public int hordeLowerBound;
    public int hordeZ;
    public float mean;
    public float stdDev;
    /// <summary>
    /// Deviation along the line they form in
    /// </summary>
    public int zDeviation;

    private TerrainData myTerrainData;
    private Terrain terrain;
    private int xLength;

    // Use this for initialization
    void Start()
    {
        myTerrainData = gameObject.GetComponent<TerrainCollider>().terrainData;
        terrain = gameObject.GetComponent<Terrain>();



        Generate();
    }

    /// <summary>
    /// Generates objects in a line with random values for scale based off a Gaussian distribution
    /// </summary>
    void Generate()
    {
        for(int i = 0; i < numberOfObjects; i++)
        {
            int xPos = (hordeLowerBound - (3*i)) - 1;
            int zPos = Random.Range(hordeZ - zDeviation, hordeZ + zDeviation);
            float yPos = terrain.SampleHeight(new Vector3(xPos, 0, zPos));

            Vector3 pos = new Vector3(xPos, yPos + offset, zPos);

            GameObject obj = (GameObject)Instantiate(leader, pos, Quaternion.identity);
            float size = Gaussian(mean, stdDev);
            Vector3 scale = new Vector3(size, Gaussian(mean, stdDev), size);
            obj.transform.localScale += scale;
        }
    }

    /// <summary>
    /// Returns a random float that follows the Gaussian distribution 
    /// </summary>
    /// <param name="mean">The average value for the distribution</param>
    /// <param name="stdDev">The average difference from the mean for the distribution</param>
    /// <returns></returns>
    float Gaussian(float mean, float stdDev)
    {
        float val1 = Random.Range(0f, 1f);
        float val2 = Random.Range(0f, 1f);
        float gaussValue = Mathf.Sqrt(-2.0f * Mathf.Log(val1)) * Mathf.Sin(2.0f * Mathf.PI * val2);
        return mean + stdDev * gaussValue;
    }
}
