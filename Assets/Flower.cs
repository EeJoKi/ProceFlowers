using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Flower : MonoBehaviour
{
    public int trunkWidth, trunkHeight;
    public float slowTime;
    private Vector3[] vertices;

    private void Awake()
    {
        Generate();
    }

    private IEnumerator Generate()
    {
        WaitForSeconds wait = new WaitForSeconds(slowTime);
        vertices = new Vector3[(trunkWidth + 1) * (trunkHeight + 1)];
        for (int i = 0, y = 0; y <= trunkHeight; y++)
        {
            for (int x = 0; x <= trunkWidth; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
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