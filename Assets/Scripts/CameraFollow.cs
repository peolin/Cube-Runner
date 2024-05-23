using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // target to follow
    public Vector3 offset; // keeps the camera at a certain distance
    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
    }
}
