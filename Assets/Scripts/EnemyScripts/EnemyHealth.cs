using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int currentHealth;

    //public MonsterWave waveManager;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;


    void Start()
    {
        //this is working, don't move it
        //waveManager = GameObject.FindWithTag("Manager").GetComponent<MonsterWave>();
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        

    }


    public void Initialize(int maxHealth)
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Enemy Ouch");
        StartCoroutine(FlashRed());

        if (currentHealth <= 0) 
        { 
            Die();
        }
            
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }


    void Die()
    {
        //if (waveManager != null)
            //waveManager.EnemyDied();

        Debug.Log("Enemy Rip");

        Destroy(gameObject);
    }
}
