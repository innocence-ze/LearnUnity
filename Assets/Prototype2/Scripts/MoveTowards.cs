using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    float curSpeed;

    private void Start()
    {
        RestartMoving();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * curSpeed * direction;
    }

    public void RestartMoving()
    {
        curSpeed = speed;
    }

    public void StopMoving()
    {
        curSpeed = 0;
    }

}
