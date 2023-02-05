using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    [SerializeField] private WindEffect[] windEffects;
    [SerializeField] private float timeBeforeDirectionChanges = 16f;
    [SerializeField] private float timeBetweenActivations = 8f;
    public float windStrength = 1.5f;
    public bool windIsMovingRight;

    private float startingWindStrength;

    void Start()
    {
        // Has to go first
        startingWindStrength = windStrength; 
        windIsMovingRight = !windIsMovingRight;

        StartCoroutine(ActivationCycle());
        StartCoroutine(FlipWindDirection());

        foreach (WindEffect windEffect in windEffects)
        {
            windEffect.isMovingRight = windIsMovingRight;
        }
    }

    void Update()
    {

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
            yield return new WaitForSeconds(timeBetweenActivations);

            foreach (WindEffect windEffect in windEffects)
            {
                windEffect.isActive = true;
                windEffect.windZone.enabled = true;
                windStrength = startingWindStrength;
            }
            yield return new WaitForSeconds(timeBetweenActivations);
        }
    }
}
