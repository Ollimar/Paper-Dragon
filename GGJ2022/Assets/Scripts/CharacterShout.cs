using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShout : MonoBehaviour
{
    private AudioSource myAudio;
    public AudioClip shout;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shout()
    {
        if(!myAudio.isPlaying)
        {
            myAudio.PlayOneShot(shout);
        }
    }
}
