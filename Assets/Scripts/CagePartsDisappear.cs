using UnityEngine;

public class CagePartsDisappear : MonoBehaviour
{
    public string playerTag = "Player";
    public GameObject[] partsToHide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            SetParts(false); // hide
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            SetParts(true); // show again
        }
    }

    private void SetParts(bool show)
    {
        foreach (GameObject part in partsToHide)
        {
            if (part != null)
            {
                part.SetActive(show);
            }
        }
    }
}