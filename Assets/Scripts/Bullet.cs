using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float movementSpeed;
    private Vector3 movementDirection;

    void Update()
    {
        SetBulletMovement(movementDirection, movementSpeed);
    }

    public void SetBulletMovement(Vector3 direction, float speed)
    {
        movementDirection = direction;
        movementSpeed = speed;

        transform.Translate(direction * speed * Time.deltaTime);
    }
}
