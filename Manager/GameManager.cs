using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public data mydata;
    public GameObject FinishPanle;
    public GameObject coinNotEnough;
    public GameObject cubePrefab;
    public GameObject target;
    public Transform cubePos;
    public bool isFinishing;
    public Text Coin;
    public Text Goal;
    public Text NetworkingScore;
    private int coin;
    public int score;
    public float screwangle;
    public int spend1 = 100;
    public int spend2 = 300;
    public int spend3 = 200;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        FinishPanle.SetActive(false);
        coinNotEnough.SetActive(false);
        coin = mydata.coin;
        score = 0;
        index = SceneManager.GetActiveScene().buildIndex;
        isFinishing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 5)
        {
            NetworkingScore = GameObject.Find("Robotic_arm_Net(Clone)").GetComponentInChildren<Text>();
        }
        //isFinishing = GameObject.Find("SM_PolygonCity_Veh_Car_Small_Wheel_fl").GetComponent<AssambleObject>().isfinishing;
        Coin.text = mydata.coin.ToString();
        Goal.text = score.ToString();
        if (index == 3)
        {
            screwangle = GameObject.Find("RObotic_arm_auto").GetComponent<RoboticArm_IK>().ScrewAngle;//获取当前加工件是否加工完成
            if (screwangle >= 500) isFinishing = true;
        }
        if (index == 4)//第三关
        {
            score = GameObject.Find("SM_PolygonCity_Veh_Car_Small_Wheel_fl").GetComponent<AssambleObject>().score;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        print("in!");
        if (other.gameObject.tag == "cube")   //!bug：这里需要将子物体的tag也全部改为cube
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                GameObject cube = Instantiate(cubePrefab, cubePos.position, Quaternion.Euler(0, 0, 0));//物块进入区域后重新生成
                cube.transform.parent = target.transform;//把实例化的物体放到父物体位置之下
            }
            if (SceneManager.GetActiveScene().buildIndex == 3)
                Destroy(other.transform.parent.gameObject);
            Destroy(other.gameObject);

            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                if(isFinishing)
                {
                    score += 1;  //对于第二关，需要加工完成才可以加分
                }
                else
                {
                    if(score>0)
                        score -= 1;//扣分
                }
                isFinishing = false;
            }
            else//第一关
                score += 1;
            //FinishPanle.SetActive(true);
        }
    }
    public void testinstance()
    {
        Instantiate(cubePrefab, cubePos.position, Quaternion.Euler(0, 0, 0));//物块进入区域后重新生成
    }

    public void addCoin()
    {
        mydata.coin += score*100;
    }

    public void spendCoin_1()
    {
        if(mydata.coin - spend1>=0)
            mydata.coin -= spend1;
        else
        {
            coinNotEnough.SetActive(true);
        }
    }

    public void spendCoin_2()
    {
        if (mydata.coin - spend2 >= 0)
            mydata.coin -= spend2;
        else
        {
            coinNotEnough.SetActive(true);
        }
    }

    public void spendCoin_3()
    {
        if (mydata.coin - spend3 >= 0)
            mydata.coin -= spend3;
        else
        {
            coinNotEnough.SetActive(true);
        }
    }

    public void exit_coin()
    {
        coinNotEnough.SetActive(false);
    }
}
