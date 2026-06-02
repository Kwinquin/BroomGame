using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Move Slots")]
    public MoveData lightAttack;
    public MoveData heavyAttack;
    public MoveData specialAttack;

    [Header("Hitbox")]
    public Transform attackPoint;   // this is the reference for the direction of attack

    [Header("Combat Settings")]
    public LayerMask enemyLayer;

    private Animator attackAnimator;

    [Header("Unlocks")]
    public bool heavyUnlocked = false;
    public bool specialUnlocked = false;

    public AudioClip pokeSound;
    public AudioClip wackSound;
    public AudioClip sweepSound;


    void Awake()
    {
        attackAnimator = GetComponent<Animator>();
    }

    void OnLightAttack()
    {
        attackAnimator.SetTrigger("poke");
        OtherAudio.Instance.PlaySound(pokeSound);
        PerformAttack(lightAttack);
    }

    void OnHeavyAttack()
    {
        if (!heavyUnlocked)
        {
            Debug.Log("Heavy attack not unlocked yet!");
            return;
        }
        attackAnimator.SetTrigger("wack");
        OtherAudio.Instance.PlaySound(wackSound);
        PerformAttack(heavyAttack);
    }

    void OnSpecialAttack()
    {
        if (!specialUnlocked)
        {
            Debug.Log("Special attack not unlocked yet!");
            return;
        }
        attackAnimator.SetTrigger("sweep");
        OtherAudio.Instance.PlaySound(sweepSound);
        PerformAttack(specialAttack);
    }


    void PerformAttack(MoveData move)
    {
        if (move == null)
        {
            Debug.Log("No move assigned to this slot");
            return;
        }

        Debug.Log("Performing attack: " + move.moveName);

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            move.attackRange,
            enemyLayer
        );

        foreach (Collider2D hit in hits)
        {
            Debug.Log("Hit enemy: " + hit.name);
            EnemyHealth eh = hit.GetComponent<EnemyHealth>();
            if (eh != null)
                eh.TakeDamage(move.damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, 1f);
    }

}
