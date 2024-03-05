using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    void Update()
    {
        RotateAround();
    }

    private void RotateAround()
    {
        var desiredRotate = -Vector3.right;
        transform.Rotate(desiredRotate * rotateSpeed);
    }
}
