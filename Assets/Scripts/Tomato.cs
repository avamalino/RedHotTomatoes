using UnityEngine;

public class Tomato : MonoBehaviour
{
    private float speed = 4f;
    public Vector3 target;
    public float hitTime;
    public bool wasDodged = false;
    public GameObject tomatoPrefab;
    public TimingIndicator timingCircle;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position == target)
        {
            if (!wasDodged)
            {
                Debug.Log("Miss");
            }
            //Debug.Log("This is the time of destroying the object: " + Time.time);
            Destroy(gameObject);
            Destroy(timingCircle.gameObject);
        }
    }
}
