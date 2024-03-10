using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    public void DestroyColumn()
    {
        //Create Smoke Particle
        gameObject.SetActive(false);
        Debug.Log("Destroyed!!" + gameObject.name);
    }
}
