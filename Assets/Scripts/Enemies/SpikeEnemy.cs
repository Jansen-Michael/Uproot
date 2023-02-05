using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEnemy : MonoBehaviour
{
    //[SerializeField] private Transform spawnPosition;
    [SerializeField] private GameObject exclamationPoint;
    [SerializeField] private float stayingTime = 1f;
    [SerializeField] private float respawnTime = 2.5f;

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;
    private Rigidbody rb;
    private Vector3 spikeBallStartPos;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();

        spikeBallStartPos = transform.position;
    }

    void Update()
    {
        
    }

    IEnumerator HitLandingZone()
    {
        exclamationPoint.SetActive(false);
        yield return new WaitForSeconds(stayingTime);

        rb.useGravity = false;
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(respawnTime);

        transform.position = spikeBallStartPos;
        rb.useGravity = true;
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
        exclamationPoint.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            meshRenderer.enabled = false;
            boxCollider.enabled = false;
            StartCoroutine(HitLandingZone());
        }
        else
        {
            StartCoroutine(HitLandingZone());
        }
    }
}
