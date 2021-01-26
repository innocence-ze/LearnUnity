using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaController02 : MonoBehaviour
{
    public string animalTag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(animalTag))
        {
            ObjectPoolMgr.Singleton.Recycle(other.gameObject);
            ObjectPoolMgr.Singleton.Recycle(gameObject);
        }
    }
    private void Update()
    {
        if(transform.position.z > 20)
        {
            ObjectPoolMgr.Singleton.Recycle(gameObject);
        }
    }
}
