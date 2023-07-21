using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WaterArea : MonoBehaviour
{
    public bool debugMode = false;

    [Header("Mesh properties")]
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    public Material material;
    [Header("Perlin noise properties")]
    public float xOrg;
    public float yOrg;
    public int scale;
    [Header("Wave properties")]
    public int size;
    public float speed;
    public float waveSize;

    private void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = material;
        CreateShape();
    }

    private void Update() {
        MoveWaves();
    }

    private void MoveWaves() {
        xOrg += 1f * speed * Time.deltaTime;
        yOrg += 1f * speed * Time.deltaTime;

        for(int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(vertices[i].x, CalculateSample(new Vector2(vertices[i].x, vertices[i].z))*waveSize,vertices[i].z);
        }
        UpdateMesh();
    }

    private void CreateShape() {
        vertices = new Vector3[(size+1)*(size+1)];

        for (int i = 0, x = 0; x <= size; x++)
        {
            for (int z = 0; z <= size; z++)
            {
                vertices[i] = new Vector3(x,0,z);
                i++;
            }
        }
        triangles = new int[size * size * 6];

        int vert = 0;
        int tris = 0;
        
        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                triangles[tris + 5] = vert + 0;
                triangles[tris + 4] = vert + size + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 1] = vert + size + 1;
                triangles[tris + 0] = vert + size + 2;

                vert++;
                tris+=6;
            }
            vert++;
        }        
    }

    private void UpdateMesh() {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
    
    private float CalculateSample(Vector2 current) {
        float xCoord = xOrg + current.x / size * scale;
        float yCoord = yOrg + current.y / size * scale;
        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    private void OnDrawGizmos() {
        if(vertices != null && debugMode)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Gizmos.DrawSphere(vertices[i], .1f);
            }
        }
    }
}
