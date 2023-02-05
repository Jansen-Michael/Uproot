using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 500f;
    [SerializeField] private Vector2 xSpawnPositions = new Vector2(-15f, 15f);

    private Rigidbody rb;

    private bool isMovingRight;
    private bool isForeground;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Vector3 startPos = transform.position;
        isForeground = false;

        float rand = Random.Range(0f, 1f);
        if (rand >= 0.5f)
        {
            transform.position = new Vector3(xSpawnPositions.x, startPos.y, startPos.z);
            transform.rotation = Quaternion.Euler(0, 0, 0f);
            isMovingRight = true;
        }
        else
        {
            transform.position = new Vector3(xSpawnPositions.y, startPos.y, startPos.z);
            transform.rotation = Quaternion.Euler(0, 180f, 0f);
            isMovingRight = false;
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
                transform.Translate(new Vector3(3.5f, 0f, isMovingRight ? -5f : 5f));
            }
            else
            {
                transform.Translate(new Vector3(3.5f, 0f, isMovingRight ? 5f : -5f));
            }

            isForeground = !isForeground;
            isMovingRight = !isMovingRight;
        }
    }
}
