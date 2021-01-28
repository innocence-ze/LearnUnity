using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGReset : MonoBehaviour
{
    Vector3 startPos;
    float bgSize;

    // Start is called before the first frame update
    void Start()
    {
        bgSize = GetComponent<BoxCollider>().size.x;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < startPos.x - bgSize / 2)
        {
            transform.position = startPos;
        }
    }
}
