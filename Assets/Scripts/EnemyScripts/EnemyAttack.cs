using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackADamage = 5;
    public int attackBDamage = 10;
    public int attackCDamage = 15;

    public float attackCooldown = 5f;
    private float nextAttackTime = 10f;

    void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && Time.time >= nextAttackTime)
        {
            Debug.Log("OMG IT'S THE PLAYER, KILL IT");
            
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                int attackChoice = Random.Range(0, 3);
                int damage = 0;

                // Determine which attack to use based on the random number
                if (attackChoice == 0)
                {
                    Debug.Log("Enemy uses Attack A (-5 Health)");
                    damage = attackADamage;
                }
                else if (attackChoice == 1)
                {
                    Debug.Log("Enemy uses Attack B (-10 Health)");
                    damage = attackBDamage;
                }
                else if (attackChoice == 2)
                {
                    Debug.Log("Enemy uses Attack C (-15 Health)");
                    damage = attackCDamage;
                }

                ph.TakeDamageP(damage);
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }
}


//PlayerController p;
//[SerializeFiled] float radiusToAttack;

// Start is called once before the first execution of Update after the MonoBehaviour is created
//void Start()
//{
//    p = FindAnyObjectByType<PlayerController>();
//    StartCoroutine(BossStateMachine());
//}

// Update is called once per frame
//void FixedUpdate()
//{
//    float distanceToPlayer = Vector2.Distance(transform.position, p.transform.position);
//    if (distanceToPlayer < radiusToAttack)
//    {
//        Attack();
//    }
//}

//void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            Debug.Log("Enemy catch player");
//            AttackA();
//        }
//    }

//    void OnTriggerExit2D()
//    {

//    }

//    void AttackA()
//    {

//    }

//    void AttackB()
//    {

//    }

//IEnumerator BossStateMachine()
//{
//    while (true)
//    {
//        int choice = Random.Range(0, 4);
//        switch (choice)
//        {
//            case 0:
//                AttackA();
//                yield return new WaitForSeconds(timeBetweenAttacks);
//                MoveToNewLocation();
//                break;
//            case 1
//                AttackB();
//                yield return new WaitForSeconds(timeBetweenAttacks);
//                MoveToNewLocation();
//                break;
//            case 2:
//                StartCoroutine(Attack!());
//                MoveToNewLocation();
//                break;
//            case 3:
//                MoveToNewLocation();
//                break;
//            default:
//                break;
//        }


//        yield return null;
//    }




//}




