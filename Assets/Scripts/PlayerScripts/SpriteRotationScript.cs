using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotationScript : MonoBehaviour
{
    public float rotationSpeed;
    void Update()
    {
        transform.RotateAround(transform.forward, rotationSpeed * Time.deltaTime);
    }
}
