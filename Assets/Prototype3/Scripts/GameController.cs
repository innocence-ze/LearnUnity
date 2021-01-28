using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController s_singleton = null;
    public static GameController Singleton
    {
        get
        {
            if (s_singleton == null)
            {
                s_singleton = FindObjectOfType<GameController>();
            }
            if (s_singleton == null)
            {
                Debug.LogError("cannot find Game controller!!");
            }
            return s_singleton;
        }
    }


    public List<string> msgList = new List<string>();
    Dictionary<string, MsgListener> msgDic = new Dictionary<string, MsgListener>();

    public delegate void MsgListener();

    public void AddMsgListener(string name, MsgListener ml)
    {
        if (msgDic.ContainsKey(name))
            msgDic[name] += ml;
        else
            msgDic[name] = ml;
    }

    public void RemoveMsgListener(string name, MsgListener ml)
    {
        if (msgDic.ContainsKey(name))
            msgDic[name] -= ml;
    }

    public void FireMsgListener(string name)
    {
        if (msgDic.ContainsKey(name))
            msgDic[name]();
    }

    private void Update()
    {
        if(msgList.Count > 0)
        {
            for(int i = msgList.Count - 1; i >= 0; i--)
            {
                FireMsgListener(msgList[i]);
                msgList.RemoveAt(i);
            }
        }
    }

}
