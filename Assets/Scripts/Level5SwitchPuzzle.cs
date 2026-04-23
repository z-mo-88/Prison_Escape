using UnityEngine;

public class Level5SwitchPuzzle : MonoBehaviour
{
    public Level5Switch[] allSwitches;
    public Level5Manager levelManager;

    private int correctOnCount = 0;
    private bool isSolved = false;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip resetSound;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void RecountAndCheck()
    {
        correctOnCount = 0;

        foreach (Level5Switch sw in allSwitches)
        {
            if (sw != null && sw.isCorrectSwitch && sw.IsOn())
                correctOnCount++;
        }

        if (!isSolved && correctOnCount >= 3)
        {
            isSolved = true;
            Debug.Log("Level 5 switch puzzle solved!");

            if (levelManager != null)
                levelManager.SwitchPuzzleSolved();

            NavKeypad.Keypad keypad = Object.FindFirstObjectByType<NavKeypad.Keypad>();

            if (keypad != null)
                keypad.UnlockKeypad();
            else
                Debug.LogWarning("Keypad not found in scene!");
        }

        if (correctOnCount < 3)
            isSolved = false;
    }

    public void ResetCorrectSwitchesOnly()
    {
        correctOnCount = 0;
        isSolved = false;

        // RESET SOUND
        if (audioSource != null && resetSound != null)
        {
            audioSource.PlayOneShot(resetSound);
        }

        foreach (Level5Switch sw in allSwitches)
        {
            if (sw != null && sw.isCorrectSwitch)
                sw.ResetSwitch();
        }

        Debug.Log("Wrong switch used. Correct switches turned off.");
    }

    public void ResetAllSwitches()
    {
        correctOnCount = 0;
        isSolved = false;

        foreach (Level5Switch sw in allSwitches)
        {
            if (sw != null)
                sw.ResetSwitch();
        }

        Debug.Log("All switches reset.");
    }
}