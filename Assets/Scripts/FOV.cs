using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    private void Start()
    {
        float fov = 90f;
        Vector3 origin = Vector3.zero;
        int rayCount = 2;
        float angle = 0f;
        float angleIncrease = fov / rayCount;
        float viewDistance = 50f;
        Mesh mesh = new Mesh();
        mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] vertices = new Vector3[rayCount+1+1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount*3];

        vertices[0] = origin;

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        

        int triangleIndex = 0;
        int vertexIndex = 1;
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 vertex = origin + AngleToVector(angle) * viewDistance;
            vertices[vertexIndex] = vertex;
            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    private Vector3 AngleToVector(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
