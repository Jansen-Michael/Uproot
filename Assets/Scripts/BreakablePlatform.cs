using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] private float destructionTime = 1f;
    [SerializeField] private float respawnTime = 2.5f;

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(DestroyPlatform());
        }
    }

    IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(destructionTime);
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        StartCoroutine(RestorePlatform());
    }

    IEnumerator RestorePlatform()
    {
        yield return new WaitForSeconds(respawnTime);
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
    }
}
