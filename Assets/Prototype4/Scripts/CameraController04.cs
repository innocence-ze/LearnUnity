using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController04 : MonoBehaviour
{
    public Vector3 rotatePoint;
    public float rotateSpeed;

    float rotateScale;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Singleton.AddMsgListener("CameraRotate", OnCameraRotate);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            rotateScale = Input.GetAxis("Horizontal");
            GameController.Singleton.msgList.Add("CameraRotate");
        }
    }

    void OnCameraRotate()
    {

        transform.RotateAround(rotatePoint, Vector3.up, rotateScale * rotateSpeed * Time.deltaTime * -1);
    }
}
