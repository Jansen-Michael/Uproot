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
        print(rand);
        if (rand >= 0.5f)
        {
            transform.position = new Vector3(xSpawnPositions.x, startPos.y, startPos.z);
            isMovingRight = true;
        }
        else
        {
            transform.position = new Vector3(xSpawnPositions.y, startPos.y, startPos.z);
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
            isForeground = !isForeground;
            isMovingRight = !isMovingRight;

            transform.rotation = Quaternion.Euler(0, isMovingRight ? 0f : 180f, 0f);
            transform.Translate(new Vector3(isMovingRight ? -2.5f : 2.5f, 0f, isForeground ? -5f : 5f));
        }
    }
}
