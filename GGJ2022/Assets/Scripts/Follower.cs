using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 15f;

    private Rigidbody myRB;
    private NavMeshAgent myAgent;
    public Animator myAnim;


    public GameObject leader;
    public Transform target;
    public GameObject follower;

    private AudioSource myAudio;
    public AudioClip huh;
    public AudioClip yay;
    public bool canHuh = true;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
       
        myAudio = GetComponent<AudioSource>();
        InvokeRepeating("Follow",0f,0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position);
    }

    void Follow()
    {
        myAgent.destination = target.position;
    }

    public void Stop()
    {
        myAnim.SetBool("isWalking", false);
        myAgent.speed = 0f;

        if(follower != null)
        {
            follower.GetComponent<Follower>().Stop();
        }
    }

    public void Go()
    {
        myAnim.SetBool("isWalking", true);
        myAgent.speed = 4.5f;

        if (follower != null)
        {
            follower.GetComponent<Follower>().Go();
        }
    }

    public IEnumerator Huh()
    {
        yield return new WaitForSeconds(0.5f);
        if(!myAudio.isPlaying)
        {
            myAudio.pitch = Random.Range(1.5f, 2f);
            myAudio.PlayOneShot(huh);
        }
        if(follower != null)
        {
            follower.GetComponent<Follower>().StartCoroutine("Huh");
        }
       
    }

    public void Yay()
    {
        myAudio.pitch = Random.Range(1.5f, 2f);
        myAudio.PlayOneShot(yay);
    }

    public void Activate()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }
}
