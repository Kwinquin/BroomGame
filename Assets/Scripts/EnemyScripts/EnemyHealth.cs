using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int currentHealth;
    private bool isDead = false;

    public MonsterWave waveManager;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public AudioClip damageSound;
    public AudioClip deathSound;


    void Start()
    {
        //this is working, don't move it
        waveManager = GameObject.FindWithTag("Manager").GetComponent<MonsterWave>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }


    public void Initialize(int maxHealth)
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log("Enemy Ouch");
        StartCoroutine(FlashRed());
        OtherAudio.Instance.PlaySound(damageSound);

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
        isDead = true;

        if (waveManager != null)
            waveManager.EnemyDied();
        OtherAudio.Instance.PlaySound(deathSound);

        Debug.Log("Enemy Rip");

        Destroy(gameObject);
    }
}