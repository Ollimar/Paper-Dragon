using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffMovement : MonoBehaviour
{
    public GameObject staff;

    public GameObject[] staffs;
    public GameObject[] followers;

    public float staffSpeed = 5f;

    public PlayerScript player;
    public float turnTimer = 3f;
    public float storedTurnTimer;
    public float staffRotateSpeed = 10f;

    public float timeToEat = 2f;
    public bool eating = false;

    public Vector3 originalEuler1;
    public Vector3 originalEuler2;

    public Collider dragonMouth;
    public Animator dragonHeadAnimator;

    // Start is called before the first frame update
    void Start()
    {
        storedTurnTimer = turnTimer;
        dragonMouth.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerScript>().gameStarted==true)
        {
            if (timeToEat <= 0f)
            {
                eating = true;
            }

            staffs[0].transform.Rotate(Vector3.forward, staffRotateSpeed * Time.deltaTime);
            staffs[1].transform.Rotate(Vector3.forward, -staffRotateSpeed * Time.deltaTime);
            /*
            if(eating)
            {
                dragonMouth.enabled = true;
                staffs[0].GetComponent<Animator>().enabled = true;
                staffs[0].GetComponent<Animator>().SetTrigger("Eat");
            }

            //staff.transform.RotateAround(staff.transform.position,Vector3.forward,Input.GetAxis("Horizontal2")*staffSpeed*Time.deltaTime);
            if(!eating)
            {
                dragonMouth.enabled = false;
                staffs[0].GetComponent<Animator>().enabled = false;
                staffs[0].transform.Rotate(Vector3.forward, staffRotateSpeed * Time.deltaTime);
                staffs[1].transform.Rotate(Vector3.forward, -staffRotateSpeed * Time.deltaTime);
            }
            */

            turnTimer -= Time.deltaTime;
            if (turnTimer <= 0f)
            {
                turnTimer = storedTurnTimer;
                staffRotateSpeed = -staffRotateSpeed;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                dragonHeadAnimator.SetTrigger("Hit");
                dragonMouth.enabled = true;
                //player.canHuh = true;
                StartCoroutine("StopDragonCollision");
                StartCoroutine("StaffsUp");

            }

            if (Input.GetAxis("Horizontal2") != 0f)
            {
                //StartCoroutine("StaffRotate");
            }

            if (Input.GetButton("Fire1") && staff.transform.position.y <= 2f)
            {
                //timeToEat -= Time.deltaTime;
                //player.PlayHuh();
                //staff.transform.Translate(Vector3.up * staffSpeed * Time.deltaTime);

            }


            else if (staff.transform.position.y >= 0f)
            {

                //staff.transform.Translate(Vector3.up * -staffSpeed * Time.deltaTime);

            }

            if (Input.GetButtonUp("Fire1"))
            {
                StartCoroutine("StaffsDown");
                eating = false;
                timeToEat = 1f;
            }
        }
        
    }

    public IEnumerator StaffsUp()
    {
        yield return new WaitForSeconds(0.5f);
        staffs[0].transform.Translate(Vector3.up * staffSpeed * Time.deltaTime);
        followers[0].GetComponent<Follower>().Huh();
        yield return new WaitForSeconds(0.5f);
        staffs[1].transform.Translate(Vector3.up * staffSpeed * Time.deltaTime);
        followers[1].GetComponent<Follower>().Huh();


    }

    public IEnumerator StaffsDown()
    {
        yield return new WaitForSeconds(0.5f);
        staffs[0].transform.Translate(Vector3.up * -staffSpeed * Time.deltaTime);
        yield return new WaitForSeconds(0.5f);
        staffs[1].transform.Translate(Vector3.up * -staffSpeed * Time.deltaTime);

    }

    public IEnumerator StaffRotate()
    {
        yield return new WaitForSeconds(0.5f);
        staffs[0].transform.eulerAngles = new Vector3(staff.transform.eulerAngles.x, staff.transform.eulerAngles.y, Input.GetAxis("Horizontal2") * 100f);
    }

    public IEnumerator StopDragonCollision()
    {
        yield return new WaitForSeconds(1f);
        dragonMouth.enabled = false;
    }
}
