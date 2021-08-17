using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VolumetricLines;
using UnityEngine.SceneManagement;
//this is a prototype script

public class AutoMove : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    // gravity will work if this is enabled
    public bool gravityON;

    //toque applied to each part of the robot
    public float[] torque;
    //error in quaternion
    public float error = 0.001f;

    //drags
    public float drag = 0.1f;
    public float angDrag = 0.01f;

    //movement of the robot
    public float stepMovement;
    public bool automovement = false;
    private int ScrewForce = 1;//第二关工具增强的增大扭转力的系数
    public GameObject Auto;

    //part fo the robot
    public Rigidbody part0;
    public Rigidbody part1;
    public Rigidbody part2;
    public Rigidbody part3;
    public Rigidbody gripLeft;
    public Rigidbody gripRight;
    public Rigidbody rotatingScrew;

    public bool grip = false;
    public bool startWork = false;
    //public GameObject screw;


    public AnimationCurve curve;
    //rigidbodies
    Rigidbody[] rbs;

    //used to instantiate a cube
    //public GameObject cubePrefab;
    public GameObject dynamicArm;
    public GameObject TAG;
    public GameObject line;
    public Transform tagPos;
    public Dropdown pointList;
    //public Transform cubePos;

    List<Quaternion> t_armList = new List<Quaternion>();
    List<Quaternion> q1_armList = new List<Quaternion>();
    List<Quaternion> q2_armList = new List<Quaternion>();
    List<Quaternion> q3_armList = new List<Quaternion>();
    List<Vector3> t = new List<Vector3>();
    List<Vector3> q1 = new List<Vector3>();
    List<Vector3> q2 = new List<Vector3>();
    List<Vector3> q3 = new List<Vector3>();
    List<bool> Grip = new List<bool>();
    List<GameObject> tagBall = new List<GameObject>();
    public List<GameObject> lineList = new List<GameObject>();
    bool temp;
    public GameObject toogle;
    public float aT, aQ1, aQ2, aQ3;
    float value;
    //quaternions that are used for pick position and drop position
    //public Quaternion Tpick, Q1pick, Q2pick, Q3pick;
    //public Quaternion Tdrop, Q1drop, Q2drop, Q3drop;

    //quaternions that are used for displating the arm rotation
    public Quaternion t_arm, q1_arm, q2_arm, q3_arm;
    Quaternion t_arm0, q1_arm0, q2_arm0, q3_arm0;

    void Start()
    {
        dynamicArm.GetComponent<AutoMove>().enabled = false;
        //create array of rigidbodies for future use
        
        rbs = new Rigidbody[6];
        rbs[0] = part0;
        rbs[1] = part1;
        rbs[2] = part2;
        rbs[3] = part3;
        rbs[4] = gripLeft;
        rbs[5] = gripRight;

        //show reative rotation on the inspector and set the initial rotations
        t_arm = part0.transform.rotation;
        q1_arm = part1.transform.rotation;
        q2_arm = part2.transform.rotation;
        q3_arm = part3.transform.rotation;

        //下面多余
        t_arm0 = part0.transform.rotation;
        q1_arm0 = part1.transform.rotation;
        q2_arm0 = part2.transform.rotation;
        q3_arm0 = part3.transform.rotation;
        
        Auto = GameObject.Find("Auto");




    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //print(t_armList.Count);
        //set gravity if changed
        if (gravityON)
        {
            for (int ii = 0; ii < rbs.Length; ii++)
            {
                rbs[ii].useGravity = true;
            }

        }
        else
        {
            for (int ii = 0; ii < rbs.Length; ii++)
            {
                rbs[ii].useGravity = false;
            }
        }


        //set drags

        for (int ii = 0; ii < rbs.Length; ii++)
        {
            rbs[ii].drag = drag;
            rbs[ii].angularDrag = angDrag;
        }

        if(SceneManager.GetActiveScene().buildIndex == 2)//对于第一关
        {
            if (grip)
            {
                gripLeft.AddTorque(torque[4] * gripLeft.mass * gripLeft.transform.forward);
                gripRight.AddTorque(torque[4] * gripRight.mass * gripRight.transform.forward);

            }
            else
            {
                gripLeft.AddTorque(-torque[4] * gripLeft.mass * gripLeft.transform.forward);
                gripRight.AddTorque(-torque[4] * gripRight.mass * gripRight.transform.forward);

            }
        }
        else if(SceneManager.GetActiveScene().buildIndex == 3)//对于第二关 转动螺丝手自动化
        {
            if (grip)
            {

                rotatingScrew.AddTorque(torque[4] * rotatingScrew.mass * rotatingScrew.transform.up * ScrewForce);
            }
            else
            {

                //rotatingScrew.MoveRotation(Quaternion.Euler(90, 0, 0));
            }
        }

        



        //show reative rotation on the inspector
        t_arm = part0.transform.rotation;
        q1_arm = part1.transform.rotation;
        q2_arm = part2.transform.rotation;
        q3_arm = part3.transform.rotation;
        print(q3_arm);

    }

    IEnumerator movement()
    {
        
        while (true)
        {
            //yield return moveArm(t_arm0, q1_arm0, q2_arm0, q3_arm0);

            if(automovement)//在我点击终止按钮后，协程也一直在循环工作,因此再引入一个判断
            {
                for (int i = 0; i < t_armList.Count; i++) 
                {
                    //print(i);
                    yield return new WaitForSeconds(0.5f);
                    yield return moveArm(t_armList[i], q1_armList[i], q2_armList[i], q3_armList[i]);
                    //yield return new WaitForSeconds(0.3f);
                    if (Grip[i])
                    {

                        print("grip");
                        grip = true;
                        yield return new WaitForSeconds(0.8f);
                    }
                    else
                    {
                        grip = false;
                    }
                }
            }
            
            ////move to pick object
            //yield return moveArm(Tpick, Q1pick, Q2pick, Q3pick);
            //grip = true;
            //yield return new WaitForSeconds(1);

            ////move to original position
            //yield return moveArm(t_arm0, q1_arm0, q2_arm0, q3_arm0);

            ////move to drop object
            //yield return moveArm(Tdrop, Q1drop, Q2drop, Q3drop);
            //yield return new WaitForSeconds(1);
            //grip = false;
            //yield return new WaitForSeconds(1);

            //screw = Instantiate(cubePrefab, cubePos.position, Quaternion.Euler(0, 0, 0));
            //screw.tag = "cube";
            //startWork = false;
            //move to original position
            //yield return moveArm(t_arm0, q1_arm0, q2_arm0, q3_arm0);
            yield return null;


        }
    }


    //this is the funciton used to move to a specific arm position
    IEnumerator moveArm(Quaternion T, Quaternion q1, Quaternion q2, Quaternion q3)
    {

        aT = quatError(t_arm, T);
        aQ1 = quatError(q1_arm, q1);
        aQ2 = quatError(q2_arm, q2);
        aQ3 = quatError(q3_arm, q3);
        float stepInit = 5;
        value = 0;
        float Angle;
        float partAngle;
        //print(T);
        while ((Mathf.Abs(aT) < 1 - error || Mathf.Abs(aQ1) < 1 - error || Mathf.Abs(aQ2) < 1 - error || Mathf.Abs(aQ3) < 1 - error)&& automovement)
        {
            

                aT = quatError(t_arm, T);
                aQ1 = quatError(q1_arm, q1);
                aQ2 = quatError(q2_arm, q2);
                aQ3 = quatError(q3_arm, q3);

                //方法一:利用时间逐渐递减//
                //if(stepMovement>2.5f)//大于最小步长时按照曲线递减
                //{
                //    value += Time.deltaTime/5;
                //    stepMovement = stepInit*curve.Evaluate(value);
                //}

                //方法二:利用转角//
                partAngle = (Quaternion.Angle(t_arm, T) + Quaternion.Angle(q1_arm, q1) + Quaternion.Angle(q2_arm, q2) + Quaternion.Angle(q3_arm, q3))/180;//四个部分的转角平均数
                Angle = partAngle/4;//归一化
                stepMovement = stepInit*curve.Evaluate(Angle);//通过归一化的值得到曲线对应的参数
                print(Angle);

                part0.MoveRotation(Quaternion.RotateTowards(t_arm, T, stepMovement));
                yield return null;
                part1.MoveRotation(Quaternion.RotateTowards(q1_arm, q1, stepMovement));
                yield return null;
                part2.MoveRotation(Quaternion.RotateTowards(q2_arm, q2, stepMovement));
                yield return null;
                part3.MoveRotation(Quaternion.RotateTowards(q3_arm, q3, stepMovement));
                yield return null;
            
            

        }
        yield return null;


    }
    //quaternion relative error
    public float quatError(Quaternion q, Quaternion p)
    {
        return (Quaternion.Dot(p, q));

    }

    public void addArm()
    {
        //t.Add(part0.transform.rotation);
        //q1.Add(part1.transform.rotation);
        //q2.Add(part2.transform.rotation);
        //q3.Add(part3.transform.rotation);
        //Quaternion temp = Quaternion.FromToRotation(t[t.Count-1], part0.transform.position);//拿上一个位置和现在位置进行四元数计算
        //t_armList.Add(temp);
        //temp = Quaternion.FromToRotation(q1[q1.Count - 1], part1.transform.position);//拿上一个位置和现在位置进行四元数计算
        //q1_armList.Add(temp);
        //temp = Quaternion.FromToRotation(q2[q2.Count - 1], part2.transform.position);//拿上一个位置和现在位置进行四元数计算
        //q2_armList.Add(temp);
        //temp = Quaternion.FromToRotation(q3[q3.Count - 1], part3.transform.position);//拿上一个位置和现在位置进行四元数计算
        //q3_armList.Add(temp);
        t_armList.Add(part0.transform.rotation);
        
        q1_armList.Add(part1.transform.rotation);
        
        q2_armList.Add(part2.transform.rotation);
        
        q3_armList.Add(part3.transform.rotation);

        Grip.Add(temp);
        GameObject tb = Instantiate(TAG, tagPos.position, Quaternion.identity);
        tb.transform.SetParent(Auto.transform);//置为子物体
        tb.GetComponentInChildren<Text>().text = (t_armList.Count).ToString();
        tagBall.Add(tb);//将生成的标记球加入序列
        AddDropDownOptionsData((t_armList.Count).ToString());
        if(t_armList.Count>1) //存在两个以上的标记点
        {
            var l = Instantiate(line);
            l.GetComponent<VolumetricMultiLineBehavior>().m_lineVertices[0] = tagBall[tagBall.Count - 2].transform.position;
            l.GetComponent<VolumetricMultiLineBehavior>().m_lineVertices[1] = tagBall[tagBall.Count - 1].transform.position;
            l.transform.SetParent(Auto.transform);//置为子物体
            lineList.Add(l);
            //(tagBall[tagBall.Count - 1].transform.position.x, tagBall[tagBall.Count - 1].transform.position.y, tagBall[tagBall.Count - 1].transform.position.z);
        }
    }

    public void start()
    {

        dynamicArm.GetComponent<AutoMove>().enabled = true;
        automovement = true;
        StartCoroutine(movement());
        print("start");
            
        dynamicArm.GetComponent<RoboticArm_tools>().enabled = false;

    }

    public void switch_state()
    {
        automovement = false;
        StopCoroutine(movement());
        dynamicArm.GetComponent<RoboticArm_tools>().enabled = true;
        dynamicArm.GetComponent<AutoMove>().enabled = false;
        Auto.SetActive(false);

    }

    public void makeGrip()
    {
        temp = true;
    }

    public void makeNotGrip()
    {
        temp = false;
    }

    public void DeletePoint()
    {
        RemoveAtDropDownOptionsData(pointList.value);
    }


    /// <summary>
    /// 移除指定位置   参数:索引
    /// </summary>
    /// <param name="index"></param>
    void RemoveAtDropDownOptionsData(int index) 
    {

        // 安全校验
        if (index >= pointList.options.Count || index < 0)
        {
            return;
        }
        //移除指定位置   参数:索引
        pointList.options.RemoveAt(index);
        //移除相应的节点数据
        t_armList.Remove(t_armList[index]);
        q1_armList.Remove(q1_armList[index]);
        q2_armList.Remove(q2_armList[index]);
        q3_armList.Remove(q3_armList[index]);
        Grip.Remove(Grip[index]);
        Destroy(tagBall[index]);
        tagBall.Remove(tagBall[index]);
        Destroy(lineList[0]);
        lineList.Remove(lineList[0]);
        ResetOrder();//删除中间元素后整体顺序保持不变

    }

    /// <summary>
    /// 添加一个下拉数据
    /// </summary>
    /// <param name="itemText"></param>
    void AddDropDownOptionsData(string itemText)
    {
        //添加一个下拉选项
        Dropdown.OptionData data = new Dropdown.OptionData();
        data.text = itemText;
        //data.image = "指定一个图片做背景不指定则使用默认"；
        pointList.options.Add(data);
    }

    void ClearDropDownOptionsData()
    {
        //直接清理掉所有的下拉选项，
        pointList.ClearOptions();
    }

    void ResetOrder()
    {
        ClearDropDownOptionsData();//先清空，再按照顺序添加
        for (int i = 0; i < tagBall.Count; i++)
        {
            tagBall[i].GetComponentInChildren<Text>().text = (i+1).ToString();
            AddDropDownOptionsData((i+1).ToString());
            if (i<lineList.Count)//路径重新规划
            {
                print("here!!!!!!");
                Vector3[] tmp= new Vector3[2];
                tmp[0] = tagBall[i].transform.position;
                tmp[1] = tagBall[i+1].transform.position;
                lineList[i].GetComponent<VolumetricMultiLineBehavior>().UpdateLineVertices(tmp);
            }
            
        }


    }

}


