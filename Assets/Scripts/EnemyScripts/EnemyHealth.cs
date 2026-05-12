using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int currentHealth;

    public void Initialize(int maxHealth)
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Ouch");

        if (currentHealth <= 0) { }
            Die();

    }

    void Die()
    {
        Debug.Log("Rip");
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        TakeDamage(1);
    }
}
