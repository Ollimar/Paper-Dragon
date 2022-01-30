using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternScript : MonoBehaviour
{
    public int color;

    private AudioSource myAudio;
    public AudioClip chime;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Chime()
    {
        if(!myAudio.isPlaying)
        {
            myAudio.PlayOneShot(chime);
        }
    }
}
