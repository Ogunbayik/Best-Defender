using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    private int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }
    private void OnTriggerEnter(Collider other)
    {
        var enemyBullet = other.gameObject.GetComponent<Bullet>();
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
