using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(Text))]
[AddComponentMenu("Photon Networking/Photon Score View")]
public class PhotonViewScore : MonoBehaviour, IPunObservable
{
    public PhotonView m_PhotonView;

    Text Score;

    void Awake()
    {
        this.Score = GetComponent<Text>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {


        if (stream.isWriting == true)
        {
            stream.SendNext(this.Score.text);
        }
        else
        {
            if (m_PhotonView.isMine == true)
                this.Score.text = (string)stream.ReceiveNext();
        }
    }
}