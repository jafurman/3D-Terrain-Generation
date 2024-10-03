using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    Color[] colors;

    public int xSize = 20;
    public int zSize = 20;

    public Gradient gradient;

    private float minTerrainHeight;
    private float maxTerrainHeight;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        createShape();
        updateMesh();
    }
    void createShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        System.Random random = new System.Random();
        float baseRandX = (float)random.NextDouble() * 100f;
        float baseRandZ = (float)random.NextDouble() * 100f;

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float offsetX = (float)random.NextDouble() * 0.1f;
                float offsetZ = (float)random.NextDouble() * 0.1f;

                float y = Mathf.PerlinNoise((x * 0.3f) + baseRandX + offsetX, (z * 0.3f) + baseRandZ + offsetZ) * 2f;
                y += Mathf.PerlinNoise((x * 0.1f) + baseRandX + offsetX, (z * 0.1f) + baseRandZ + offsetZ) * 4f;
                y -= Mathf.PerlinNoise((x * 0.05f) + baseRandX + offsetX, (z * 0.05f) + baseRandZ + offsetZ) * 3f;
                y *= 2.6f;

                if (y > maxTerrainHeight)
                {
                    maxTerrainHeight = y;
                }
                if (y < minTerrainHeight)
                {
                    minTerrainHeight = y;
                }

                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            
            for (int x = 0; x < xSize; x++ )
            {

            triangles[tris + 0] = vert + 0;
            triangles[tris + 1] = vert + xSize + 1;
            triangles[tris + 2] = vert + 1;
            triangles[tris + 3] = vert + 1;
            triangles[tris + 4] = vert + xSize + 1;
            triangles[tris + 5] = vert + xSize + 2;

            vert++;
            tris += 6;

            }
            vert += 1;
        }

        colors = new Color[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, vertices[i].y);
                colors[i] = gradient.Evaluate(height);
                i++;
            }
        }

    }

    void updateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals();

    }

}
