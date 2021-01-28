using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController03 : MonoBehaviour
{
    public ParticleSystem runParticle;
    public ParticleSystem deathParticle;

    public AudioClip jumpAudio;
    public AudioClip deathAudio;

    public string obsTagName;

    public float jumpScale;
    public float gravityScale;

    bool isGameOver = false;
    bool isOnGround = true;

    Animator anim;
    Rigidbody rb;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityScale;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

        anim.SetFloat("Speed_f", 1);

        GameController.Singleton.AddMsgListener("GameOver", OnGameOver);
        GameController.Singleton.AddMsgListener("Restart", OnRestart);
        GameController.Singleton.AddMsgListener("Jump", OnJump);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameController.Singleton.FireMsgListener("Jump");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(obsTagName))
        {
            GameController.Singleton.FireMsgListener("GameOver");
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    void OnJump()
    {
        if (!isOnGround || isGameOver)
            return;

        rb.AddForce(Vector3.up * jumpScale, ForceMode.Impulse);
        isOnGround = false;

        anim.SetTrigger("Jump_trig");

        audio.clip = jumpAudio;
        audio.Play();
    }

    void OnGameOver()
    {
        isGameOver = true;
        anim.SetBool("Death_b", true);

        runParticle.Pause();
        runParticle.gameObject.SetActive(false);
        deathParticle.gameObject.SetActive(true);
        deathParticle.Play();

        audio.clip = deathAudio;
        audio.Play();
    }

    void OnRestart()
    {
        isGameOver = false;
        runParticle.gameObject.SetActive(true);
        runParticle.Play();
        deathParticle.gameObject.SetActive(false);
    }
}
