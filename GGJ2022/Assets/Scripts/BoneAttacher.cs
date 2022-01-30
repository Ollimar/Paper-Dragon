using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneAttacher : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x,target.position.y+2f,target.position.z);
        transform.eulerAngles = new Vector3(target.eulerAngles.x, target.eulerAngles.y-90f, target.eulerAngles.z);
    }
}
