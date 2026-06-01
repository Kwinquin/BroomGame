using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 1;
    public float attackRange = 1f;
    public LayerMask enemyLayer;

    void OnAttack()
    {
        Attack();
    }

    void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            Debug.Log("Hit!");
            EnemyHealth eh = hit.GetComponent<EnemyHealth>();
            if (eh != null)
            {
                eh.TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

 
    //public int damage = 1;
    //public float attackCooldown = 5f;
    //private float nextAttackTime = 10f;

    //void OnTriggerStay2D(Collider2D other)
    //{

    //    if (other.CompareTag("Player") && Time.time >= nextAttackTime)
    //    {
    //        Debug.Log("OMG IT'S THE PLAYER, KILL IT");

    //        PlayerHealth ph = other.GetComponent<PlayerHealth>();
    //        if (ph != null)
    //        {
    //            ph.TakeDamageP(damage);
    //            nextAttackTime = Time.time + attackCooldown;
    //        }
    //    }
    //}

}
