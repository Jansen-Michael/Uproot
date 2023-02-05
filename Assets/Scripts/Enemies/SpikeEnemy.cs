using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEnemy : MonoBehaviour
{
    [SerializeField] private GameObject spikeBall;
    [SerializeField] private Transform landZone;
    [SerializeField] private GameObject exclamationPoint;
    [SerializeField] private float spikeBallSpeed = 10f;
    [SerializeField] private float stayingTime = 1f;
    [SerializeField] private float respawnTime = 2.5f;

    private bool canFall = false;
    private Vector3 spikeBallStartPos;

    void Start()
    {
        spikeBallStartPos = spikeBall.transform.position;
    }

    void Update()
    {
        float distanceToLandZone = Vector3.Distance(spikeBall.transform.position, landZone.position);

        if (distanceToLandZone > 0.05f && canFall) 
        {
            spikeBall.transform.Translate(new Vector3(0f, -spikeBallSpeed * Time.deltaTime, 0f));
        }
        else
        {
            StartCoroutine(HitLandingZone());
        }
    }

    IEnumerator HitLandingZone()
    {
        exclamationPoint.SetActive(false);
        canFall = false;
        yield return new WaitForSeconds(stayingTime);

        spikeBall.transform.position = spikeBallStartPos;
        spikeBall.SetActive(false);
        yield return new WaitForSeconds(respawnTime);

        spikeBall.SetActive(true);
        canFall = true;
        exclamationPoint.SetActive(true);
    }
}
