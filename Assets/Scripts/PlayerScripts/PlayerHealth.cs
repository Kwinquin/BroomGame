using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxhealth = 20;
    private int currentPHealth;

    public Slider healthSlider;

    void Start()
    {
        currentPHealth = maxhealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxhealth;
            healthSlider.value = currentPHealth;
        }
    }
    

    public void TakeDamageP(int amount)
    {
        currentPHealth -= amount;
        Debug.Log("Player Ouch");

        if (healthSlider != null)
        {
            healthSlider.value = currentPHealth;
        }

        if (currentPHealth <= 0)
        {
            PDie();
        }

    }

    void PDie()
    {
        SceneManager.LoadScene("DeathScreen");
        Debug.Log("RipPlayer");
    }

}
