using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] private float destructionTime = 1f;
    [SerializeField] private float respawnTime = 2.5f;
    [SerializeField] private GameObject rock;

    private BoxCollider boxCollider;
    private AudioSource audioSource;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
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
        audioSource.Play();
        yield return new WaitForSeconds(destructionTime);
        rock.SetActive(false);
        boxCollider.enabled = false;
        StartCoroutine(RestorePlatform());
    }

    IEnumerator RestorePlatform()
    {
        yield return new WaitForSeconds(respawnTime);
        rock.SetActive(true);
        boxCollider.enabled = true;
    }
}
