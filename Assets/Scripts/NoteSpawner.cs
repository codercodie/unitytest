using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;  // The prefab for the notes
    public Transform[] paths;      // The paths where the notes will follow
    public float bpm;              // The beats per minute of the song
    private float secondsPerBeat;

    private float timeElapsed = 0f;
    private float spawnTime = 0f;

    public AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        bpm = audioManager.GetBPMForSong(0);
        secondsPerBeat = 60f / bpm;  // Calculate time per beat
        spawnTime = secondsPerBeat;  // The first note will spawn at the first beat
        audioManager.PlaySong(0);
    }

    void Update()
    {
        // Update the time elapsed since the song started
        timeElapsed += Time.deltaTime;

        // Check if it's time to spawn a note
        if (timeElapsed >= spawnTime)
        {
            SpawnNote();
            spawnTime += secondsPerBeat;  // Set the time for the next note
        }
    }

    void SpawnNote()
    {
        // You can customize this by randomly choosing a path or using predefined patterns
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
        float timeForNoteToReach = secondsPerBeat;  // The note will reach the trigger on the next beat
        noteScript.SetSpeed(timeForNoteToReach);
    }
}
