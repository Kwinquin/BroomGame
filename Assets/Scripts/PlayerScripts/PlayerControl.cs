
using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    
    private AudioSource audioSource;
    [SerializeField] public AudioClip gamemakerexplosion; 

    private float movementX;
    private float movementY;

    [SerializeField] Animator directionAnimator;

    private Vector2 lastInputVector = Vector2.zero;
    public enum Direction {None, Up, Right, Down, Left}
    public Direction lastPressedDirection = Direction.None;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float dodgeSpeed = 25f;
    public char direction;

    [SerializeField] float attackHitboxOffset;
    private Transform hitboxTransform;
    public bool isDodging = false;
    Vector2 targetposition;
    float dodgeCooldown = 0;
    BoxCollider2D playerHitbox; 
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
    }
    
    void Start()
    {
        playerHitbox = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        hitboxTransform = transform.Find("AttackHitbox");
        attackHitboxOffset = Math.Abs(GetComponentInChildren<Transform>().position.y); //currently sets to the position of the given circle atm; can comment this to just use serialize field instead
    }

    void OnDodge()
    {
        if (dodgeCooldown <= 0)
        {
            isDodging = true;
            if(lastPressedDirection == Direction.Up)
            {
                targetposition = new Vector2(transform.position.x + 0, transform.position.y + 6);
            }
            if(lastPressedDirection == Direction.Right)
            {
                targetposition = new Vector2(transform.position.x - 6, transform.position.y + 0);
            }
            if(lastPressedDirection == Direction.Down)
            {
             targetposition = new Vector2(transform.position.x + 0, transform.position.y - 6);
            }
            if(lastPressedDirection == Direction.Left)
            {
                targetposition = new Vector2(transform.position.x + 6, transform.position.y + 0);
            }
            playerHitbox.enabled = !playerHitbox.enabled; //disables collider in dodge
            directionAnimator.SetTrigger("dodge");
        }
        else
        {
            return;
        }
    }

    void OnBoom()
    {
            audioSource.Play();
    }

    void FixedUpdate()
    {
        if (isDodging == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetposition, dodgeSpeed * Time.fixedDeltaTime);
            if (transform.position.x == targetposition.x && transform.position.y == targetposition.y)
            {
                isDodging = false;
                dodgeCooldown = 150; //50 frames per second means 3 seconds
                playerHitbox.enabled = !playerHitbox.enabled; //hitbox is re-enabled
                return;
            }
        }
        //basic movement
        float XmoveDistance = movementX * speed * Time.fixedDeltaTime;
        float YmoveDistance = movementY * speed * Time.fixedDeltaTime;
        
        if (isDodging != true)
        {
        transform.position = new Vector2(transform.position.x + XmoveDistance, transform.position.y + YmoveDistance);
        }

        //hitbox rotation
        switch (direction)
        {
            case 'N':
            hitboxTransform.position = transform.position + new Vector3(0,attackHitboxOffset,0);
            break;
            case 'S':
            hitboxTransform.position = transform.position + new Vector3(0,-attackHitboxOffset,0);
            break;
            case 'E':
            hitboxTransform.position = transform.position + new Vector3(-attackHitboxOffset,0,0);
            break;
            case 'W':
            hitboxTransform.position = transform.position + new Vector3(attackHitboxOffset,0,0);
            break;
        
        }

        //dodgecooldown timer

        if(dodgeCooldown > 0)
        {
        dodgeCooldown = dodgeCooldown - 1;
        }
    }
    void Update()
    {  
        //movement animation logic
        if(lastPressedDirection == Direction.Up)
        {
            directionAnimator.SetInteger("dir", 1);
            direction = 'N'; //keeps track of direction for hitbox movement as well
        }
        if(lastPressedDirection == Direction.Right)
        {
            directionAnimator.SetInteger("dir", 2);
            direction = 'E';
        }
        if(lastPressedDirection == Direction.Down)
        {
            directionAnimator.SetInteger("dir", 3);
            direction = 'S';
        }
        if(lastPressedDirection == Direction.Left)
        {
            directionAnimator.SetInteger("dir", 4);
            direction = 'W';
        }
    }
}