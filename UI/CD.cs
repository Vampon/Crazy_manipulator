using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ricimi;

public class CD : MonoBehaviour
{
    private float currentTime; 
    public Image coolingImage;
    private float coolingTimer;

    void Start()
    {
        //coolingImage = transform.GetChild(5).GetComponent();
        coolingImage.raycastTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateImage();
    }

    public void OnBtnClickSkill(float timer)
    {
        coolingTimer = timer;
        currentTime = 0.0f;
        coolingImage.fillAmount = 1.0f;

    }

    private void UpdateImage()
    {
        if (currentTime < coolingTimer)
        {
            currentTime += Time.deltaTime;
            coolingImage.fillAmount = 1 - currentTime / coolingTimer;
            if (coolingImage.fillAmount != 0)
            {
                coolingImage.raycastTarget = true;
                gameObject.GetComponent<BasicButton>().enabled = false;
            }
            else
            {
                coolingImage.raycastTarget = false;
                gameObject.GetComponent<BasicButton>().enabled = true;
            }
        }
    }

}
