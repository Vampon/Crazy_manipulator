using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.GetComponent<BoxCollider>().enabled==false)
        {
            //print("box false");
            transform.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            //print("box true");
            transform.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
