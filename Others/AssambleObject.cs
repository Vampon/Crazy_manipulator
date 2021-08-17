using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssambleObject : MonoBehaviour
{
    private Transform mMark;

    private Transform tMark;

    public Transform TAssambleObject;

    public Transform carpos;

    public float Angle;
    public float distance;
    public int score;

    public GameObject screw;
    public GameObject car;

    public bool isfinishing;
    private bool condition = false;

    // Use this for initialization
    void Start()
    {
        score = 0;
        isfinishing = false;
        mMark = this.transform.Find("LocationMark");
        if (mMark == null)
        {
            Debug.LogError("本地对象未找到位置标志");
        }

        tMark = TAssambleObject.Find("LocationMark");
        if (tMark == null)
        {
            Debug.LogError("目标对象未找到位置标志");
        }

    }

    // Update is called once per frame
    void Update()
    {
        Angle = Vector3.Angle(mMark.transform.forward, tMark.transform.forward);
        distance = Vector3.Distance(mMark.transform.position, tMark.transform.position);
        if (Vector3.Distance(car.transform.position, carpos.transform.position) <= 0.1)
        {
            isfinishing = false;
            condition = false;
        }
        //print(distance);
        if(distance<=1f)
        {
            MoveToTarget();
            screw.transform.DetachChildren();//取消子物体绑定
            if(!condition)
            {
                score += 1;
                condition = true;
            }

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveToTarget();
        }
    }


    int j = 0;
    private void MoveToTarget()
    {

        Vector3 RotateAix = Vector3.Cross(mMark.transform.forward, tMark.transform.forward);
        float angle = Vector3.Angle(mMark.transform.forward, tMark.transform.forward);
        mMark.transform.parent.Rotate(RotateAix, angle, Space.World);
        while (true)
        {
            //第一步
            float Angle = Vector3.Angle(tMark.transform.up, mMark.transform.up);
            mMark.transform.parent.Rotate(tMark.transform.forward, Angle, Space.World);
            //第二步
            Vector3 moveVector = tMark.transform.position - mMark.transform.position;
            mMark.transform.parent.transform.Translate(moveVector, Space.World);
            j++;
            if (Angle == 0 && moveVector == Vector3.zero)
            {
                Debug.Log("进行了多少次数：" + j);//发现一次不是能达到目标，所以重复几次
                break;
            }
        }
        isfinishing = true;
        
        //Invoke("switchState", 5f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag== "robot_head")
        {
            transform.parent=other.transform;
        }
    }

   

    public void switchState()
    {
        isfinishing = false;
    }
}