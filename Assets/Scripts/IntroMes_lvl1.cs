using UnityEngine;
using TMPro; // Required for TextMeshPro
using System.Collections;

public class IntroMessage : MonoBehaviour
{
    public TextMeshProUGUI messageText; // Reference to your UI Text
    public float displayDuration = 4f;  // How long the text stays on screen

    void Start()
    {
        if (messageText != null)
        {
            // Set your prison escape phrase here
            messageText.text = "The door is secured. Find the right key!";

            // Start the timer to hide it
            StartCoroutine(HideMessage());
        }
    }

    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(displayDuration);

        // Turn off the text object completely
        messageText.gameObject.SetActive(false);
    }
}