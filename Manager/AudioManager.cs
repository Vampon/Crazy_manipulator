using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Transition.Method;

public class AudioManager : MonoBehaviour
{
    public AudioSource music;
    public Slider sd;
    public Slider vol;
    // Start is called before the first frame update
    void Start()
    {
        //music.Play();
        
        music.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //control_audio();
    }

    public void control_sound()
    {
        music.volume = sd.value;
    }

    public void control_audio()
    {
        foreach (LeanPlaySound go in Resources.FindObjectsOfTypeAll(typeof(LeanPlaySound)))
        {
            go.Data.Volume = vol.value;
        }

    }
}
