using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private Image eatingBar;
    [SerializeField] private float maxFillAmount;
    [SerializeField] private float decreaseSpeed;

    private bool isEating;
    void Start()
    {
        BarActivate(false);
        eatingBar.fillAmount = maxFillAmount;
        isEating = false;
    }

    void Update()
    {
        if (isEating)
            eatingBar.fillAmount -= Time.deltaTime * decreaseSpeed;
    }

    public void IsEating(bool isEat)
    {
        isEating = isEat;
        BarActivate(isEat);
    }

    private void BarActivate(bool isActive)
    {
        bar.gameObject.SetActive(isActive);
    }





}
