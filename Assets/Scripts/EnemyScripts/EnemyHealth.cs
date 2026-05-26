using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int currentHealth;

    public MonsterWave waveManager;

    void Start()
    {
        //this is working, don't move it
        waveManager = GameObject.FindWithTag("Manager").GetComponent<MonsterWave>();
    }


    public void Initialize(int maxHealth)
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Enemy Ouch");

        if (currentHealth <= 0) 
        { 
            Die();
        }
            
    }

    //void OnMouseDown()
    //{
    //    TakeDamage(1);
    //}

    void Die()
    {
        if (waveManager != null)
            waveManager.EnemyDied();

        Debug.Log("Enemy Rip");

        Destroy(gameObject);
    }
}
