using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class tomatoController : MonoBehaviour
{
    //player position
    public Vector3 destPosition;
    public GameObject player;

    Tomato tomatoScript;
    public GameObject tomatoPrefab;
    private List<GameObject> tomatoList = new List<GameObject>();
    private float timer;

    // good bad perfect hits
    private const float good = 0.5f;
    private const float perfect = 0.2f;
    private const float bad = 0.8f;

    public TextAsset jsonFile;
    Notes noteTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destPosition = player.transform.position;
        Notes noteTime = JsonUtility.FromJson<Notes>(jsonFile.text);
        Debug.Log(noteTime);
        foreach (float note in noteTime.notes)
        {
            Debug.Log("note time: " + note);
        }
    }

    private int currentNoteIndex = 0;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float nextNoteTime = noteTime.notes[currentNoteIndex];
        //create tomato every three seconds
        if (timer >= nextNoteTime)
        {
            
            GameObject newTomato = Instantiate(tomatoPrefab, new Vector3(Random.Range(0f,-10f), -4, 0), Quaternion.identity);
            
            //attach the tomato script (which moves tomato towards player) to the new tomato
            tomatoScript = newTomato.GetComponent<Tomato>();
            tomatoScript.target = destPosition;
            tomatoList.Add(newTomato);
            Debug.Log(tomatoList);

            currentNoteIndex++;
            timer = 0f;
        }

    }
    
    void CheckHit()
    {
        float currentTime = Time.time;

    }

}
