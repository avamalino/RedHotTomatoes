using UnityEngine;

public class TimingIndicator : MonoBehaviour
{
    public float hitTime;
    public float travelTime = 1.49f;

    // Update is called once per frame
    void Update()
    {
        float minScale = 0.05f;
        float maxScale = 0.3f;

        float percentRemaining = Mathf.Clamp01((hitTime - Time.time) / travelTime);
        float scale = Mathf.Lerp(minScale, maxScale, percentRemaining);
        transform.localScale = Vector3.one * scale;
    }
}
