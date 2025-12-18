using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class FlashScript : MonoBehaviour
{
    Light targetLight;

    public float minWaitBetweenBursts = 1f;
    public float maxWaitBetweenBursts = 7f;

    public float minBurstDuration = 0.1f;
    public float maxBurstDuration = 0.5f;

    public float blackoutChance = 0.3f;

    public float minBlackoutTime = 0.05f;
    public float maxBlackoutTime = 0.2f;

    public float minIntensity = 0.2f;
    public float maxIntensity = 2.0f;

    public float minFlickerStep = 0.02f;
    public float maxFlickerStep = 0.08f;

    public float baseIntensity = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetLight = GetComponent<Light>();

        StartCoroutine(FlickerLoop());
    }

    // Update is called once per frame
    void Update()
    {

    }


    private IEnumerator FlickerLoop()
    {
        while (true)
        {
            // Wait random time before starting a flicker burst
            float wait = Random.Range(minWaitBetweenBursts, maxWaitBetweenBursts);
            yield return new WaitForSeconds(wait);

            // Duration of this burst
            float burstDuration = Random.Range(minBurstDuration, maxBurstDuration);
            float endTime = Time.time + burstDuration;

            // Maybe do a quick total blackout at some point in the burst
            bool willBlackout = Random.value < blackoutChance;
            bool blackoutDone = false;

            while (Time.time < endTime)
            {
                // optional blackout once per burst
                if (willBlackout && !blackoutDone && Random.value < 0.2f)
                {
                    float blackoutTime = Random.Range(minBlackoutTime, maxBlackoutTime);
                    targetLight.intensity = 0f;
                    yield return new WaitForSeconds(blackoutTime);
                    blackoutDone = true;
                }

                // Normal random flicker
                float newIntensity = Random.Range(minIntensity, maxIntensity);
                targetLight.intensity = newIntensity;

                float step = Random.Range(minFlickerStep, maxFlickerStep);
                yield return new WaitForSeconds(step);
            }

            // Return to base intensity between bursts
            targetLight.intensity = baseIntensity;
        }
    }
}
