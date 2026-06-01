using System.Collections.Generic;
using UnityEngine;

public class TomatoController : MonoBehaviour
{
    //player position
    public Vector3 destPosition;
    public GameObject player;
    public PlayerManager playerManager;

    Tomato tomatoScript;
    public GameObject tomatoPrefab;
    private List<GameObject> tomatoList = new List<GameObject>();
    private List<float> noteTimes = new List<float>();

    private float timer;
    private const float tomatoTravelTime = 1.49f; //time it takes for tomato to reach player from each side;

    // good bad perfect hits
    private const float good = 0.8f;
    private const float perfect = 0.4f;
    private const float bad = 1.2f;

    public TextAsset jsonFile;

    private float songTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destPosition = player.transform.position;

        //read from json file to get note times and store them in a list (cause i dont fucking know how to read from json file in unity)
        Notes noteTime = JsonUtility.FromJson<Notes>(jsonFile.text);

        foreach (float note in noteTime.notes)
        {
            noteTimes.Add(note);
        }
    }

    private int currentNoteIndex = 0;
    private float randomX = 0;

    void Update()
    {
        //In update
        //make song time = current time
        //call spawn notes
        //call check hit

        songTime = Time.time;

        spawnNotes();
        CheckHit();

        //timer += Time.deltaTime;
        //float nextNoteTime = noteTimes[currentNoteIndex];
        ////create tomato every three seconds
        //if (timer >= nextNoteTime)
        //{
        //    randomX = Random.Range(0, 2) == 0 ? -4f : 4f;
        //    Debug.Log("this note time: " + nextNoteTime);
        //    GameObject newTomato = Instantiate(tomatoPrefab, new Vector3(randomX, -5, 0), Quaternion.identity);

        //    //attach the tomato script (which moves tomato towards player) to the new tomato
        //    tomatoScript = newTomato.GetComponent<Tomato>();
        //    tomatoScript.target = destPosition;
        //    tomatoList.Add(newTomato);

        //   currentNoteIndex++;
        //    timer = 0f;
        //}

    }

    void spawnNotes()
    {
        //if current note index is equal to the noteTimes.Count you are all done with song so return;
        //record the current hit time of the tomato you are currently on
        //if the current time of the song is >= notehittime - tomatotraveltime
        // initialize a new tomato
        // get the tomato component
        // store the tomato hit time
        // add tomato to list
        // increment current note index

        if (currentNoteIndex >= noteTimes.Count)
        {
            return;
        }

        float noteHitTime = noteTimes[currentNoteIndex];

        randomX = Random.Range(0, 2) == 0 ? -4f : 4f;
        if (songTime >= noteHitTime - tomatoTravelTime)
        {
            GameObject newTomato = Instantiate(tomatoPrefab, new Vector3(randomX, -5, 0), Quaternion.identity);
            Tomato tomatoScript = newTomato.GetComponent<Tomato>();
            tomatoScript.hitTime = noteHitTime;
            tomatoScript.target = destPosition;
            tomatoList.Add(newTomato);
            currentNoteIndex++;
        }
    }

    private float buttonPressTime;
    void CheckHit()
    {
        // if the player never pressed the button then return
        // record press time based on song time
        // create reference for closest tomato
        // create reference for smallest difference and set it to infinity
        // loop through tomatos
        //if there are no more tomatoes then return
        //get tomato component
        // calculate difference
        // check difference against closestDifference
        //check closestdifference against perfect, good, bad thresholds
        // record if closest tomato was dodged
        // remove tomato from list
        // destroy tomato

        if (!playerManager.isPressed)
        {
            return;
        }

        buttonPressTime = songTime;

        Tomato closestTomato = null;
        float closestDifference = Mathf.Infinity;

        foreach (GameObject tomatoObject in tomatoList)
        {
            if (tomatoObject == null)
            {
                return;
            }

            Tomato tomato = tomatoObject.GetComponent<Tomato>();

            float difference = Mathf.Abs(buttonPressTime - tomato.hitTime);
            Debug.Log("Difference: " + difference);

            if (difference < closestDifference)
            {
                closestDifference = difference;
                closestTomato = tomato;
            }

            if (closestDifference <= perfect)
            {
                Debug.Log("Perfect!");
            }
            else if (closestDifference <= good)
            {
                Debug.Log("Good!");
            }
            else if (closestDifference <= bad)
            {
                Debug.Log("Bad!");
            }
            else
            {
                Debug.Log("Miss!");
                closestTomato.wasDodged = true;
            }

            if (closestTomato != null)
            {
                tomatoList.Remove(closestTomato.gameObject);
                Destroy(closestTomato.gameObject);
            }
        }
    }
}
