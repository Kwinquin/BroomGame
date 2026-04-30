using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AudioSource audioSource;
    [SerializeField] public AudioClip gamemakerexplosion; 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnBoom()
        {
            audioSource.Play();
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
