using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains a flow field that is a 2D array of vector3s and methods to sample said field
/// Will Dickinson
/// </summary>
public class FlowField : MonoBehaviour
{

    public Vector3[,] field;
    public Material fieldColor;
    public float scale = 25;

    private ExerciseManager managerScript;

    // Use this for initialization
    void Start()
    {
        managerScript = GetComponent<ExerciseManager>();


        field = new Vector3[managerScript.maxX - managerScript.minX, managerScript.maxZ - managerScript.minZ];

        for (int x = 0; x < field.GetLength(0); x++)
        {
            for (int z = 0; z < field.GetLength(1); z++)
            {
                Vector3 dirVec = Vector3.zero;
                float turnAngle = 360 * Mathf.PerlinNoise((float)x / scale, (float)z / scale);
                dirVec = Quaternion.Euler(0, turnAngle, 0) * Vector3.right;
                field[x, z] = dirVec;
            }
        }

    }

    /// <summary>
    /// Returns the direction to align at this position
    /// </summary>
    /// <param name="pos">The current position</param>
    /// <returns></returns>
    public Vector3 GetFlowDirection(Vector3 pos)
    {
        //Convert to flow field coordinates
        Vector2 roundedPos = new Vector3((int)pos.x + managerScript.maxX, (int)pos.z + managerScript.maxZ);

        if(roundedPos.x >= 0 && roundedPos.x < field.GetLength(0) && roundedPos.y >= 0 && roundedPos.y < field.GetLength(1))
            return field[(int)roundedPos.x, (int)roundedPos.y];

        return Vector3.zero;
    }

    void OnRenderObject()
    {
        if (managerScript.debug)
        {
            fieldColor.SetPass(0);

            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int z = 0; z < field.GetLength(1); z++)
                {
                    Vector3 pos = new Vector3(x - managerScript.maxX, 15, z - managerScript.maxZ);
                    GL.Begin(GL.LINES);
                    GL.Vertex(pos);
                    GL.Vertex(pos + field[x, z]);
                    GL.End();
                }
            }
        }
    }
}
