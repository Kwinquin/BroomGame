using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    //private Animator anim; when you implement the animation, just make a bool "isRunning" to transition from idle to walking
    private Transform currentPoint;
    public float speed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        //anim.SetBool("isRunning", true);
    }

    void FixedUpdate()
    {
        Vector2 direction = (currentPoint.position - transform.position).normalized;

        rb.linearVelocity = direction * speed;

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.2f)
        {
            currentPoint = (currentPoint == pointA.transform) ? pointB.transform : pointA.transform;
        }
    }


}
