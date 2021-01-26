using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car01 : MonoBehaviour
{
    public List<GameObject> carPrefab = new List<GameObject>();
    public float carSpeed;
    public float carRotSpeed;
    public float maxForwardWheelsRotate;

    private GameObject car;
    private Rigidbody rb;
    private List<GameObject> wheels = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        car = carPrefab[Random.Range(0, carPrefab.Count)];
        car = Instantiate(car);
        car.transform.parent = transform;
        car.transform.position = new Vector3(0, 0, -5);

        rb = car.AddComponent<Rigidbody>();
        rb.mass = 10;

        wheels.Add(car.transform.Find("Wheel_fl").gameObject);
        wheels.Add(car.transform.Find("Wheel_fr").gameObject);
        wheels.Add(car.transform.Find("Wheel_rl").gameObject);
        wheels.Add(car.transform.Find("Wheel_rr").gameObject);

        FindObjectOfType<CameraFollow01>().target = car.transform;

    }

    // Update is called once per frame
    void Update()
    {
        CarMoveUpdate();
    }

    private void CarMoveUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //转动前轮用于转弯
        var temp = wheels[0].transform.localRotation.eulerAngles;
        wheels[0].transform.localRotation = Quaternion.Euler(new Vector3(temp.x, x * maxForwardWheelsRotate, 0));
        wheels[1].transform.localRotation = wheels[0].transform.localRotation;

        //有速度时
        if (Mathf.Abs(y) >0.05f)
        {
            car.transform.Rotate(0, x * carRotSpeed * Time.deltaTime * (y > 0? 1 :-1), 0);
            car.transform.position += y * car.transform.forward * carSpeed * Time.deltaTime;
            //转动四轮用于移动
            foreach(var w in wheels)
            {
                w.transform.Rotate(y * Time.deltaTime * carSpeed * 360, 0, 0);
            }
        }
    }
}
