using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Duplicator : MonoBehaviour
{
    public Vector3 mimicDimensions = new Vector3(10, 10, 10);
    public float spacing = 50;
    public GameObject mimicPrefab;

    // Use this for initialization
    void Start ()
    {
        int halfX = (int)(mimicDimensions.x / 2);
        int halfY = (int)(mimicDimensions.y / 2);
        int halfZ = (int)(mimicDimensions.z / 2);

        for (int k = 0; k < mimicDimensions.z; k++)
        {
            float zOffset = spacing * (k - halfZ);

            for (int j = 0; j < mimicDimensions.y; j++)
            {
                float yOffset = spacing * (j - halfY);

                for (int i = 0; i < mimicDimensions.x; i++)
                {
                    if ((i == halfX) && (j == halfY) && (k == halfZ))
                    {
                        continue;
                    }

                    float xOffset = spacing * (i - halfX);

                    GameObject mimic = CreateMimic();
                    Vector3 offset = new Vector3(xOffset, yOffset, zOffset);
                    Mimic mimicScript = mimic.GetComponent<Mimic>();
                    mimicScript.offset = offset;
                    mimicScript.target = gameObject.transform;
                }
            }
        }
    }

    GameObject CreateMimic()
    {
        GameObject mimic = new GameObject("Mimic " + gameObject.name);
        mimic.AddComponent<Mimic>();
        MeshFilter meshFilter = mimic.AddComponent<MeshFilter>();
        FieldInfo[] fields = meshFilter.GetType().GetFields();
        
        foreach (FieldInfo field in fields)
        {
            field.SetValue(meshFilter, field.GetValue(GetComponent<MeshFilter>()));
        }

        return mimic;
    }
}
