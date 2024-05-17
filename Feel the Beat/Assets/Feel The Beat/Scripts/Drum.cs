using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
    // Audio source to play the associated clip
    private AudioSource audioSource;

    // Initial clip is setup in prefab for DrumPad
    [SerializeField] private AudioClip drumAudio;

    // Keeps track of the current finger that is on the pad 
    private Collider currentFinger;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = drumAudio; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Changes the audio clip associated with this drum
    public void ChangeAudioClip(AudioClip newClip)
    {
        drumAudio = newClip;
    }

    // Play the audio clip when drum collider is triggered by a fingertip
    private void OnTriggerEnter(Collider other)
    {
        // Don't play the sound if there is a finger already on the pad
        if (other.tag == "Fingertip" && currentFinger == null)
        {
            currentFinger = other;
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    // Reset current finger if necessary
    private void OnTriggerExit(Collider other)
    {
        if (other == currentFinger)
        {
            currentFinger = null;
        }
    }
}
