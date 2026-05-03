using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button[] levelButtons;

    private void Start()
    {

        int levelAt = PlayerPrefs.GetInt("levelAt", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelAt)
            {
                // Locked  not interactable
                levelButtons[i].interactable = false;
            }
            else
            {
                // Unlocked  change normal color
                ColorBlock cb = levelButtons[i].colors;
                cb.normalColor = new Color32(197, 131, 72, 255); // C58348
                levelButtons[i].colors = cb;
            }
        }
    }
}
