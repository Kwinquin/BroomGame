using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //PlayerController p;
    //[SerializeFiled] float radiusToAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    p = FindAnyObjectByType<PlayerController>();
    //    StartCoroutine(BossStateMachine());
    //}

    //// Update is called once per frame
    //void FixedUpdate()
    //{
    //    float distanceToPlayer = Vector2.Distance(transform.position, p.transform.position);
    //    if (distanceToPlayer < radiusToAttack)
    //    {
    //        Attack();
    //    }
    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy catch player");
            AttackA();
        }
    }

    void OnTriggerExit2D()
    {

    }

    void AttackA()
    {

    }

    void AttackB()
    {

    }

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



}
