using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public Vector3 closest;
    private void Update() {
        transform.position = new Vector3(transform.position.x,GetFloatingPoint(),transform.position.z);
    }
    
    private float GetFloatingPoint() {
        Vector3[] vertices = FindObjectOfType<WaterArea>().vertices;
        int indexOfMatch = 0;
        for (int i = 0; i < vertices.Length; i++){
            if(Mathf.Abs(transform.position.x - vertices[i].x) < Mathf.Abs(transform.position.x - vertices[indexOfMatch].x)
            && Mathf.Abs(transform.position.z - vertices[i].z) < Mathf.Abs(transform.position.z - vertices[indexOfMatch].z))
            {
                indexOfMatch = i;
            }
        }
        closest = vertices[indexOfMatch];   

        return vertices[indexOfMatch].y;
    }
}
