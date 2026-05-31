using UnityEngine;

public class Tomato : MonoBehaviour
{
    private float speed = 4f;
    public Vector3 target;
    public GameObject tomatoPrefab;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target)
        {
            Destroy(gameObject);
        }
    }

}
