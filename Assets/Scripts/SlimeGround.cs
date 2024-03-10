using UnityEngine;

public class SlimeGround : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject[] allColumns;
    [SerializeField] private GameObject ground;
    [SerializeField] private float collapseSpeed;

    private int currentHealth;
    private int lastColumnIndex;
    void Start()
    {
        currentHealth = maxHealth;
        lastColumnIndex = allColumns.Length - 1;

        for (int i = 0; i < allColumns.Length; i++)
        {
            allColumns[i].SetActive(true);
        }
    }

    private void Update()
    {
        SetColumnActivate();

    }

    private void SetColumnActivate()
    {
        if (currentHealth < 100 && 80 <= currentHealth)
        {
            allColumns[lastColumnIndex].GetComponent<Column>().DestroyColumn();
        }
        else if (currentHealth < 80 && 60 <= currentHealth)
        {
            allColumns[lastColumnIndex - 1].GetComponent<Column>().DestroyColumn();
        }
        else if (currentHealth < 60 && 40 <= currentHealth)
        {
            allColumns[lastColumnIndex - 2].GetComponent<Column>().DestroyColumn();
        }
        else if (currentHealth < 40 && 20 <= currentHealth)
        {
            allColumns[lastColumnIndex - 3].GetComponent<Column>().DestroyColumn();
        }
        else if (currentHealth <= 0)
        {
            allColumns[lastColumnIndex - 4].GetComponent<Column>().DestroyColumn();
            CollapseTheFloor();
        }
    }

    private void CollapseTheFloor()
    {
        ground.transform.localPosition += Vector3.down * collapseSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        var axe = other.gameObject.GetComponent<Axe>();
        var axeDamage = 10;

        if (axe)
        {
            DecreaseHealth(axeDamage);
            Destroy(axe.gameObject);

            if (currentHealth <= 0)
                DestructionCastle();
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
