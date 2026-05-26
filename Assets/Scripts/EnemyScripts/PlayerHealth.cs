using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxhealth = 20;
    private int currentPHealth;

    public void TakeDamageP(int amount)
    {
        currentPHealth -= amount;
        Debug.Log("Player Ouch");

        if (currentPHealth <= 0) { }
            PDie();

    }

    void PDie()
    {
        Debug.Log("RipPlayer");
        Destroy(gameObject);
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E)) 
    //        TakeDamageP(1);
    //}
}
