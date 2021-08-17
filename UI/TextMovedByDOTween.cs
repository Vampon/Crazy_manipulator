using UnityEngine;

using UnityEngine.UI;

using DG.Tweening;

public class TextMovedByDOTween : MonoBehaviour
{

    public Graphic text;
    public Graphic text1;
    public Graphic text2;
    public Graphic coin1;
    public Graphic coin2;
    public Graphic coin3;
    public Vector3 inipos1;
    public Vector3 inipos2;
    public Vector3 inipos3;
    public Vector3 coinpos1;
    public Vector3 coinpos2;
    public Vector3 coinpos3;
    // Use this for initialization

    void Start()
    {
        inipos1 = text.transform.localPosition;
        inipos2 = text1.transform.localPosition;
        inipos3 = text2.transform.localPosition;
        //text = GameObject.Find("Text").GetComponent<Text>(); ;
        coinpos1 = coin1.transform.localPosition;
        coinpos2 = coin2.transform.localPosition;
        coinpos3 = coin3.transform.localPosition;

    }

    // Update is called once per frame

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {

            TextMoved(text,text.transform.position);
            text.transform.DOLocalMove(inipos1, 2f);
            //text.transform.localPosition = Vector3.MoveTowards(text.transform.localPosition, new Vector3(0,0,0) , 2 * Time.deltaTime);

        }

    }

    public void moveText1()
    {
        TextMoved(text,text.transform.position);
        //text.transform.DOLocalMove(inipos1, 1);
        text.transform.DOLocalMove(inipos1, 2f);
    }

    public void moveText2()
    {
        TextMoved(text1,text1.transform.position);
        text1.transform.DOLocalMove(inipos2, 2f);
    }

    public void moveText3()
    {
        TextMoved(text2,text2.transform.position);
        text2.transform.DOLocalMove(inipos3, 2f);
    }

    public void CoinMoveText1()
    {
        TextMoved(coin1, coin1.transform.position);
        coin1.transform.DOLocalMove(coinpos1, 2f);
    }

    public void CoinMoveText2()
    {
        TextMoved(coin2, coin2.transform.position);
        coin2.transform.DOLocalMove(coinpos1, 2f);
    }

    public void CoinMoveText3()
    {
        TextMoved(coin3, coin3.transform.position);
        coin3.transform.DOLocalMove(coinpos3, 2f);
    }


    private void TextMoved(Graphic graphic,Vector3 iniPos)
    {

        //获得Text的rectTransform，和颜色，并设置颜色微透明

        RectTransform rect = graphic.rectTransform;

        Color color = graphic.color;

        graphic.color = new Color(color.r, color.g, color.b, 0);

        //设置一个DOTween队列

        Sequence textMoveSequence = DOTween.Sequence();

        //设置Text移动和透明度的变化值

        Tweener textMove01 = rect.DOMoveY(rect.position.y + 50, 0.5f);

        Tweener textMove02 = rect.DOMoveY(rect.position.y + 100, 0.5f);

        Tweener textColor01 = graphic.DOColor(new Color(color.r, color.g, color.b, 1), 0.5f);

        Tweener textColor02 = graphic.DOColor(new Color(color.r, color.g, color.b, 0), 0.5f);

        Tweener textMove03 = rect.DOMoveY(rect.position.y - 50, 0.5f);

        Tweener textMove04 = rect.DOMoveY(rect.position.y - 100, 0.5f);
        //Append 追加一个队列，Join 添加一个队列

        //中间间隔一秒

        //Append 再追加一个队列，再Join 添加一个队列

        textMoveSequence.Append(textMove01);

        textMoveSequence.Join(textColor01);

        //textMoveSequence.AppendInterval(1);

        textMoveSequence.Append(textMove02);

        textMoveSequence.Join(textColor02);

        //textMoveSequence.Append(textMove03);//返回初始位置
        //textMoveSequence.AppendInterval(1);
        //textMoveSequence.Append(textMove04);



    }


}