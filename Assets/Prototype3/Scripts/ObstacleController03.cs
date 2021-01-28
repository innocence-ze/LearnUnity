using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController03 : MonoBehaviour
{
    public float interval;
    public List<string> obsName;
    public float destoryX;
    public Vector3 generatePos;
    Queue<GameObject> activeObsQueue = new Queue<GameObject>();

    Coroutine generateObsCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        GameController.Singleton.AddMsgListener("GameOver", OnGameOver);
        GameController.Singleton.AddMsgListener("Restart", OnRestart);
        generateObsCoroutine = StartCoroutine(GenerateObstacle());
    }

    // Update is called once per frame
    void Update()
    {
        if(activeObsQueue.Peek().transform.position.x < destoryX)
        {
            ObjectPoolMgr.Singleton.Recycle(activeObsQueue.Dequeue());
        }
    }

    IEnumerator GenerateObstacle()
    {
        while (true)
        {
            var go = ObjectPoolMgr.Singleton.Utilize(obsName[Random.Range(0, obsName.Count)]);
            activeObsQueue.Enqueue(go);
            go.transform.position = generatePos;
            yield return new WaitForSeconds(interval);
        }
    }

    void OnGameOver()
    {
        var gos = FindObjectsOfType<MoveTowards>();
        for(int i = 0; i < gos.Length; i++)
        {
            gos[i].StopMoving();
        }
        StopCoroutine(generateObsCoroutine);
    }

    void OnRestart()
    {
        var gos = FindObjectsOfType<PreInfo>();
        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i].gameObject.activeSelf)
            {
                ObjectPoolMgr.Singleton.Recycle(gos[i].gameObject);
            }
        }
        StartCoroutine(GenerateObstacle());
    }
}
