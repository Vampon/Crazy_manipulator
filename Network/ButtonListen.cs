using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonListen : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public UnityEvent MethodCallBack;
    Dictionary<string, int> buttonList = new Dictionary<string, int>();

    //按下多长时间算长按
    private float timeLongPress = 0.3f;

    //是否按下
    private bool isPointerDown = false;

    //按下时刻
    private float timePointerDown = 0;

    private GameObject robotic_Arm;
    private int tag = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float span = Time.time - timePointerDown;
        if (isPointerDown && span > timeLongPress)
        {
            MethodCallBack.Invoke();

        }
        if (GameObject.Find("Robotic_arm_Net(Clone)") != null && tag == 0)
        {
            robotic_Arm = GameObject.Find("Robotic_arm_Net(Clone)");
            tag++;
        }
    }



    public void MoveArm()
    {
        Invoke("onClick_"+this.name, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        timePointerDown = Time.time;
        MethodCallBack.AddListener(MoveArm);
        MethodCallBack.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
    }

    void onClick_DOF0_1()
    {
        robotic_Arm.GetComponent<RoboticArm_Net>().DOF0_1();
    }

    void onClick_DOF0_2()
    {
        robotic_Arm.GetComponent<RoboticArm_Net>().DOF0_2();
    }

    void onClick_DOF1_1()
    {
        robotic_Arm.GetComponent<RoboticArm_Net>().DOF1_1();
    }

    void onClick_DOF1_2()
    {
        robotic_Arm.GetComponent<RoboticArm_Net>().DOF1_2();
    }

    void onClick_DOF2_1()
    {
        robotic_Arm.GetComponent<RoboticArm_Net>().DOF2_1();
    }

    void onClick_DOF2_2()
    {
        robotic_Arm.GetComponent<RoboticArm_Net>().DOF2_2();
    }

    void onClick_DOF3_1()
    {
        robotic_Arm.GetComponent<RoboticArm_Net>().DOF3_1();
    }

    void onClick_DOF3_2()
    {
        robotic_Arm.GetComponent<RoboticArm_Net>().DOF3_2();
    }
}
