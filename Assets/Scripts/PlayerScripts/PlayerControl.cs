
using System;
using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public char direction;

    [SerializeField] float attackHitboxOffset;
    private Transform hitboxTransform;
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
        audioSource = GetComponent<AudioSource>();
        hitboxTransform = transform.Find("AttackHitbox");
        attackHitboxOffset = Math.Abs(GetComponentInChildren<Transform>().position.y); //currently sets to the position of the given circle atm; can comment this to just use serialize field instead
    }

    void OnDodge()
    {
        transform.position = new Vector2(movementX * (speed * 2) * Time.fixedDeltaTime + transform.position.x, movementY * (speed * 2) * Time.fixedDeltaTime + transform.position.y);
    }

    void OnBoom()
    {
            audioSource.Play();
    }

    void FixedUpdate()
    {
       
        //basic movement
        float XmoveDistance = movementX * speed * Time.fixedDeltaTime;
        float YmoveDistance = movementY * speed * Time.fixedDeltaTime;
        
        transform.position = new Vector2(transform.position.x + XmoveDistance, transform.position.y + YmoveDistance);


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