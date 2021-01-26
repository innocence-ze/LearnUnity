using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController02 : MonoBehaviour
{
    public float interval;
    public List<string> animalTypes = new List<string>();
    public float range;

    List<GameObject> animalList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateAnimal());
    }

    IEnumerator GenerateAnimal()
    {
        while (true)
        {
            GameObject go = ObjectPoolMgr.Singleton.Utilize(animalTypes[Random.Range(0, animalTypes.Count)]);
            go.transform.rotation = Quaternion.Euler(0, 180, 0);
            go.transform.position = new Vector3(Random.Range(-range, range), 0, 15);
            animalList.Add(go);
            yield return new WaitForSeconds(interval);
        }
    }

    private void Update()
    {
        for(int i = animalList.Count - 1; i >= 0; i--)
        {
            if(animalList[i].transform.position.z < -20.0f)
            {
                ObjectPoolMgr.Singleton.Recycle(animalList[i]);
                animalList.RemoveAt(i);
            }
        }
    }

}
