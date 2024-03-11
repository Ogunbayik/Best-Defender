using UnityEngine;

public class SlimeGround : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject[] allColumns;
    [SerializeField] private GameObject ground;
    [SerializeField] private float collapseSpeed;

    private Column currentColumn;
    private int currentHealth;
    private int lastColumnIndex;
    void Start()
    {
        currentColumn = null;
        currentHealth = maxHealth;
        lastColumnIndex = allColumns.Length - 1;

        for (int i = 0; i < allColumns.Length; i++)
        {
            allColumns[i].SetActive(true);
        }
    }

    private void CollapseTheFloor()
    {
        ground.transform.localPosition += Vector3.down * collapseSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        var axe = other.gameObject.GetComponent<Axe>();
        var obj = axe.gameObject.GetComponentInParent<MovePrefab>();
        var axeDamage = 10;

        if (axe)
        {
            DecreaseHealth(axeDamage);

            Destroy(obj.gameObject);

            SetColumnActivate();
        }
    }
    private void SetColumnActivate()
    {
        if (currentHealth < 100 && 80 <= currentHealth)
        {
            currentColumn = allColumns[lastColumnIndex].GetComponent<Column>();
            currentColumn.Destroyed();
        }
        else if (currentHealth < 80 && 60 <= currentHealth)
        {
            currentColumn = allColumns[lastColumnIndex - 1].GetComponent<Column>();
            currentColumn.DestroyColumn();
        }
        else if (currentHealth < 60 && 40 <= currentHealth)
        {
            currentColumn = allColumns[lastColumnIndex - 2].GetComponent<Column>();
            currentColumn.DestroyColumn();
        }
        else if (currentHealth < 40 && 20 <= currentHealth)
        {
            currentColumn = allColumns[lastColumnIndex - 3].GetComponent<Column>();
            currentColumn.DestroyColumn();
        }
        else if (currentHealth <= 0)
        {
            currentColumn = allColumns[lastColumnIndex - 4].GetComponent<Column>();
            currentColumn.DestroyColumn();

            CollapseTheFloor();
        }
    }
    private void DecreaseHealth(int health)
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
