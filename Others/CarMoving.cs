using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMoving : MonoBehaviour
{
    private Rigidbody body;
    public GameObject wheel;
    public float speed;
    public Transform endpoint;
    public Transform startpoint;
    public Transform assemblePos;
    public Transform WheelPos;
    public Transform[] pos;//wheel的移动轨迹
    public bool isFinshing;
    public bool wMove;
    public bool isreach;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        isFinshing = false;
        wMove = false;
        isreach = false;
    }

    // Update is called once per frame
    void Update()
    {
        isFinshing = GameObject.Find("SM_PolygonCity_Veh_Car_Small_Wheel_fl").GetComponent<AssambleObject>().isfinishing;
        if(isFinshing)
        {
            run();
        }
        if(Vector3.Distance(this.transform.position, assemblePos.transform.position)<=0.1)
        {
            isreach = false;
            isFinshing = false;
            wMove = false;
        }

    }

    public void startMoveToEnd()
    {
        this.gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, endpoint.transform.position, speed * Time.deltaTime);
        
    }

    public void startMoveToAssemblePos()
    {
        this.gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, assemblePos.transform.position, speed * Time.deltaTime);
    }
    
    public void run()
    {
        
        
        float des = Vector3.Distance(this.transform.position, endpoint.transform.position);
        if(!isreach)
            startMoveToEnd();
        if (this.gameObject.transform.position.x <= endpoint.position.x)
        {
            wMove = true;
            isreach = true;
            transform.position = startpoint.position;//瞬移到始发点
        }
        //print(des);
        if (isreach)
        {
            startMoveToAssemblePos();
        }
            

    }

    //public void wheelMove()
    //{
    //    int i = 0;
    //    float des;
    //    //看向目标点
    //    wheel.transform.LookAt(pos[i].transform);
    //    //计算与目标点间的距离
    //    des = Vector3.Distance(wheel.transform.position, pos[i].transform.position);
    //    //移向目标
    //    transform.position = Vector3.MoveTowards(wheel.transform.position, pos[i].transform.position, Time.deltaTime * speed);
    //    //如果移动到当前目标点，就移动向下个目标
    //    if (des < 0.1f && i < pos.Length - 1)
    //    {
    //        i++;
    //    }
    //    wMove = false;
    //}

}
