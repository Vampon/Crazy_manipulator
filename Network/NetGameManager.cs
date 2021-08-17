using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;
using UnityEngine.Networking;
using UnityEngine.Events;

public class PlayerData
{

    public string userName;
    public int score;
    public PlayerData(string userName, int score)
    {
        this.userName = userName;
        this.score = score;
    }
}

public class NetGameManager : MonoBehaviour
{
    private const string url = "http://dreamlo.com/lb/";
    private const string privateCode = "otSnExoK80apTp-ZdjA5Ngz6vQKl2_LUSf_MZTaJQrKA";
    private const string publicCode = "6117a29c8f40bb6e98906a26";
    public data mydata;
    public GameObject cubePrefab;
    public GameObject target;
    public Transform cubePos;
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
        coin = mydata.coin;
        score = 0;
        index = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            if(GameObject.Find("Robotic_arm_Net(Clone)"))
                NetworkingScore = GameObject.Find("Robotic_arm_Net(Clone)").GetComponentInChildren<Text>();
        }
        
    }
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "NetCube")   //!bug：这里需要将子物体的tag也全部改为cube
        {
            score++;
            print("in!");
            if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                //GameObject cube = Instantiate(cubePrefab, cubePos.position, Quaternion.Euler(0, 0, 0));//物块进入区域后重新生成
                //cube.transform.parent = target.transform;//把实例化的物体放到父物体位置之下
                other.gameObject.transform.position = cubePos.position;
                StartCoroutine(RankingList.GetHighScore(GetHighScoreCallBack));
                NetworkingScore.text = score.ToString();
            }
        }
    }
    public void testinstance()
    {
        Instantiate(cubePrefab, cubePos.position, Quaternion.Euler(0, 0, 0));//物块进入区域后重新生成
    }

    public void addCoin()
    {
        mydata.coin += score * 100;
    }

    public static IEnumerator CreateHighScore(string usrname, int score)
    {
        UnityWebRequest request = new UnityWebRequest(url + privateCode + "/add/" + UnityWebRequest.EscapeURL(usrname) + "/" + score);
        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("添加成功");
        }
    }

    public static IEnumerator GetHighScore(UnityAction<List<UserData>> callBack)
    {
        UnityWebRequest request = UnityWebRequest.Get(url + publicCode + "/json");

        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            var data = JsonMapper.ToObject(request.downloadHandler.text);
            Debug.Log(data);
            var userDatas = data["dreamlo"]["leaderboard"]["entry"];
            List<UserData> userDataList = new List<UserData>();
            if (userDatas.IsArray)
            {
                foreach (JsonData user in userDatas)
                {
                    userDataList.Add(new UserData(user["name"].ToString(), int.Parse(user["score"].ToString())));

                    Debug.Log(user["name"]);
                }
            }
            else
            {
                userDataList.Add(new UserData(userDatas["name"].ToString(), int.Parse(userDatas["score"].ToString())));
                Debug.Log(userDatas["name"]);
            }
            callBack(userDataList);
        }
    }

    public void uploadScore()
    {

        //StartCoroutine(CreateHighScore(PlayerName.text, ));
    }

    public void GetHighScoreCallBack(List<UserData> datas)
    {
        foreach (var data in datas)
        {
            
        }
    }


}
