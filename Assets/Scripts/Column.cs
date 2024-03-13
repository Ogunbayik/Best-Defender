using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Column : MonoBehaviour
{
    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private ParticleSystem smokeParticle;
    public void DestroyColumn(Vector3 smokePosition)
    {
        var smoke = Instantiate(smokeParticle);
        smoke.transform.position = smokePosition + offsetPosition;

        Debug.Log("Destroyed! " + gameObject.name);
        gameObject.SetActive(false);

        var destroyTime = 1f;
        Destroy(smoke, destroyTime);
    }
}
