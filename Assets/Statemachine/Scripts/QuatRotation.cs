using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuatRotation : MonoBehaviour
{
    public float x;
    public float y;
    public float z;
    public float w;
    void Update()
    {
        transform.rotation = new Quaternion(x, y, z, w); 
    }
}
