using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController05 : MonoBehaviour
{

    LevelData level;
    Coroutine mainCoroutine;
    Vector3 originalGravity;

    int score = 0;

    public List<LevelData> levelDataList = new List<LevelData>();
    public List<string> propName = new List<string>();
    public float xRange, yInitPos;
    public float torqueRange;

    public GameObject GamePanel;
    public GameObject MenuPanel;
    public GameObject DefeatPanel;
    public Text scoreText;

    public BoxCollider sensor;

    void OnSetGameLevel(int l)
    {
        level = levelDataList[l];
    }

    // Start is called before the first frame update
    void Start()
    {
        originalGravity = Physics.gravity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChooseLevel(int level)
    {
        OnSetGameLevel(level);
        Physics.gravity = originalGravity * this.level.gravityScale;
        mainCoroutine = StartCoroutine(GenerateProp());
        UpdateScore(-score);

        GamePanel.SetActive(true);
        MenuPanel.SetActive(false);
        DefeatPanel.SetActive(false);
    }

    public void OnReturnMenu()
    {
        GamePanel.SetActive(false);
        MenuPanel.SetActive(true);
        DefeatPanel.SetActive(false);
        StopCoroutine(mainCoroutine);
        Physics.gravity = originalGravity;
        GameController.Singleton.msgList.Add("ReturnMenu");
    }

    IEnumerator GenerateProp()
    {
        while (true)
        {
            var prop = ObjectPoolMgr.Singleton.Utilize(propName[Random.Range(0, propName.Count)]);
            prop.transform.position = new Vector3(Random.Range(-xRange, xRange), yInitPos, 0);
            var rb = prop.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * Random.Range(level.minForce, level.maxForce), ForceMode.Impulse);
            float torqueX, torqueY, torqueZ;
            torqueX = Random.Range(0f,torqueRange);
            torqueY = Random.Range(0f,torqueRange);
            torqueZ = Random.Range(0f,torqueRange);
            rb.AddTorque(torqueX,torqueY,torqueZ,ForceMode.Impulse);
            yield return new WaitForSeconds(level.interval);
        }
    }

    public void Defeat()
    {
        StopCoroutine(mainCoroutine);
        GamePanel.SetActive(false);
        MenuPanel.SetActive(false);
        DefeatPanel.SetActive(true);
        DefeatPanel.transform.Find("Score").GetComponent<Text>().text = "Score:" + score;
    }

    public void UpdateScore(int addScore)
    {
        score += addScore;
        scoreText.text = "Score:" + score;
    }

}

[System.Serializable]
public struct LevelData
{
    public float interval;
    public float minForce;
    public float maxForce;
    public float gravityScale;
}
