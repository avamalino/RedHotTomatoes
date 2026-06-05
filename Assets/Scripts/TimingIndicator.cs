using UnityEngine;

public class TimingIndicator : MonoBehaviour
{
    public float hitTime;
    public float travelTime = 1.49f;

    public AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        float songTime = audioSource.time;

        float minScale = 0.05f;
        float maxScale = 0.3f;

        float percentRemaining = Mathf.Clamp01((hitTime - songTime) / travelTime);
        float scale = Mathf.Lerp(minScale, maxScale, percentRemaining);
        transform.localScale = Vector3.one * scale;
    }
}
