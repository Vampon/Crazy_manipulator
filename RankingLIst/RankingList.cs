using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using UnityEngine.Events;
using UnityEngine.UI;

public class UserData
{
    
    public string userName;
    public int coin;
    public UserData(string userName,int coin)
    {
        this.userName = userName;
        this.coin = coin;
    }
}
public class RankingList : MonoBehaviour
{
    public InputField name;
    public data myData;
    private const string url = "http://dreamlo.com/lb/";
    private const string privateCode = "EKB7YQFhUUqTo_T0Y7sF_wzXOMJALqJ02jzS2Q8SATyQ";
    private const string publicCode = "6076e94e8f40bb5afc833c71";
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CreateHighScore("test", 1000));
        //StartCoroutine(GetHighScore());
    }

    // Update is called once per frame
    void Update()
    {
        
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
            if(userDatas.IsArray)
            {
                foreach(JsonData user in userDatas)
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

    public static IEnumerator CreateHighScore(string usrname,int coin)
    {
        UnityWebRequest request = new UnityWebRequest(url + privateCode + "/add/" + UnityWebRequest.EscapeURL(usrname) + "/" + coin);
        yield return request.SendWebRequest();
        if(request.isHttpError || request.isNetworkError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("添加成功");
        }
    }

    public void uploadScore()
    {
        StartCoroutine(CreateHighScore(name.text, myData.coin));
    }
}
