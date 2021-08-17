using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quality : MonoBehaviour
{
    public Dropdown dropdown;
    private void Start()
    {
        dropdown.value = 3;
    }
    private void Update()
    {
        int value = dropdown.value;
        Colortransform(value);
    }
    public void Colortransform(int value)
    {
        switch (value)
        {
            case 0:
                QualitySettings.SetQualityLevel(0, true);
                break;
            case 1:
                QualitySettings.SetQualityLevel(1, true);
                break;
            case 2:
                QualitySettings.SetQualityLevel(2, true);
                break;
            case 3:
                QualitySettings.SetQualityLevel(3, true);
                break;
            case 4:
                QualitySettings.SetQualityLevel(4, true);
                break;
            case 5:
                QualitySettings.SetQualityLevel(5, true);
                break;
        }
    }
}
