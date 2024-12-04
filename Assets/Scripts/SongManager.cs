using UnityEngine;

public class SongManager : MonoBehaviour
{
    public float bpm = 85f; // BPM of the song
    private float secondsPerBeat;

    void Start()
    {
        // Calculate time per beat (seconds per beat)
        secondsPerBeat = 60f / bpm;
    }

    public float GetSecondsPerBeat()
    {
        return secondsPerBeat;
    }
}
