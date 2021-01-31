using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController04 : MonoBehaviour
{

    public List<GameObject> enemyList = new List<GameObject>();
    int level = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Singleton.AddMsgListener("NextLevel", OnNextLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyList.Count == 0)
        {
            GameController.Singleton.msgList.Add("NextLevel");
        }

        for(int i = enemyList.Count - 1; i >= 0; i--)
        {
            if(enemyList[i].transform.position.y < -2)
            {
                enemyList[i].GetComponent<EnemyController04>().Die();
                enemyList.Remove(enemyList[i]);
            }
        }
    }

    void OnNextLevel()
    {
        level++;
        for (int i = 0; i < level + 2; i++)
        {
            var enemy = ObjectPoolMgr.Singleton.Utilize("Enemy");
            enemyList.Add(enemy);
            enemy.GetComponent<EnemyController04>().Revive();
        }
        for(int i = 0; i < level / 3 + 1; i++)
        {
            ObjectPoolMgr.Singleton.Utilize("Buff").transform.position = new Vector3(Random.Range(-4, 4f), 0, Random.Range(-4, 4f));
        }
        GameController.Singleton.msgList.Add("EnemyCreated");
    }
}
