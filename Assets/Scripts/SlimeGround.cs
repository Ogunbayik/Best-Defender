using UnityEngine;
using System;

public class SlimeGround : MonoBehaviour
{
    public event EventHandler OnDestroyed;

    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject[] allColumns;

    private Column currentColumn;
    private int currentHealth;
    private int lastColumnIndex;
    void Start()
    {
        currentHealth = maxHealth;
        lastColumnIndex = allColumns.Length - 1;
        currentColumn = allColumns[lastColumnIndex].GetComponent<Column>();

        Debug.Log(currentColumn.transform.localPosition);

        for (int i = 0; i < allColumns.Length; i++)
        {
            allColumns[i].SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var axe = other.gameObject.GetComponent<Axe>();
        var obj = axe.gameObject.GetComponentInParent<MovePrefab>();
        var axeDamage = 5;

        if (axe)
        {
            DecreaseHealth(axeDamage);

            Destroy(obj.gameObject);

            SetColumnActivate();
        }
    }
    private void SetColumnActivate()
    {
        if (currentHealth == 80)
        {
            currentColumn.DestroyColumn(currentColumn.transform.position);
            currentColumn = allColumns[lastColumnIndex - 1].GetComponent<Column>();
        }
        else if (currentHealth == 60)
        {
            currentColumn.DestroyColumn(currentColumn.transform.position);
            currentColumn = allColumns[lastColumnIndex - 2].GetComponent<Column>();
        }
        else if (currentHealth == 40)
        {
            currentColumn.DestroyColumn(currentColumn.transform.position);
            currentColumn = allColumns[lastColumnIndex - 3].GetComponent<Column>();
        }
        else if (currentHealth == 20)
        {
            currentColumn.DestroyColumn(currentColumn.transform.position);
            currentColumn = allColumns[lastColumnIndex - 4].GetComponent<Column>();
        }
        else if (currentHealth <= 0)
        {
            currentColumn.DestroyColumn(currentColumn.transform.position);
            OnDestroyed?.Invoke(this, EventArgs.Empty);
        }
    }
    private void DecreaseHealth(int health)
    {
        currentHealth -= health;
    }
}
