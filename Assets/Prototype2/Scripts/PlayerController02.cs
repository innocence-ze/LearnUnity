using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController02 : MonoBehaviour
{
    public float speed;
    public float range;

    public string pizzaStr;


    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= range && transform.position.x >= -range)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.position += horizontalInput * transform.right * speed * Time.deltaTime;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -range, range), transform.position.y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var pizza = ObjectPoolMgr.Singleton.Utilize(pizzaStr);
            pizza.transform.position = transform.position;
        }
    }
}
