using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DoorPoint
{
    public Vector3 top_left;
    public Vector3 top_right;
    public Vector3 buttom_left;
    public Vector3 buttom_right;
}

public class LocationMark : MonoBehaviour
{

    public Vector2 Size;
    DoorPoint m_point;

    private Vector3 m_postion;
    private Vector3 m_rotation;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_postion = this.gameObject.transform.position;
        m_rotation = new Vector3(this.gameObject.transform.rotation.x, this.gameObject.transform.rotation.y, this.gameObject.transform.rotation.z);
    }

    //绘制出参考标点
    void OnDrawGizmos()
    {
        Vector2 halfSize = Size * 0.5f;
        Gizmos.color = Color.red;
        float lineLength = Mathf.Min(Size.x, Size.y);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * lineLength);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * lineLength);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * lineLength);

        Gizmos.color = Color.blue;
        Vector3 topLeft = transform.position - (transform.right * halfSize.x) + (transform.up * Size.y) / 2;
        Vector3 topRight = transform.position + (transform.right * halfSize.x) + (transform.up * Size.y) / 2;

        Vector3 bottomLeft = transform.position - (transform.right * halfSize.x) - (transform.up * Size.y) / 2;
        Vector3 bottomRight = transform.position + (transform.right * halfSize.x) - (transform.up * Size.y) / 2;

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }

}