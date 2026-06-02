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

    public GameObject timingCirclePrefab;

    private float timer;
    private const float tomatoTravelTime = 1.49f; //time it takes for tomato to reach player from each side;

    // good bad perfect hits
    private const float good = 0.2f;
    private const float perfect = 0.05f;
    private const float bad = 0.4f;

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
        if (playerManager.isPressed)
        {
            CheckHit();
            playerManager.isPressed = false;
        }
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
            //Create tomato
            GameObject newTomato = Instantiate(tomatoPrefab, new Vector3(randomX, -5, 0), Quaternion.identity);
            Tomato tomatoScript = newTomato.GetComponent<Tomato>();
            tomatoScript.hitTime = noteHitTime;
            tomatoScript.target = destPosition;
            tomatoList.Add(newTomato);

            //create timing circle
            GameObject timingCircle = Instantiate(timingCirclePrefab, destPosition, Quaternion.identity);
            TimingIndicator indicator = timingCircle.GetComponent<TimingIndicator>();
            indicator.hitTime = noteHitTime;
            tomatoScript.timingCircle = indicator;

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
        tomatoList.RemoveAll(t => t == null);


        Tomato closestTomato = null;
        float closestDifference = Mathf.Infinity;
        foreach (GameObject tomatoObject in tomatoList)
        {
            if (tomatoObject == null)
            {
                Debug.LogError("2");

                continue;
            }

            Tomato tomato = tomatoObject.GetComponent<Tomato>();
            Debug.Log("creating new tomato");
            Debug.Log("tomato was dodged: " + tomato.wasDodged);

            if (tomato == null)
            {
                Debug.LogError("1");
                return;
            }
            float difference = Mathf.Abs(buttonPressTime - tomato.hitTime);
            Debug.Log("Difference: " + difference);

            if (difference < closestDifference)
            {
                closestDifference = difference;
                Debug.Log("Closest Difference: " + closestDifference);
                closestTomato = tomato;
            }
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
            return;
        }
        if (closestTomato != null)
        {
            closestTomato.wasDodged = true;
            tomatoList.Remove(closestTomato.gameObject);
            Destroy(closestTomato.gameObject);
        }
    }
}
