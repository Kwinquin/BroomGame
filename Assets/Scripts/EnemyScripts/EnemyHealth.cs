using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    //so then when you want to call for th eenemy to take damage it will be
    //enemy.GetComponent<EnemyHealth>().TakeDamage(1);
    //you can also compare tag


    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("ouch");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("I died!");
    }

    //this part is just for testing
    void OnMouseDown()
    {
        GetComponent<EnemyHealth>().TakeDamage(1);
    }



}
