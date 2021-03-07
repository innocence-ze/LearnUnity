using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropController05 : MonoBehaviour
{
    public int score;
    public string explosionName;

    GameController05 gc;


    // Start is called before the first frame update
    void Start()
    {
        GameController.Singleton.AddMsgListener("Boom", OnBoom);
        GameController.Singleton.AddMsgListener("ReturnMenu", OnReturnMenu);
        gc = FindObjectOfType<GameController05>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        gc.UpdateScore(score);
        if (CompareTag("Bomb"))
        {
            GameController.Singleton.msgList.Add("Boom");
        }
        else
        {
            var explosion = ObjectPoolMgr.Singleton.Utilize(explosionName);
            explosion.transform.position = transform.position;
            ObjectPoolMgr.Singleton.Recycle(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sensor"))
        {
            if (gameObject.CompareTag("Bomb"))
            {

            }
            else
            {
                gc.UpdateScore(-score);
                gc.Defeat();
            }
            ObjectPoolMgr.Singleton.Recycle(gameObject);
        }
    }

    void OnBoom()
    {
        if (gameObject.activeSelf)
        {
            var explosion = ObjectPoolMgr.Singleton.Utilize(explosionName);
            explosion.transform.position = transform.position;
            ObjectPoolMgr.Singleton.Recycle(gameObject);
        }
    }

    void OnReturnMenu()
    {
        if (gameObject.activeSelf)
        {
            ObjectPoolMgr.Singleton.Recycle(gameObject); 
        }
    }
}
