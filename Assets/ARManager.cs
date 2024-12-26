using UnityEngine;
using UnityEngine.UI;

public class ARManager : MonoBehaviour
{
    public Button backButton; // Assign this button in the Inspector
    private ScreenManager screenManager;

    void Start()
    {
        screenManager = FindObjectOfType<ScreenManager>();

        // Assign button listener
        if (backButton != null)
        {
            backButton.onClick.AddListener(BackToHomePage);
        }
    }

    void BackToHomePage()
    {
        screenManager.ShowHomeScreen(); // Transition back to home page
    }
}
