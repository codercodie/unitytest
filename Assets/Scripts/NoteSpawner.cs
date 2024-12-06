using System.Collections;
using TMPro;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;  // The prefab for the notes
    public Transform[] paths;      // The paths where the notes will follow
    public float bpm;              // The beats per minute of the song
    private float secondsPerBeat;

    private float timeElapsed = 0f;
    private float spawnTime = 0f;
    private float introDelay;      // Time delay for 8 beats

    public AudioManager audioManager;

    public TextMeshProUGUI countdown;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        bpm = audioManager.GetBPMForSong(0);
        secondsPerBeat = 60f / bpm;  // Calculate time per beat

        introDelay = secondsPerBeat * 8;  // Delay for intro
        audioManager.PlaySong(0);
        audioManager.PauseSong();
        StartCoroutine(IntroCoroutine());

    }

    void Update()
    {
        if (audioManager.isPlaying)
        {
            // Update the time elapsed since the song started
            timeElapsed += Time.deltaTime;

            // Check if it's time to spawn a note
            if (timeElapsed >= spawnTime)
            {
                SpawnNote();
                spawnTime += secondsPerBeat;  // Schedule the next note spawn
            }
        }
    }

    private IEnumerator IntroCoroutine()
    {
        spawnTime = introDelay + secondsPerBeat;
        yield return new WaitForSeconds(1);
        countdown.text = "3";
        yield return new WaitForSeconds(1);
        countdown.text = "2";
        yield return new WaitForSeconds(1);
        countdown.text = "1";
        yield return new WaitForSeconds(1);
        countdown.text = "";
        audioManager.ResumeSong();

        
    }

    void SpawnNote()
    {
        // Randomly choose a path or use predefined patterns
        Transform spawnPath = paths[Random.Range(0, paths.Length)];

        // Look for a specific "NoteStartPoint" or use path position if no specific point exists
        Transform startPoint = spawnPath.Find("NoteStartPoint") ?? spawnPath;

        // Instantiate the note at the start point position
        GameObject newNote = Instantiate(notePrefab, startPoint.position, Quaternion.identity);

        // Parent the note to the correct path for proper hierarchy behavior
        newNote.transform.SetParent(spawnPath, true);

        // Set up the note's script and pass the path and trigger information
        Note noteScript = newNote.GetComponent<Note>();
        noteScript.SetPath(spawnPath);

        // Set the timing for when the note should reach the trigger
        float timeForNoteToReach = 1f;  // The note will reach the trigger on the next beat
        noteScript.SetSpeed(timeForNoteToReach);
    }
}
