using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorks : MonoBehaviour
{

    private AudioSource myAudio;
    private Animator myAnim;

    public AudioClip expl;
    public GameObject follower;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fireworks()
    {
        StartCoroutine("Yay");
        if (!myAudio.isPlaying)
        {
            myAudio.PlayOneShot(expl);
        }
        
    }

    public void Stop()
    {
        myAnim.SetBool("Boom",false);
    }

    public IEnumerator Yay()
    {
        yield return new WaitForSeconds(5f);
        follower.GetComponent<Follower>().Yay();
    }
}
