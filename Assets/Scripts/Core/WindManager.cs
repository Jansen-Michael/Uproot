using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WindManager : MonoBehaviour
{
    [SerializeField] private WindEffect[] windEffects;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private float timeBeforeDirectionChanges = 16f;
    [SerializeField] private float timeBetweenActivations = 8f;

    public float windStrength = 1.5f;
    public bool windIsMovingRight;
    private float startingWindStrength;

    private AudioSource audioSource;

    void Start()
    {
        // Has to go first
        startingWindStrength = windStrength; 
        windIsMovingRight = !windIsMovingRight;

        audioSource = GetComponent<AudioSource>();

        StartCoroutine(ActivationCycle());
        StartCoroutine(FlipWindDirection());

        foreach (WindEffect windEffect in windEffects)
        {
            windEffect.isMovingRight = windIsMovingRight;
        }
    }

    IEnumerator FlipWindDirection()
    {
        while (true)
        {
            windIsMovingRight = !windIsMovingRight;

            foreach (WindEffect windEffect in windEffects)
            {
                windEffect.isMovingRight = windIsMovingRight;
            }

            yield return new WaitForSeconds(timeBeforeDirectionChanges);
        }
    }

    IEnumerator ActivationCycle()
    {
        while (true)
        {
            foreach (WindEffect windEffect in windEffects)
            {
                windEffect.isActive = false;
                windEffect.windZone.enabled = false;
                windStrength = 0f;
            }
            yield return new WaitForSeconds(timeBetweenActivations - 2f);

            warningText.gameObject.SetActive(true);
            audioSource.Play();
            yield return new WaitForSeconds(2f);

            foreach (WindEffect windEffect in windEffects)
            {
                windEffect.isActive = true;
                windEffect.windZone.enabled = true;
                windStrength = startingWindStrength;
            }
            warningText.gameObject.SetActive(false);
            audioSource.Stop();
            yield return new WaitForSeconds(timeBetweenActivations);
        }
    }
}
