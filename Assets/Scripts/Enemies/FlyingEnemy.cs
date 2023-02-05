using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 500f;
    [SerializeField] private Vector2 xSpawnPositions = new Vector2(-15f, 15f);
    [SerializeField] private GameObject birdMesh;

    private Rigidbody rb;
    private BoxCollider boxCollider;

    private bool isMovingRight;
    private bool isForeground;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();

        Vector3 startPos = transform.position;
        isForeground = false;

        float rand = Random.Range(-5f, 5f);
        if (rand >= 0f)
        {
            transform.position = new Vector3(xSpawnPositions.x + rand, startPos.y, startPos.z);
            transform.rotation = Quaternion.Euler(0, 180f, 0f);
            isMovingRight = true;
        }
        else
        {
            transform.position = new Vector3(xSpawnPositions.y + rand, startPos.y, startPos.z);
            transform.rotation = Quaternion.Euler(0, 0f, 0f);
            isMovingRight = false;
        }
    }

    private void Update()
    {
        if (transform.position.x < xSpawnPositions.x || transform.position.x > xSpawnPositions.y)
        {
            boxCollider.enabled = true;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(isMovingRight ? speed * Time.deltaTime : -speed * Time.deltaTime, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Boarder")
        {
            transform.rotation = Quaternion.Euler(0, isMovingRight ? 180f : 0f, 0f);

            if (isForeground)
            {
                transform.Translate(new Vector3(3.5f, 0f, isMovingRight ? -7.5f : 7.5f));
            }
            else
            {
                transform.Translate(new Vector3(3.5f, 0f, isMovingRight ? 7.5f : -7.5f));
            }

            isForeground = !isForeground;
            isMovingRight = !isMovingRight;

            birdMesh.transform.localScale = new Vector3(isForeground ? 15f : 5f, isForeground ? 15f : 5f, isForeground ? 15f : 5f);
        }
    }
}
