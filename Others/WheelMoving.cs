using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMoving : MonoBehaviour
{
    public bool wMove;
    public bool isFinshing;
    public Transform WheelPos;
    public Transform initPos;
    public Vector3 tag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        wMove = GameObject.Find("car").GetComponent<CarMoving>().wMove;
        isFinshing = GameObject.Find("car").GetComponent<CarMoving>().isFinshing;
        if (wMove)
        {
            this.transform.position = WheelPos.transform.position;
        }
        tag = gameObject.transform.position - initPos.transform.position;
        //print(gameObject.transform.position - initPos.transform.position);
        if (!isFinshing && tag.x == 0 && tag.z == 0)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, initPos.transform.position, 10 * Time.deltaTime);
        }
    }
}
