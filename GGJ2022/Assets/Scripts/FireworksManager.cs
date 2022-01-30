using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksManager : MonoBehaviour
{
    public GameObject[] fireWorks;

    public bool[] colorsCollected;

    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x,transform.position.y,target.position.z);
    }


    public IEnumerator Boom(int boomNum)
    {
        colorsCollected[boomNum] = true;
        for (int i = 0; i < colorsCollected.Length; i++)
        {
            if (colorsCollected[i] == true)
            {
                yield return new WaitForSeconds(1f);
                fireWorks[i].GetComponent<Animator>().SetTrigger("Boom");
            }
        }
    }
}
