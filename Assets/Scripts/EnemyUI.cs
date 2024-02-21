using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyUI : MonoBehaviour
{
    public event EventHandler OnGettingFull;

    private Enemy enemy;

    [Header("UI Settings")]
    [SerializeField] private Image bar;
    [SerializeField] private Image eatingBar;
    [SerializeField] private float maxFillAmount;
    [SerializeField] private float decreaseSpeed;

    private bool isEating;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    void Start()
    {
        enemy.OnHit += Enemy_OnHit;

        BarActivate(false);
        eatingBar.fillAmount = maxFillAmount;
        isEating = false;
    }

    private void Enemy_OnHit(object sender, System.EventArgs e)
    {
        IsEating(true);
        BarActivate(true);
    }

    void Update()
    {
        if (isEating)
        {
            eatingBar.fillAmount -= Time.deltaTime * decreaseSpeed;

            if (eatingBar.fillAmount <= 0)
            {
                IsEating(false);
                BarActivate(false);
                eatingBar.fillAmount = maxFillAmount;
                OnGettingFull?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void IsEating(bool isEat)
    {
        isEating = isEat;
    }

    private void BarActivate(bool isActive)
    {
        bar.gameObject.SetActive(isActive);
    }





}
