using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;  // The AudioSource that will play the song
    public AudioClip[] songClips;   // Array of available songs
    public float bpm;               // BPM of the currently playing song
    public AudioClip currentSong;
    public bool isPlaying = false; // Track if a song is playing

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>(); // Get the AudioSource if not assigned
        }
    }

    private void Update()
    {
        if (GetSongTime() == currentSong.length)
        {
            StopSong();
        }
    }

    // Play a song from the list
    public void PlaySong(int songIndex)
    {
        if (songIndex >= 0 && songIndex < songClips.Length)
        {
            audioSource.clip = songClips[songIndex];
            audioSource.Play();
            isPlaying = true;
            currentSong = audioSource.clip;

            // Set BPM
            bpm = GetBPMForSong(songIndex);

            Debug.Log($"Now playing: {songClips[songIndex].name}, BPM: {bpm}");
        }
        else
        {
            Debug.LogWarning("Invalid song index!");
        }
    }

    // Stop the currently playing song
    public void StopSong()
    {
        audioSource.Stop();
        isPlaying = false;
    }

    // Pause the song
    public void PauseSong()
    {
        audioSource.Pause();
        isPlaying = false;
    }

    // Resume the song
    public void ResumeSong()
    {
        audioSource.Play();
        isPlaying = true;
    }

    // Get the current time in the song (for syncing with notes)
    public float GetSongTime()
    {
        if (isPlaying)
        {
            return audioSource.time; // Returns time in seconds
        }

        return 0f;
    }

    // Dummy BPM data, you could use a more sophisticated method of getting this
   public float GetBPMForSong(int songIndex)
    {
        // For now, hardcode BPM values per song (this would usually come from your music data)
        switch (songIndex)
        {
            case 0: return 85f;  // Example: Song 1 at 120 BPM
            case 1: return 130f;  // Example: Song 2 at 130 BPM
            default: return 120f; // Default BPM
        }
    }
}
