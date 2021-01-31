using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController04 : MonoBehaviour
{
    private GameObject player;
    Rigidbody rb;

    public float speed;

    // Start is called before the first frame update
    void OnEnable()
    {
        player = FindObjectOfType<PlayerController04>().gameObject;
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        rb.AddForce(direction * speed);
    }

    public void Die()
    {
        ObjectPoolMgr.Singleton.Recycle(gameObject);
        rb.velocity = Vector3.zero;
    }

    public void Revive()
    {
        float randX = Random.Range(-8, 8.0f);
        float randZ = Random.Range(-8, 8.0f);
        transform.position = new Vector3(randX, 0, randZ);
        rb.velocity = Vector3.zero;
    }

}
