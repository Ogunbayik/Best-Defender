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
    [SerializeField] private Image imageDo;
    [SerializeField] private Image eatingBar;
    [SerializeField] private float maxFillAmount;
    [SerializeField] private float decreaseSpeed;
    [SerializeField] private Sprite eatingSprite;
    [SerializeField] private Sprite happySprite;

    private bool isEating;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    void Start()
    {
        enemy.OnHit += Enemy_OnHit;
        OnGettingFull += EnemyUI_OnGettingFull;

        BarActivate(false);
        DoImageActivate(false, null);
        eatingBar.fillAmount = maxFillAmount;
        isEating = false;
    }

    private void EnemyUI_OnGettingFull(object sender, EventArgs e)
    {
        DoImageActivate(false, null);
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
            DoImageActivate(true, eatingSprite);
            eatingBar.fillAmount -= Time.deltaTime * decreaseSpeed;

            if (eatingBar.fillAmount <= 0)
            {
                IsEating(false);
                eatingBar.fillAmount = maxFillAmount;
                OnGettingFull?.Invoke(this, EventArgs.Empty);
            }
        }
        else
        {
            BarActivate(false);
        }
    }

    public void IsEating(bool isEat)
    {
        isEating = isEat;
    }

    public void DoImageActivate(bool isActive, Sprite sprite)
    {
        imageDo.gameObject.SetActive(isActive);
        imageDo.sprite = sprite;
    }

    private void BarActivate(bool isActive)
    {
        bar.gameObject.SetActive(isActive);
    }

    public Sprite GetHappySprite()
    {
        return happySprite;
    }



}
