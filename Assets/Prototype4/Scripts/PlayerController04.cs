using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController04 : MonoBehaviour
{
    public float speed;
    public bool isBuffing;
    public float buffStrength;
    public float buffTime;

    private Vector3 direction;
    private float speedScale;

    GameObject ring;


    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Singleton.AddMsgListener("PlayerMove", OnPlayerMove);
        GameController.Singleton.AddMsgListener("CameraRotate", ChangeDirection);
        GameController.Singleton.AddMsgListener("Buffing", OnBuffing);

        direction = Camera.main.transform.forward;
        direction.y = 0;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Vertical"))
        {
            speedScale = Input.GetAxis("Vertical");
            GameController.Singleton.msgList.Add("PlayerMove");
        }

        if(transform.position.y < -2)
        {
            OnDie();
        }

        if (isBuffing)
        {
            ring.transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Buff"))
        {
            ObjectPoolMgr.Singleton.Recycle(other.gameObject);
            GameController.Singleton.msgList.Add("Buffing");
        }
    }

    void OnBuffing()
    {
        if (isBuffing)
            return;

        StartCoroutine(BuffCoroutine());
    }

    IEnumerator BuffCoroutine()
    {
        isBuffing = true;
        ring = ObjectPoolMgr.Singleton.Utilize("Ring");
        yield return new WaitForSeconds(buffTime);
        isBuffing = false;
        ObjectPoolMgr.Singleton.Recycle(ring);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isBuffing)
        {
            Vector3 force = (collision.transform.position - transform.position).normalized * buffStrength;
            collision.rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }


    void OnPlayerMove()
    {
        rb.AddForce(direction * speedScale * speed );
    }

    void ChangeDirection()
    {
        direction = Camera.main.transform.forward;
        direction.y = 0;
    }

    //滚落下平面后恢复原位
    void OnDie()
    {
        transform.position = Vector3.zero;
        rb.velocity = Vector3.zero;
    }
}
