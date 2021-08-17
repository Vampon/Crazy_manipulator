using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public GameObject screw;
    //used to instantiate a cube
    public GameObject cubePrefab;
    public Transform cubePos;
    public bool trigger;
    // Start is called before the first frame update
    void Start()
    {
        trigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="cube"&&!trigger)
        {
            Destroy(other.transform.parent.gameObject);
            screw = Instantiate(cubePrefab, cubePos.position, Quaternion.Euler(0, 0, 0));
            screw.tag = "cube";
            trigger = true;
            print("run");
        }
    }

    public bool istrigger()
    {
        return trigger;
    }
}
