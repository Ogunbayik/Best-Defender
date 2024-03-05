using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePrefab : MonoBehaviour
{
    private float movementSpeed;

    private Vector3 movementDirection;

    void Update()
    {
        SetPrefabMovement(movementDirection, movementSpeed);
    }

    public void SetPrefabMovement(Vector3 direction, float speed)
    {
        movementDirection = direction;
        movementSpeed = speed;

        transform.Translate(direction * speed * Time.deltaTime);
    }
}
