using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public event EventHandler OnHit;

    private ScoreManager scoreManager;
    private BoxCollider boxCollider;
    private EnemyUI enemyUI;

    private enum States
    {
        Chase,
        Eating,
        Attack,
        GoingBack
    }

    private States currentState;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float bulletSpeed;
    [Header("Attack Settings")]
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private float attackDistance;
    [SerializeField] private float maxAttackTimer;
    [Header("Score Settings")]
    [SerializeField] private int maxScore;
    [SerializeField] private int minScore;
    [Header("Eat Settings")]
    [SerializeField] private int maxNumberOfEating;

    private int enemyScore;
    private int numberOfEating = 0;

    private float currentSpeed;
    private float attackTimer;

    private Transform castle;

    private Vector3 outSidePoint;
    private void Awake()
    {
        enemyUI = GetComponent<EnemyUI>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Start()
    {
        castle = FindObjectOfType<Castle>().transform;
        scoreManager = FindObjectOfType<ScoreManager>();
        currentState = States.Chase;
        currentSpeed = movementSpeed;

        enemyScore = UnityEngine.Random.Range(minScore, maxScore);
        enemyUI.OnGettingFull += EnemyUI_OnGettingFull;
        outSidePoint = new Vector3(transform.position.x + 30f, 0f, transform.position.z);
    }

    private void EnemyUI_OnGettingFull(object sender, EventArgs e)
    {
        numberOfEating++;
        currentState = States.Chase;
        currentSpeed = movementSpeed;
    }

    void Update()
    {
        switch(currentState)
        {
            case States.Chase:
                Chasing();
                break;
            case States.Attack:
                Attacking();
                break;
            case States.Eating:
                Eating();
                break;
            case States.GoingBack:
                GoingBack();
                break;
        }
    }
    private void Chasing()
    {
        if (numberOfEating >= maxNumberOfEating)
        {
            currentState = States.GoingBack;
            scoreManager.AddScore(enemyScore);
        }

        boxCollider.enabled = true;

        transform.position = Vector3.MoveTowards(transform.position, castle.position, currentSpeed * Time.deltaTime);
        var distanceBetweenCastle = Vector3.Distance(transform.position, castle.position);
        if (distanceBetweenCastle <= attackDistance)
            currentState = States.Attack;

    }

    private void Attacking()
    {
        if (numberOfEating >= maxNumberOfEating)
        {
            currentState = States.GoingBack;
            scoreManager.AddScore(enemyScore);
        }

        transform.LookAt(castle.position);

        if (attackTimer <= 0)
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.position = attackPosition.position;
            var movementDirection = castle.position - transform.position;
            bullet.GetComponent<Bullet>().SetBulletMovement(movementDirection, bulletSpeed);
            attackTimer = maxAttackTimer;
        }
        else
            attackTimer -= Time.deltaTime;
    }

    private void Eating()
    {
        boxCollider.enabled = false;
        currentSpeed = 0f;
    }

    private void GoingBack()
    {
        boxCollider.enabled = false;
        transform.position = Vector3.MoveTowards(transform.position, outSidePoint, currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var bullet = other.gameObject.GetComponent<Bullet>();

        if (bullet)
        {
            currentState = States.Eating;
            OnHit?.Invoke(this, EventArgs.Empty);
            Destroy(bullet.gameObject);
        }
    }

}
