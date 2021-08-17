using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DG.Tweening;

public class UIManager : MonoBehaviour
{

    public GameObject CanvesStart;
    public GameObject uploadScore;
    public GameObject Auto;
    public GameObject magnet;
    public RectTransform SettingPanel;
    public RectTransform gameover;
    public RectTransform group1;
    public RectTransform group2;
    public RectTransform notification;
    public RectTransform RankingList;
    public RectTransform info;
    public RectTransform ModeSelect;
    public Text finshScore;
    public Text totalTime;
    public int time;
    public int totaltime;
    public data myData;
    public int score;
    public bool over;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        show_notification();
        time = GameObject.Find("Canvas").GetComponent<TimeDelete>().TotalTime;
        over = false;
        Auto = GameObject.Find("Auto");
    }

    public void Quit()
    {
        Application.Quit();
    }


    public void back()
    {
        //SettingPanel.SetActive(false);
        //CanvesStart.SetActive(true);
        SettingPanel.DOLocalMove(new Vector3(0, -500, 0), 1);
    }

    public void showMode()
    {
        ModeSelect.DOLocalMove(new Vector3(0, -150, 0), 1);
    }

    public void BackMode()
    {
        ModeSelect.DOLocalMove(new Vector3(0, -560, 0), 1);
    }

    public void showSetting()
    {
        //SettingPanel.SetActive(true);
        SettingPanel.DOLocalMove(new Vector3(0, 0, 0), 1);
    }

    public void loadHome()
    {
        SaveGame();
        SceneManager.LoadScene(0);
    }

    public void restart()
    {
        SaveGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void load_Level1()
    {
        SceneManager.LoadScene(1);
    }

    public void load_LevelSelect()
    {
        SceneManager.LoadScene(1);
    }
    public void nextLevel()
    {
        SaveGame();
        LoadGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//进入下一关
    }

    // Update is called once per frame
    void Update()
    {
        time = GameObject.Find("Canvas").GetComponent<TimeDelete>().TotalTime;
        totaltime = GameObject.Find("Canvas").GetComponent<TimeDelete>().totaltime;
        score = GameObject.Find("Cylinder").GetComponent<GameManager>().score;
        finshScore.text = "+"+(score*100).ToString();
        totalTime.text = secondTominute(totaltime);
        if (time<=0)
        {
            if(!over)
            {
                Debug.Log("Game Over");
                gameOver();
                over = true;
            }
            
        }
    }

    public string secondTominute(int time)
    {
        string t="";
        int forward=0, after=0;
        while(time>60)
        {
            forward += 1;
            time -= 60;
        }
        after = time;
        t = forward.ToString() + ":" + after.ToString();
        return t;
    }

    public void gameOver()
    {
        gameover.DOLocalMove(new Vector3(0, 0, 0), 1);
        //Time.timeScale = 0;
        addCoin();
        //gameover.SetActive(true);
        Debug.Log("Game over");
        SaveGame();
    }

    public void addCoin()
    {
        myData.coin += score*100;//根据分数获取金币奖励
    }

    public void SaveGame()
    {
        Debug.Log(Application.persistentDataPath);
        if(!Directory.Exists(Application.persistentDataPath + "/game_SaveData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_SaveData");
        }

        BinaryFormatter formatter = new BinaryFormatter();//二进制转换

        FileStream file = File.Create(Application.persistentDataPath + "/game_SaveData/data.txt");

        var json = JsonUtility.ToJson(myData);

        formatter.Serialize(file, json);

        file.Close();
    }

    public void LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();

        if(File.Exists(Application.persistentDataPath + "/game_SaveData/data.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_SaveData/data.txt",FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), myData);
            file.Close();
        }
    }

    public void show_notification()
    {
        notification.DOLocalMove(new Vector3(0, 0, 0), 1);
    }

    public void back_notification()
    {
        notification.DOLocalMove(new Vector3(0, 500, 0), 1);
    }

    public void switch_group1()
    {
        group2.DOLocalMove(new Vector3(-300, 0, 0), 1);
        group1.DOLocalMove(new Vector3(0, 0, 0), 1);
    }

    public void switch_group2()
    {
        group1.DOLocalMove(new Vector3(-300, 0, 0), 1);
        group2.DOLocalMove(new Vector3(0, 0, 0), 1);
        Auto.SetActive(true);
    }

    public void show_ranking()
    {
        RankingList.DOLocalMove(new Vector3(0, 0, 0), 1);
    }

    public void back_ranking()
    {
        RankingList.DOLocalMove(new Vector3(0, 800, 0), 1);
    }

    public void show_upload()
    {
        uploadScore.SetActive(true);
    }

    public void back_upload()
    {
        uploadScore.SetActive(false);
    }
    
    public void magnetview()
    {
        magnet.SetActive(true);
        Invoke("disappear_magnet", 8f);
    }

    public void disappear_magnet()
    {
        magnet.SetActive(false);
    }

    public void showInfo()
    {

        info.transform.localScale = Vector3.zero;
        info.DOScale(1.706666f, .3f);
    }

    public void backInfo()
    {
        info.DOScale(0, .3f);
    }


    public void loadNetworkMode()
    {
        SceneManager.LoadScene(5);
    }

    public void loadTeachLevel()
    {
        SceneManager.LoadScene(6);
    }
}
