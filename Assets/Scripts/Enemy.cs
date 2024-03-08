using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public event EventHandler OnHit;

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
    [SerializeField] private Transform allParts;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float axeSpeed;
    [Header("Attack Settings")]
    [SerializeField] private Transform axePrefab;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private float maxAttackDistance;
    [SerializeField] private float minAttackDistance;
    [SerializeField] private float maxAttackTimer;
    
    [Header("Score Settings")]
    [SerializeField] private int maxScore;
    [SerializeField] private int minScore;
    [Header("Eat Settings")]
    [SerializeField] private int maxNumberOfEating;

    private int enemyScore;
    private int numberOfEating = 0;

    private float attackDistance;
    private float currentSpeed;
    private float attackTimer;
    private float lookOffsetY;

    private Transform castle;

    private Vector3 outSidePoint;
    private Vector3 movePosition;
    private void Awake()
    {
        enemyUI = GetComponent<EnemyUI>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Start()
    {
        InitiliazeSettings();
    }

    private void InitiliazeSettings()
    {
        lookOffsetY = transform.localScale.y / 2;
        castle = FindObjectOfType<Castle>().transform;
        movePosition = new Vector3(castle.position.x, castle.position.y, transform.position.z);
        currentState = States.Chase;
        currentSpeed = movementSpeed;
        attackDistance = UnityEngine.Random.Range(minAttackDistance, maxAttackDistance);

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
        HandleStates();
    }

    private void HandleStates()
    {
        switch (currentState)
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
            var happy = enemyUI.GetHappySprite();
            enemyUI.DoImageActivate(true, happy);

            currentState = States.GoingBack;
            ScoreManager.instance.AddScore(enemyScore);
        }

        boxCollider.enabled = true;
        
        transform.position = Vector3.MoveTowards(transform.position, movePosition, currentSpeed * Time.deltaTime);
        var distanceBetweenCastle = Vector3.Distance(transform.position, castle.position);
        if (distanceBetweenCastle <= attackDistance)
            currentState = States.Attack;

    }

    private void Attacking()
    {
        if (numberOfEating >= maxNumberOfEating)
        {
            currentState = States.GoingBack;
            ScoreManager.instance.AddScore(enemyScore);
        }

        var desiredLookY = new Vector3(0f, lookOffsetY, 0f);
        allParts.transform.LookAt(castle.position + desiredLookY);

        if (attackTimer <= 0)
        {
            var axe = Instantiate(axePrefab);
            axe.transform.position = attackPosition.position;
            var movementDirection = castle.position - transform.position;
            axe.GetComponent<MovePrefab>().SetPrefabMovement(movementDirection, axeSpeed);
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
        transform.LookAt(outSidePoint);
        transform.position = Vector3.MoveTowards(transform.position, outSidePoint, currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerBullet = other.gameObject.GetComponent<PlayerLogBoat>();

        if (playerBullet)
        {
            currentState = States.Eating;
            OnHit?.Invoke(this, EventArgs.Empty);
            Destroy(playerBullet.gameObject);
        }
    }

}
