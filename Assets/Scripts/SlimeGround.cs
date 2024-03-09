using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGround : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject[] allColumns;

    private int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;

        for (int i = 0; i < allColumns.Length; i++)
        {
            allColumns[i].SetActive(true);
        }
    }

    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        var enemyBullet = other.gameObject.GetComponent<MovePrefab>();
        var bulletDamage = 50;

        if (enemyBullet)
        {
            DecreseHealth(bulletDamage);

            if (currentHealth <= 0)
                DestructionCastle();
        }
    }
    private void DecreseHealth(int health)
    {
        currentHealth -= health;
    }

    private void DestructionCastle()
    {
        Debug.Log("Game is over");
        //Animator castle
        //Add Particle Effect
    }
}
