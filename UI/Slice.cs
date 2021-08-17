using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Slice : MonoBehaviour
{
    public Material mater;
    public bool lightout;
    // Start is called before the first frame update
    void Start()
    {
        lightout = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mx = Input.GetAxis("Mouse X");

        //transform.Rotate(0, 0, -mx);
        if (Input.GetMouseButtonDown(0))
        {

            //FindGameObjectsWithTag(string)
        }
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(2.5f, 0.0005f, 0.05f), transform.rotation, ~LayerMask.GetMask("cube"));
        foreach (Collider c in colliders)
        {
            if(!lightout)
            {
                Destroy(c.gameObject);
                //GameObject[] objs = c.gameObject.SliceInstantiate(transform.position, transform.up);
                SlicedHull hull = c.gameObject.Slice(transform.position, transform.up);
                if (hull != null)
                {
                    GameObject lower = hull.CreateLowerHull(c.gameObject, mater);
                    GameObject upper = hull.CreateUpperHull(c.gameObject, mater);
                    GameObject[] objs = new GameObject[] { lower, upper };
                    foreach (GameObject obj in objs)
                    {
                        obj.tag = "cube";
                        obj.AddComponent<Rigidbody>();
                        obj.AddComponent<MeshCollider>().convex = true;
                    }
                }
            }

        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "cube" && !lightout)
        {
            ////Destroy(other.gameObject);
            ////GameObject[] objs = c.gameObject.SliceInstantiate(transform.position, transform.up);
            //SlicedHull hull = other.gameObject.Slice(transform.position, transform.up);
            //if (hull != null)
            //{
            //    GameObject lower = hull.CreateLowerHull(other.gameObject, mater);
            //    GameObject upper = hull.CreateUpperHull(other.gameObject, mater);
            //    GameObject[] objs = new GameObject[] { lower, upper };
            //    foreach (GameObject obj in objs)
            //    {
            //        obj.tag = "test";
            //        obj.AddComponent<Rigidbody>();
            //        obj.AddComponent<MeshCollider>().convex = true;
            //    }
            //}
            lightout = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        lightout = true;
    }
}
