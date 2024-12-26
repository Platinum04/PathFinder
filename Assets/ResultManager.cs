using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public Button goToARButton; // Assign this button in the Inspector
    private ScreenManager screenManager;

    void Start()
    {
        screenManager = FindObjectOfType<ScreenManager>();

        // Assign button listener
        if (goToARButton != null)
        {
            goToARButton.onClick.AddListener(GoToARScene);
        }
    }

    void GoToARScene()
    {
        screenManager.ShowARScreen(); // Transition to AR screen
    }
}
