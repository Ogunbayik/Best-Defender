using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string HORIZONTAL_INPUT = "Horizontal";

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float xRange;
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float maxAttackTimer;

    private Vector3 movementDirection;

    private float horizontalInput;
    private float maximumX;
    private float minimumX;
    private float attackTimer;
    void Start()
    {
        InitialSettings();
    }
    private void InitialSettings()
    {
        maximumX = xRange;
        minimumX = -xRange;
        attackTimer = maxAttackTimer;
    }

    void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    #region Attack
    private void HandleAttack()
    {
        var attakButton = Input.GetKey(KeyCode.Space);

        if (attackTimer <= 0f)
        {
            if (attakButton)
            {
                attackTimer = maxAttackTimer;
                CreateBullet();
            }
            else
                attackTimer = 0;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab);
        var bulletDirection = Vector3.forward;
        bullet.transform.position = attackPoint.position;
        bullet.GetComponent<MovePrefab>().SetPrefabMovement(bulletDirection ,bulletSpeed);
    }
    #endregion

    #region Movement
    private void HandleMovement()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL_INPUT);

        movementDirection = new Vector3(horizontalInput, 0f, 0f);
        movementDirection.Normalize();

        transform.Translate(movementDirection * movementSpeed * Time.deltaTime);

        SetMovementBorder();
    }
    private void SetMovementBorder()
    {
        if (transform.position.x >= maximumX)
            transform.position = new Vector3(maximumX, transform.position.y, transform.position.z);
        else if (transform.position.x <= minimumX)
            transform.position = new Vector3(minimumX, transform.position.y, transform.position.z);
    }
    #endregion
}
