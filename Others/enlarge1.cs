using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enlarge1 : MonoBehaviour
{
    public GameObject right;
    public GameObject left;
    public Text time;
    public int T;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //T = int.Parse(time.text);
    }
    public void enlarge()
    {
        right.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        left.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        Invoke("ensmall", 10f);
    }

    public void ensmall()
    {
        right.transform.localScale = new Vector3(1, 1, 1);
        left.transform.localScale = new Vector3(1, 1, 1);
    }
}
