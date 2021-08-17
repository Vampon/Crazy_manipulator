using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeDelete : MonoBehaviour
{
    public Text time;
    public int TotalTime = 60;//计时
    public int totaltime;//总时间
    public Image timeAni;

    void Start()
    {
        StartCoroutine(Time());
        totaltime = TotalTime;
        timeAni = GameObject.Find("Background Animation").GetComponent<Image>();
    }

    IEnumerator Time()
    {
        while (TotalTime >= 0)
        {

            time.text = TotalTime.ToString();   
            yield return new WaitForSeconds(1);
            TotalTime--;
            if (TotalTime <= 15)//预警
            {
                timeAni.color = Color.red;
            }
            else
            {
                timeAni.color = new Color((255 / 255f), (255 / 255f), (255 / 255f), (2 / 255f));
            }
        }
    }

    void update()
    {
        if (TotalTime <= 15)//预警
        {
            print("time not enough");
            timeAni.color = new Color((255 / 255f), (0 / 255f), (0 / 255f), (2 / 255f));
        }
    }

    public void add_Time()
    {
        TotalTime += 10;
        totaltime += 10;
    }

  

}