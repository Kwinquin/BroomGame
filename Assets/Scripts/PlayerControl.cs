using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AudioSource audioSource;
    [SerializeField] public AudioClip gamemakerexplosion; 
    private float movementX;
    private float movementY;
    
    [SerializeField] private float speed = 5f;
    void OnMove(InputValue value)
    {
        // get the 2D vector that represents the input value
        Vector2 v = value.Get<Vector2>();
        Debug.Log(v);
        
        movementX = v.x;
        movementY = v.y;
    }
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnBoom()
    {
            audioSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float XmoveDistance = movementX * speed * Time.fixedDeltaTime;
        float YmoveDistance = movementY * speed * Time.fixedDeltaTime;

        transform.position = new Vector2(transform.position.x + XmoveDistance, transform.position.y + YmoveDistance);
        transform.Rotate(0, 0, movementX + movementY);

    }
    void Update()
    {
        
    }
}
