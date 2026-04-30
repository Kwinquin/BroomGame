using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] string exposedMusicParam; 
    [SerializeField] Slider audioSlider;
    [SerializeField] AudioMixerSnapshot loadNoBG;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        loadNoBG.TransitionTo(2.0f);
    }

    public void SetMusicVolume()
    {
        float volumeinDb = Mathf.Log10(Mathf.Max(audioSlider.value, 0.0001f)) * 20f;
        mixer.SetFloat(exposedMusicParam, volumeinDb);

    }
}
