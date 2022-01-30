using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerScript : MonoBehaviour
{

    public bool gameStarted = false;
    public GameObject logo;

    public float speed = 5f;
    public float rotationSpeed = 15f;

    private Rigidbody myRB;
    private AudioSource myAudio;

    public Animator myAnim;

    public Transform trail;
    public Transform latestTrail;
    public int trailCount;

    public GameObject follower;
    public GameObject[] followers;

    public PostProcessVolume post;
    public DepthOfField depthOfField;

    [SerializeField]
    public AudioClip huhSound;
    public bool canHuh = true;
    public float huhTimer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAudio = GetComponent<AudioSource>();
        post = GameObject.Find("PP").GetComponent<PostProcessVolume>();
        post.profile.TryGetSettings(out depthOfField);
        Transform latest = Instantiate(trail,transform.position,transform.rotation);
        latestTrail = latest;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            gameStarted = true;
            logo.GetComponent<Animator>().SetTrigger("Away");
            followers[0].GetComponent<Follower>().Activate();
            followers[1].GetComponent<Follower>().Activate();
            followers[0].GetComponent<Follower>().Stop();
            followers[1].GetComponent<Follower>().Stop();
            depthOfField.focusDistance.value = 26f;
            StartCoroutine("LogoAway");
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(gameStarted)
        {
            Move(h, v);

            if (h == 0f && v == 0f)
            {
                myAnim.SetBool("isWalking", false);
                follower.GetComponent<Follower>().Stop();
                canHuh = false;
                if (IsInvoking("PlayHuh"))
                {
                    CancelInvoke();
                }
            }

            else if (h != 0f || v != 0f)
            {
                myAnim.SetBool("isWalking", true);
                follower.GetComponent<Follower>().Go();
                if (!IsInvoking("PlayHuh"))
                {
                    InvokeRepeating("PlayHuh", 0f, 1f);
                }
            }
        }

    }

    public void Move(float h, float v)
    {
        Vector3 movement = new Vector3(h, 0f, v);
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        myRB.MovePosition(transform.position + movement);
        if (h != 0f || v != 0f)
        {
            Rotating(h, v);
        }
    }

    public void Rotating(float hor, float ver)
    {
        Vector3 targetDirection = new Vector3(hor, 0f, ver);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(myRB.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        myRB.MoveRotation(newRotation);
    }

    public void PlayHuh()
    {
        if(!myAudio.isPlaying)
        {
            myAudio.PlayOneShot(huhSound);
            follower.GetComponent<Follower>().StartCoroutine("Huh");
        }
    }

    public IEnumerator LogoAway()
    {
        yield return new WaitForSeconds(1f);
        logo.SetActive(false);
    }

}
