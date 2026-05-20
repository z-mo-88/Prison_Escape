using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    public Light flickerLight;

    public float minIntensity = 0.5f;
    public float maxIntensity = 2f;

    public float flickerSpeed = 0.1f;

    void Start()
    {
        if (flickerLight == null)
            flickerLight = GetComponent<Light>();

        InvokeRepeating(nameof(Flicker),
            0f,
            flickerSpeed);
    }

    void Flicker()
    {
        if (flickerLight != null)
        {
            flickerLight.intensity =
                Random.Range(minIntensity,
                             maxIntensity);
        }
    }
}