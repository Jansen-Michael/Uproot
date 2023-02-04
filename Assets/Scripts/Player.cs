using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References"), SerializeField] private WindManager windManager;
    [SerializeField] private CanvasManager canvasManager;

    [Space, Header("Player Variables"), SerializeField] private int MaxHealth = 5;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float hitStun = 1.5f;

    private CharacterController controller;
    private AudioSource audioSource;

    private bool canMove = true;
    private int health = 1;
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
            if (!canMove) { return; }

            if (Input.GetButtonDown("Jump"))
            {
                verticalSpeed = jumpForce;
            }
            else
            {
                verticalSpeed = -gravity * Time.deltaTime; // Can be any negative value
            }
        }
        else
        {
            verticalSpeed -= gravity * 1.3f * Time.deltaTime;  // Apply gravity
        }

        // Wind Effect
        Vector3 windMovementVector = new Vector3(windManager.windIsMovingRight ? windManager.windStrength : -windManager.windStrength, 0, 0);
        controller.Move(windMovementVector * Time.deltaTime);

        if (!canMove) { controller.Move(new Vector3(0, verticalSpeed * Time.deltaTime, 0)); return; }

        // Movement
        Vector3 movementVector = new Vector3(Input.GetAxisRaw("Horizontal") * speed, verticalSpeed, 0);
        controller.Move(movementVector * Time.deltaTime);

        // Rotation
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            canMove = false;
            float randMovement = Random.Range(-5f, 5f);
            controller.Move(new Vector3(randMovement, jumpForce, 0f));
            health -= 1;
            canvasManager.TakeDamage(health);
            StartCoroutine(CanMove());
        }

        if (health <= 0)
        {
            // Death
            canvasManager.GameOver();
        }
    }

    private IEnumerator CanMove()
    {
        yield return new WaitForSeconds(hitStun);
        canMove = true;
    }
}
