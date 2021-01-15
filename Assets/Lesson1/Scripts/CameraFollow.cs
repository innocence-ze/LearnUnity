using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 distance;
    public Vector3 offsite;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
            return;
        Vector3 targetPos = target.position + target.forward * distance.z;
        targetPos.y += distance.y;
        Camera.main.transform.position = targetPos;
        Camera.main.transform.LookAt(target.position + offsite);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPos = target.position + target.forward * distance.z;
        targetPos.y += distance.y;
        Vector3 lastPos = Camera.main.transform.position;
        Vector3 curPos = Vector3.Lerp(lastPos, targetPos, 0.6f);
        Camera.main.transform.position = curPos;
        Camera.main.transform.LookAt(target.position + offsite);
    }
}
