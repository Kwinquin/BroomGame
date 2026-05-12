using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AudioSource audioSource;
    [SerializeField] public AudioClip gamemakerexplosion; 
    private float movementX;
    private float movementY;
    float lookAngle;
    
    [SerializeField] private float speed = 5f;
    void OnMove(InputValue value)
    {
        // get the 2D vector that represents the input value
        Vector2 inputVector = value.Get<Vector2>();
        Debug.Log(inputVector);
        
        movementX = inputVector.x;
        movementY = inputVector.y;

        if (movementY != 0)
        {
            lookAngle = 180 * (Mathf.Atan(-movementX/movementY)) / Mathf.PI;

            if (movementY < 0)
            {
            lookAngle += 180;
            }
        }
        else if (movementX > 0)
        {
            lookAngle = -90;
        }
        else if (movementX < 0)
        {
            lookAngle = 90;
        }

        Debug.Log(lookAngle);
        transform.rotation = Quaternion.Euler(0, 0, lookAngle);

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
 
    }
    void Update()
    {
        
    }
}