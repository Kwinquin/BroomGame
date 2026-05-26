using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AudioSource audioSource;
    [SerializeField] public AudioClip gamemakerexplosion; 
    private float movementX;
    private float movementY;
    char direction;
    

    
    [SerializeField] private float speed = 5f;
    void OnMove(InputValue value)
    {
        
        Vector2 inputVector = value.Get<Vector2>();
        
        movementX = inputVector.x;
        movementY = inputVector.y;

        
        //inputdirection log
        if (movementX > 0) {
            direction = 'E';
        }
        if (movementX < 0){
            direction = 'W';
        }
        if (movementY > 0) {
            direction = 'N';
        }
        if (movementY < 0) {
            direction = 'S';
        }

    }
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnBoom()
    {
            audioSource.Play();
    }

    void Attack()
    {
        
    }
    void FixedUpdate()
    {
        //basic movement
        float XmoveDistance = movementX * speed * Time.fixedDeltaTime;
        float YmoveDistance = movementY * speed * Time.fixedDeltaTime;
        
        transform.position = new Vector2(transform.position.x + XmoveDistance, transform.position.y + YmoveDistance);

        //look direction 
        switch (direction)
        {
            case 'N':
            transform.rotation = Quaternion.Euler(0, 0, 0);
            break;
            case 'S':
            transform.rotation = Quaternion.Euler(0, 0, 180);
            break;
            case 'E':
            transform.rotation = Quaternion.Euler(0, 0, -90);
            break;
            case 'W':
            transform.rotation = Quaternion.Euler(0, 0, 90);
            break;
        }
        
    }
    void Update()
    {
       
    }
}