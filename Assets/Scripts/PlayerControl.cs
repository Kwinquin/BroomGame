
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
    [SerializeField] Animator directionAnimator;
    private Vector2 lastInputVector = Vector2.zero;
    public enum Direction {None, Up, Right, Down, Left}
    public Direction lastPressedDirection = Direction.None;
    [SerializeField] private float speed = 5f;
    void OnMove(InputValue value)
    {
        
        Vector2 inputVector = value.Get<Vector2>();
         
        movementX = inputVector.x;
        movementY = inputVector.y;

        if(inputVector.x != lastInputVector.x && inputVector.x != 0)
        {
            lastPressedDirection = inputVector.x > 0 ? Direction.Left : Direction.Right;         
        }
        else if (inputVector.y != lastInputVector.y && inputVector.y != 0)
        {
            lastPressedDirection = inputVector.y > 0 ? Direction.Up : Direction.Down; 
        }

       /* 
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
        */
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

        /*
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
        */
    }
    void Update()
    {  
        if(lastPressedDirection == Direction.Up)
        {
            directionAnimator.SetInteger("dir", 1);
        }
        if(lastPressedDirection == Direction.Right)
        {
            directionAnimator.SetInteger("dir", 2);
        }
        if(lastPressedDirection == Direction.Down)
        {
            directionAnimator.SetInteger("dir", 3);
        }
        if(lastPressedDirection == Direction.Left)
        {
            directionAnimator.SetInteger("dir", 4);
        }
    }
}