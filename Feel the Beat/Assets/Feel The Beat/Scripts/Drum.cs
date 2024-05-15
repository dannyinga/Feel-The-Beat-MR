using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
    [SerializeField] private AudioClip drumAudio;
    public AudioClip DrumAudio
    {
        get {  return drumAudio; }
        set { drumAudio = value; audioSource.clip = drumAudio; }
    }

    private AudioSource audioSource;

    private Collider currentFinger;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DrumAudio = drumAudio;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fingertip" && currentFinger == null)
        {
            currentFinger = other;
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == currentFinger)
        {
            currentFinger = null;
        }
    }
}
