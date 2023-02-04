using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlatform : MonoBehaviour
{
    [SerializeField] private float timeInState = 1f;
    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        StartCoroutine(FadeOut());
    }

    void Update()
    {
        
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(timeInState);
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(timeInState);
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        StartCoroutine(FadeIn());
    }
}
