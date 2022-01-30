using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbScript : MonoBehaviour
{
    public Color[] color;
    public int colorNumber;
    public float lightIntensity = 7f;

    public Material myMaterial;

    public Vector3 storedScale;

    public FireworksManager fireWorksManager;
    public Camera cam;
    public GameObject particles;

    private AudioSource myAudio;
    public AudioClip drumming;
    public bool drummmingOn;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
       // myAudio.volume = 0f;
        myMaterial.SetColor("_EmissionColor", color[0]* lightIntensity);
        storedScale = transform.localScale;
        //InvokeRepeating("ChangeColor", 5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(drummmingOn)
        {
            myAudio.volume = Mathf.Lerp(0f, 0.5f, 0.1f * Time.deltaTime);
        }
        else if(!drummmingOn)
        {
            myAudio.volume = Mathf.Lerp(myAudio.volume, 0, 1f * Time.deltaTime);
        }
        */
    }

    public void ChangeColor()
    {
        colorNumber = Random.Range(0,color.Length);
        myMaterial.SetColor("_EmissionColor", color[colorNumber] * lightIntensity);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "DragonMouth")
        {
            transform.localScale = new Vector3(0f,0f,0f);
            GetComponent<Collider>().enabled = false;
            Instantiate(particles, transform.position, transform.rotation);
            drummmingOn = true;
            StartCoroutine(cam.GetComponent<ScreenShakeScript>().ScreenShake(0.5f,1f));
            StartCoroutine(fireWorksManager.Boom(colorNumber));
            StartCoroutine("ReturnOrb");
        }

        if(other.gameObject.tag == "Lantern")
        {
            colorNumber = other.GetComponent<LanternScript>().color;
            myMaterial.SetColor("_EmissionColor", color[colorNumber] * lightIntensity);
            other.GetComponent<LanternScript>().Chime();
            print("hit the lantern");
        }
    }

    public IEnumerator ReturnOrb()
    {
        yield return new WaitForSeconds(5f);
        transform.localScale = storedScale;
        GetComponent<Collider>().enabled = true;
        drummmingOn = false;
    }
}
