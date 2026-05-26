using UnityEngine;
using Pathfinding; 

public class EnemyAI : MonoBehaviour
{
    Transform destination; 
    AIPath ai;

    void Awake()
    {
        ai = GetComponent<AIPath>();

        
        GameObject playerObject = GameObject.Find("Player");
        
        if (playerObject != null)
        {
            destination = playerObject.transform;
        }
        else
        {
            Debug.LogError("EnemyAI: Could not find a GameObject named 'Player' in the scene!");
        }
    }

    void Update()
    {   
        
        if (ai != null && destination != null) 
        {
            ai.destination = destination.position;
        }
    }

    
}