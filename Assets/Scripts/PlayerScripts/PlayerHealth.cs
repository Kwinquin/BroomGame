using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxhealth = 20;
    private int currentPHealth;

    void Start()
    {
        currentPHealth = maxhealth;
    }
    

    public void TakeDamageP(int amount)
    {
        currentPHealth -= amount;
        Debug.Log("Player Ouch");

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
