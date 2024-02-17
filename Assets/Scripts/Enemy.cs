using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private ScoreManager scoreManager;

    private enum States
    {
        Chase,
        Attack
    }
    private States currentState;

    [Header("Settings")]
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private Transform attackPosition;

    private Transform castle;

    private float movementSpeed = 2f;
    private float bulletSpeed = 3f;
    private float attackDistance = 10f;
    private float maxAttackTimer = 5f;
    private float attackTimer = 0f;

    private int enemyScore;
    
    void Start()
    {
        castle = FindObjectOfType<Castle>().transform;
        scoreManager = FindObjectOfType<ScoreManager>();
        currentState = States.Chase;
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
        }
    }
    private void Chasing()
    {
        transform.position = Vector3.MoveTowards(transform.position, castle.position, movementSpeed * Time.deltaTime);
        var distanceBetweenCastle = Vector3.Distance(transform.position, castle.position);
        if (distanceBetweenCastle <= attackDistance)
            currentState = States.Attack;
    }

    private void Attacking()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        var bullet = other.gameObject.GetComponent<Bullet>();

        if (bullet)
        {
            Destroy(this.gameObject);
            Destroy(bullet.gameObject);

            var maxScore = 10;
            var minScore = 5;
            enemyScore = Random.Range(minScore, maxScore);
            scoreManager.AddScore(enemyScore);
        }
    }


}
