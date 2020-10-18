using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public ParticleSystem explosionParticles;
    public ParticleSystem dirtParticles;
    public float jumpForce = 10.0f;
    public float gravityModifier;
    private bool isOnGround = true;
    public bool gameOver = false;
    private Animator playerAnim;
    public AudioClip crashSFX;
    public AudioClip jumpSFX;
    private AudioSource playerAudio;
    public float volumeSFX = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && isOnGround && !gameOver)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        playerAnim.SetTrigger("Jump_trig");
        isOnGround = false;
        dirtParticles.Stop();
        playerAudio.PlayOneShot(jumpSFX, volumeSFX);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticles.Play();
        }

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticles.Play();
            dirtParticles.Stop();
            playerAudio.PlayOneShot(crashSFX, volumeSFX);
        }
    }
}
