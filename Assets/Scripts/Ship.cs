using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    Transform frontFloatTransform;
    [SerializeField]
    Transform backFloatTransform;
    [SerializeField]
    float offset;

    private void Update() {
        transform.rotation = Quaternion.FromToRotation(frontFloatTransform.position, backFloatTransform.position);
        transform.position = new Vector3(transform.position.x,frontFloatTransform.position.y+offset,transform.position.z);
    }
}
