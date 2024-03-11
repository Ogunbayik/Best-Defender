using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Column : MonoBehaviour
{
    public event EventHandler OnDestroyed;

    [SerializeField] private ParticleSystem smokeParticle;

    private bool isDestroyed = false;

    private void Start()
    {
        OnDestroyed += Column_OnDestroyed;
    }

    private void Column_OnDestroyed(object sender, EventArgs e)
    {
        Debug.Log("Destroyed!!" + gameObject.name);
    }

    private void Update()
    {
        if(isDestroyed)
        {
            OnDestroyed?.Invoke(this, EventArgs.Empty);
            isDestroyed = false;
        }
    }

    public void DestroyColumn()
    {
        gameObject.SetActive(false);
    }

    public void Destroyed()
    {
        isDestroyed = true;
    }
}
