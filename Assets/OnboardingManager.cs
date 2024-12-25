using UnityEngine;
using UnityEngine.UI;

public class OnboardingManager : MonoBehaviour
{
    public GameObject[] onboardingScreens;  // Assign all panels/screens
    private int currentScreenIndex = 0;

    public Button[] nextButtons;  // Assign the "Next" buttons (size should be set to 3 in Inspector)
    public Button[] actionButtons; // Assign the action buttons (size should be set to 3 in Inspector)
    private SceneTransitionManager sceneTransitionManager;

    void Start()
    {
        // Find the SceneTransitionManager in the scene
        sceneTransitionManager = FindObjectOfType<SceneTransitionManager>();

        if (sceneTransitionManager == null)
        {
            Debug.LogError("SceneTransitionManager not found in the scene!");
            return;
        }

        ShowScreen(0);  // Start with the first screen

        // Add listeners for all Next buttons
        foreach (Button nextButton in nextButtons)
        {
            nextButton.onClick.AddListener(NextScreen);
        }

        // Add listeners for all Action buttons
        foreach (Button actionButton in actionButtons)
        {
            actionButton.onClick.AddListener(ActionButtonClicked);
        }
    }

    void ShowScreen(int index)
    {
        // Enable the current screen and disable others
        for (int i = 0; i < onboardingScreens.Length; i++)
        {
            onboardingScreens[i].SetActive(i == index);
        }
    }

    void NextScreen()
    {
        currentScreenIndex++;

        if (currentScreenIndex < onboardingScreens.Length)
        {
            ShowScreen(currentScreenIndex);
        }
        else
        {
            // Load the Home Scene when onboarding is complete
            sceneTransitionManager.LoadHomeScene();
        }
    }

    void ActionButtonClicked()
    {
        currentScreenIndex++;

        if (currentScreenIndex < onboardingScreens.Length)
        {
            ShowScreen(currentScreenIndex);
        }
        else
        {
            // Load the Home Scene when onboarding is complete
            sceneTransitionManager.LoadHomeScene();
        }
    }
}
