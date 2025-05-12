using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    public Light campfireLight;
    public float minIntensity = 4f;
    public float maxIntensity = 6f;
    public float flickerSpeed = 2f;

    private float targetIntensity;

    void Start()
    {
        if (campfireLight == null)
            campfireLight = GetComponent<Light>();

        targetIntensity = campfireLight.intensity;
    }

    void Update()
    {
        // Choisir une nouvelle cible de temps en temps
        if (Mathf.Abs(campfireLight.intensity - targetIntensity) < 0.05f)
        {
            targetIntensity = Random.Range(minIntensity, maxIntensity);
        }

        // Lerp vers la cible en douceur
        campfireLight.intensity = Mathf.Lerp(campfireLight.intensity, targetIntensity, flickerSpeed * Time.deltaTime);
    }
}