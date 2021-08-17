using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public GameObject loadScreen;
    public Slider slider;
    public Text load;

    public void loadNextLevel()
    {
        StartCoroutine(loadlevel());
    }

    IEnumerator loadlevel()
    {
        loadScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        operation.allowSceneActivation = false;
        while(!operation.isDone)
        {
            slider.value = operation.progress;
            load.text = operation.progress * 100 + "%";
            if(operation.progress>=0.9f)
            {
                slider.value = 1;
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }

}
