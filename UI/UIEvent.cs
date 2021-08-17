using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEvent : MonoBehaviour
{

    public GameObject CanvesStart;
    public GameObject Panel;
    public GameObject LeftPanel;
    public GameObject MiddlePanel;
    public GameObject RightPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Switch()
    {
        Panel.SetActive(true);
        CanvesStart.SetActive(false);
    }

    public void back()
    {
        Panel.SetActive(false);
        CanvesStart.SetActive(true);
    }

    public void left()
    {
        LeftPanel.SetActive(true);
        MiddlePanel.SetActive(false);
        RightPanel.SetActive(false);
    }
    public void middle()
    {
        LeftPanel.SetActive(false);
        MiddlePanel.SetActive(true);
        RightPanel.SetActive(false);
    }

    public void right()
    {
        LeftPanel.SetActive(false);
        MiddlePanel.SetActive(false);
        RightPanel.SetActive(true);
    }

    public void load_camera()
    {
        SceneManager.LoadScene(1);
    }



    // Update is called once per frame
    void Update()
    {

    }
}