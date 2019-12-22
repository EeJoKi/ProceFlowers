using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Flower : MonoBehaviour
{
    private Mesh mesh;

    public int trunkWidth, trunkHeigth;
    public float slowTime;
    public Vector3[] vertices;

    public float radiusPerTop;

    public float radius = 3f;
    float oriRadius;

    private void Awake()
    {
        oriRadius = radius;
        StartCoroutine(GenerateTrunk());
    }

    private IEnumerator GenerateTrunk()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        WaitForSeconds wait = new WaitForSeconds(slowTime);

        vertices = new Vector3[trunkHeigth * trunkWidth + 4 * trunkWidth];
        float theta = 0f;

        //How can I draw a circle in Unity3D?
        //https://stackoverflow.com/questions/13708395/how-can-i-draw-a-circle-in-unity3d

        //TRUNK
        for (int i = 0, y = 0; y <= trunkHeigth; y++)
        {
            radius = oriRadius + radiusPerTop * ((float)y/trunkHeigth);

            for (int x = 0; x <= trunkWidth; x++, i++)
            {
                theta += (2f / trunkWidth) * Mathf.PI;

                float newX = radius * Mathf.Cos(theta);
                float newZ = radius * Mathf.Sin(theta);

                vertices[i] = new Vector3(newX, y, newZ);
                yield return wait;
            }
        }

        //FLOWER CENTER
        for (int i = 0, y = 0; y <= 3; y++)
        {
            if(i == 2)
            {
                radius = 0;
            }
            

            for (int x = 0; x <= trunkWidth; x++, i++)
            {
                theta += (2f / trunkWidth) * Mathf.PI;

                float newX = radius * Mathf.Cos(theta);
                float newZ = radius * Mathf.Sin(theta);

                vertices[i] = new Vector3(y, trunkHeigth + newX, newZ);
                yield return wait;
            }
        }




        mesh.vertices = vertices;

        int[] triangles = new int[trunkWidth * trunkHeigth * 6];
        for (int ti = 0, vi = 0, y = 0; y < trunkHeigth; y++, vi++)
        {
            for (int x = 0; x < trunkWidth; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + trunkWidth + 1;
                triangles[ti + 5] = vi + trunkWidth + 2;
                mesh.triangles = triangles;
                yield return wait;
            }
        }
        
    

    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }

        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }

}

