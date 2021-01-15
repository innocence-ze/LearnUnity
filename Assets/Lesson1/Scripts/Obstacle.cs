using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float minRange;
    public float maxRange;

    public List<GameObject> obstacles = new List<GameObject>();

    [Range(5,36)]
    public int obstacleCount;

    private GameObject obstacle;


    // Start is called before the first frame update
    void Start()
    {
        float interval = (maxRange - minRange) / (obstacleCount - 1);
        for(int i = 0; i< obstacleCount; i++)
        {
            obstacle = Instantiate(obstacles[Random.Range(0, obstacles.Count)]);
            obstacle.transform.position = new Vector3(0, 0, minRange + i * interval);
            obstacle.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
