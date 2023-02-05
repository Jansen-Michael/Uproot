using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlatform : MonoBehaviour
{
    [SerializeField] private float timeInState = 1f;
    [SerializeField] private bool tempo1 = false;
    [SerializeField] private GameObject cloud;
    private BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        if (tempo1)
        {
            StartCoroutine(FadeOut());
        }
        else
        {
            StartCoroutine(FadeIn());
        }
    }

    void Update()
    {
        
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(timeInState);
        cloud.SetActive(true);
        boxCollider.enabled = true;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(timeInState);
        cloud.SetActive(false);
        boxCollider.enabled = false;
        StartCoroutine(FadeIn());
    }
}
