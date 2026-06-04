using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    private string currentKey;
    Vector3 playerStartPos;
    private bool canPress = true;
    public bool isPressed = false;
    void Start()
    {
        GetComponent<Transform>().position = playerStartPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canPress)
        {
            currentKey = "Space";
            isPressed = true;
            StartCoroutine(KeysPressed(currentKey));
        }
        //if (Input.GetKeyDown(KeyCode.D) && canPress)
        //{
        //    currentKey = "D";
        //    isPressed = true;
        //    StartCoroutine(KeysPressed(currentKey));
        //}
        //if (Input.GetKeyDown(KeyCode.S) && canPress)
        //{
        //    currentKey = "S";
        //    isPressed = true;
        //    StartCoroutine(KeysPressed(currentKey));
        //}
    }

    void movePlayer(string key)
    {
        if (key == "Space")
        {
            int X = Random.Range(0, 2) == 0 ? -1 : 1;
            player.transform.position = new Vector3(X, player.transform.position.y, player.transform.position.z);
        }
        //if (key == "D")
        //{
        //    player.transform.position = new Vector3(player.transform.position.x + 1, player.transform.position.y, player.transform.position.z);
        //}
        //if (key == "S")
        //{
        //    player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 3, player.transform.position.z);
        //}
    }
    void resetPosition()
    {
        player.transform.position = playerStartPos;
    }
    
    //make sure can't press A or D multiple times in a row, then resets player position to start
    IEnumerator KeysPressed(string currentKey)
    {
        canPress = false;
        movePlayer(currentKey);
        yield return new WaitForSeconds(0.15f);
        resetPosition();
        canPress = true;
    }
}
