using UnityEngine;
using Pathfinding; 

public class EnemyAI : MonoBehaviour
{
    Transform destination; 
    AIPath ai;
    Animator animator;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        ai = GetComponent<AIPath>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        
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

            HandleAnimations();
        }
    }
    void HandleAnimations()
    {
        float horizontalSpeed = ai.velocity.x;

        if (horizontalSpeed > 0.1f)
        {
            spriteRenderer.flipX = true; // Facing Right (Default)
        }
        else if (horizontalSpeed < -0.1f)
        {
            spriteRenderer.flipX = false;  // Facing Left (Flipped)
        }
        if (animator != null)
        {
            animator.SetFloat("Speed", ai.velocity.magnitude);
        }
    } 
}