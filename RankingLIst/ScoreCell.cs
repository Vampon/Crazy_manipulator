using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCell : MonoBehaviour
{
    public Text nameText;
    public Text scoreText;
    
    public void SetModel(UserData data)
    {
        nameText.text = data.userName;
        scoreText.text = data.coin.ToString();
    }
}
