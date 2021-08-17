using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Lean.Touch
{
    public class TeachLevelManager : MonoBehaviour
    {
        [Header("UI")]
        public GameObject talkUI;
        public GameObject Mask;
        public GameObject btn_confirm;
        public Text textLabel;
        public Text goal;
        public Slider hide;
        public RectTransform gameover;
        [Header("Text")]
        public TextAsset textFile;
        public int index;
        List<string> textList = new List<string>();
        bool TextFinish;
        bool buttonDown = false;
        [Header("HighLight")]
        public GameObject contorlBtn;
        public GameObject ToolBtn;
        public GameObject Goal;
        public GameObject Abilities;
        public GameObject setting;
        public GameObject tagObj;
        public bool tag = false;
        [Header("Other")]
        public GameObject ShipBody;
        public GameObject p_color;
        private int color_tag;
        private float currentTime;
        private float coolingTimer;


        public UnityEvent MethodCallBack;
        //MethodCallBack.Invoke();
        // Start is called before the first frame update
        void Awake()
        {
            GetTextFromFile(textFile);
            HighLightObjInit();
        }
        private void OnEnable()
        {
            StartCoroutine(SetTextUI());
            //textLabel.text = textList[index];
            //index++;
        }
        // Update is called once per frame
        void Update()
        {
            float R = ShipBody.GetComponent<PaintIn3D.P3dPixelCounter>().TotalR;
            float G = ShipBody.GetComponent<PaintIn3D.P3dPixelCounter>().TotalG;
            float B = ShipBody.GetComponent<PaintIn3D.P3dPixelCounter>().TotalB;
            float change = ((R + G + B) / 3) / 1024;
            if (change <= 1)
                hide.value = (1 - change) * 10;
            //if(btn_confirm.activeSelf)
            if(hide.value>=0.8f)
            {
                gameOver();
            }
            if (index != textList.Count && TextFinish && buttonDown)
            {
                //textLabel.text = textList[index];
                StartCoroutine(SetTextUI());
                switch_Highlight(index);
            }
            else if (index == textList.Count)
            {
                talkUI.SetActive(false);
                Mask.SetActive(false);
            }
            if(tag)
            {
                if(tagObj.GetComponent<MeshRenderer>().isVisible)
                {
                    tag = false;
                    revive_talkUI();
                    
                }
            }
        }

        public void HighLightObjInit()
        {
            contorlBtn.GetComponent<LeanPulseScale>().enabled = false;
            ToolBtn.GetComponent<LeanPulseScale>().enabled = false;
            Goal.GetComponent<LeanPulseScale>().enabled = false;
            Abilities.GetComponent<LeanPulseScale>().enabled = false;
            setting.GetComponent<LeanPulseScale>().enabled = false;
        }

        public void switch_Highlight(int index)
        {
            HighLightObjInit();
            switch (index)
            {
                case 2:
                    contorlBtn.GetComponent<LeanPulseScale>().enabled = true;
                    break;
                case 3:
                    ToolBtn.GetComponent<LeanPulseScale>().enabled = true;
                    break;
                case 4:
                    Goal.GetComponent<LeanPulseScale>().enabled = true;
                    break;
                case 5:
                    Abilities.GetComponent<LeanPulseScale>().enabled = true;
                    break;
                case 6:
                    setting.GetComponent<LeanPulseScale>().enabled = true;
                    break;
            }
        }

        public void ChangeColor()
        {
            color_tag++;
            if (color_tag % 2 == 0)
            {
                ChangeColor2Blue();
            }
            else
            {
                ChangeColor2Gray();
            }
        }

        public void ChangeColor2Gray()
        {
            p_color.GetComponent<PaintIn3D.P3dPaintSphere>().Color = new Color(133f / 255, 133f / 255, 133f / 255);
            ParticleSystem ps = p_color.GetComponent<ParticleSystem>();
            var main = ps.main;
            var color = new Color(133f / 255, 133f / 255, 133f / 255);
            main.startColor = new ParticleSystem.MinMaxGradient(color);
        }

        public void ChangeColor2Blue()
        {
            p_color.GetComponent<PaintIn3D.P3dPaintSphere>().Color = new Color(106f / 255, 173f / 255, 222f / 255);
            ParticleSystem ps = p_color.GetComponent<ParticleSystem>();
            var main = ps.main;
            var color = new Color(106f / 255, 173f / 255, 222f / 255);
            main.startColor = new ParticleSystem.MinMaxGradient(color);
        }

        public void UpdataText()
        {
            buttonDown = true;
            index++;
        }

        void GetTextFromFile(TextAsset file)
        {
            textList.Clear();
            index = 0;

            var lineData = file.text.Split('\n');

            foreach (var line in lineData)
            {

                textList.Add(line);
            }
        }

        public void revive_talkUI()
        {
            talkUI.SetActive(true);
        }

        public void gameOver()
        {
            goal.text = "1";
            gameover.DOLocalMove(new Vector3(0, 0, 0), 1);
        }

        public void loadTeachLevel()
        {
            SceneManager.LoadScene(6);
        }

        IEnumerator SetTextUI()
        {
            TextFinish = false;
            textLabel.text = "";
            for (int i = 0; i < textList[index].Length; i++)
            {
                textLabel.text += textList[index][i];
                yield return new WaitForSeconds(0.06f);
            }
            TextFinish = true;
            buttonDown = false;
            if (index == 1)
            {
                talkUI.SetActive(false);
                tag = true;
            }

                
        }

    }
}

