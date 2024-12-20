using UnityEngine;

public class FloatyObject : MonoBehaviour
{
    public float amplitudeX = 0.1f; // How far the object floats on the X-axis (in units)
    public float amplitudeY = 0.1f; // How far the object floats on the Y-axis (in units)
    public float frequencyX = 1f; // How fast the object floats on the X-axis (in Hz)
    public float frequencyY = 1f; // How fast the object floats on the Y-axis (in Hz)

    private Vector3 initialLocalPosition;
    private float randomOffsetX;
    private float randomOffsetY;
    private float randomAmplitudeX;
    private float randomAmplitudeY;
    private float randomFrequencyX;
    private float randomFrequencyY;

    void Start()
    {
        // Store the initial local position relative to the parent
        initialLocalPosition = transform.localPosition;

        // Generate random offsets for X and Y frequencies
        randomOffsetX = Random.Range(0f, Mathf.PI * 2);
        randomOffsetY = Random.Range(0f, Mathf.PI * 2);

        // Add random variance to amplitude and frequency to prevent synchronization
        randomAmplitudeX = amplitudeX * Random.Range(0.8f, 1.2f);
        randomAmplitudeY = amplitudeY * Random.Range(0.8f, 1.2f);
        randomFrequencyX = frequencyX * Random.Range(0.8f, 1.2f);
        randomFrequencyY = frequencyY * Random.Range(0.8f, 1.2f);
    }

    void Update()
    {
        // Calculate the new floaty position with randomness
        float floatOffsetX = Mathf.Sin(Time.time * randomFrequencyX + randomOffsetX) * randomAmplitudeX;
        float floatOffsetY = Mathf.Cos(Time.time * randomFrequencyY + randomOffsetY) * randomAmplitudeY;
        transform.localPosition = initialLocalPosition + new Vector3(floatOffsetX, floatOffsetY, 0);
    }
}
