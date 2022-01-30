using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform dragonHead;
    public Transform cameraPoint;

    public float cameraDistanceY = 10f;
    public float cameraDistanceZ = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = Vector3.Distance(target.position, dragonHead.position);

        cameraDistanceY = dist*2f;
        cameraDistanceY = Mathf.Clamp(cameraDistanceY,20f,35f);
        cameraDistanceZ = dist*2f;
        //cameraDistanceZ = Mathf.Clamp(cameraDistanceZ, -10f, 10f);


        //cameraDistanceZ = target.position.z + dragonHead.position.z;

        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y + cameraDistanceY, target.position.z - cameraDistanceZ), 3f * Time.deltaTime);


    }
}
