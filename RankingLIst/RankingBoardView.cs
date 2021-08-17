using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingBoardView : MonoBehaviour
{
    public GameObject scoreCell;
    List<GameObject> cellList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RankingList.GetHighScore(GetHighScoreCallBack));
    }

    public void GetHighScoreCallBack(List<UserData> datas)
    {
        foreach(var data in datas)
        {
            var cell = Instantiate(scoreCell, transform);
            cell.GetComponent<ScoreCell>().SetModel(data);
            cellList.Add(cell);
        }
    }

    public void refresh()
    {
        for(int i=0;i<cellList.Count; i++)
        {
            Destroy(cellList[i]);
        }
        cellList.Clear();
        Invoke("invokeAdd", 1f);
    }

    public void invokeAdd()
    {
        StartCoroutine(RankingList.GetHighScore(GetHighScoreCallBack));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
