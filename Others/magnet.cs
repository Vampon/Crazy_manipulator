using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnet : MonoBehaviour
{
    //最大距离
    [SerializeField]
    float MaxDistance;
    //最远距离
    [SerializeField]
    float MinDistance;
    //最大的力度
    [SerializeField]
    float MaxForce;
    //磁极标识
    [SerializeField]
    ItemType _Type;

    //动画曲线，用来做磁力的变化
    public AnimationCurve forceCurve;

    Rigidbody thisRig;
    public ItemType mType
    {
        get
        {
            return _Type;
        }
    }
    private void Awake()
    {
        thisRig = GetComponentInParent<Rigidbody>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="robot_head")
        {
            
            magnet script = other.GetComponent<magnet>();
            if (script == null)
                return;
            Rigidbody otherRig = other.attachedRigidbody;

            //获取两极之间的距离
            float distance = Mathf.Max(MinDistance, Vector3.Distance(this.transform.position, this.transform.position));
            
            //根据距离获取当前力度
            float forceAmount = Mathf.Max(GetForce(distance), MaxForce);

            //获取方向
            Vector3 forceDir = Vector3.Normalize(this.transform.parent.position - other.transform.position);
            print(this.transform.parent.localPosition);
            
            //方向以及力度
            Vector3 force = forceDir * forceAmount;

            //如果同极反方向运动
            if (script.mType == this.mType)
                force *= -1;

            //因为都是磁铁所以需要给产生碰撞的两极都添加力
            otherRig.AddForceAtPosition(force, script.transform.position);

            if (thisRig != null)
                thisRig.AddForceAtPosition(-force, script.transform.position);
        }
    }
    //利用动画曲线对磁力度进行获取，根据两极之前的距离变大或变小。
    float GetForce(float value)
    {
        float var = value / MaxDistance;
        float curveValue = forceCurve.Evaluate(var);
        float farce = MaxForce * curveValue;
        return farce;
    }
}
public enum ItemType
{
    South,
    North,
}