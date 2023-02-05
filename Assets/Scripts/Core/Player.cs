using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References"), SerializeField] private WindManager windManager;
    [SerializeField] private CanvasManager canvasManager;
    [SerializeField] private Animator animator;

    [Space, Header("Player Variables"), SerializeField] private int MaxHealth = 5;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float hitStun = 1f;

    private CharacterController controller;
    private AudioSource audioSource;

    private bool canMove = true;
    private int health = 1;
    public int Health { get { return health; } }
    private float verticalSpeed;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        health = MaxHealth;
        canMove = true;
    }

    void Update()
    {
        // Jump
        if (controller.isGrounded)
        {
            animator.SetBool("isGrounded", true);
            if (!canMove) { return; }

            if (Input.GetButtonDown("Jump"))
            {
                verticalSpeed = jumpForce;
                animator.SetBool("isJumping", true);
            }
            else
            {
                verticalSpeed = -gravity * Time.deltaTime; // Can be any negative value
            }
        }
        else
        {
            verticalSpeed -= gravity * 1.3f * Time.deltaTime;  // Apply gravity
            animator.SetBool("isJumping", false);
            animator.SetBool("isGrounded", false);
        }

        // Wind Effect
        Vector3 windMovementVector = new Vector3(windManager.windIsMovingRight ? windManager.windStrength : -windManager.windStrength, 0, 0);
        controller.Move(windMovementVector * Time.deltaTime);

        if (!canMove) { controller.Move(new Vector3(0, verticalSpeed * Time.deltaTime, 0)); return; }

        // Movement
        Vector3 movementVector = new Vector3(Input.GetAxisRaw("Horizontal") * speed, verticalSpeed, 0);
        controller.Move(movementVector * Time.deltaTime);

        // Rotation & Animation
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            animator.SetBool("isRunning", true);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hazard" && animator.GetBool("isHurt") == false)
        {
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            animator.SetBool("isHurt", true);

            health -= 1;
            canvasManager.TakeDamage(health);
            StartCoroutine(CanMove(collision));
        }

        if (health <= 0)
        {
            // Death
            animator.SetBool("isDead", true);
            canvasManager.GameOver();
        }
    }

    private IEnumerator CanMove(Collision collision)
    {
        yield return new WaitForSeconds(hitStun);
        collision.gameObject.GetComponent<BoxCollider>().enabled = true;
        canMove = true;
        animator.SetBool("isHurt", false);
    }
}
