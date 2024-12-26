using UnityEngine;
using UnityEngine.UI;

public class OnboardingManager : MonoBehaviour
{
    public GameObject[] onboardingSteps; // Assign all three onboarding steps here
    private int currentStepIndex = 0;

    private ScreenManager screenManager;

    void Start()
    {
        screenManager = FindObjectOfType<ScreenManager>();
        ShowCurrentStep();

        // Optionally add listeners to buttons if they are not set in the Inspector
        // Example: 
        // Button nextButton = onboardingSteps[currentStepIndex].GetComponentInChildren<Button>();
        // nextButton.onClick.AddListener(NextStep);
    }

    public void NextStep()
    {
        currentStepIndex++;

        if (currentStepIndex < onboardingSteps.Length)
        {
            ShowCurrentStep();
        }
        else
        {
            screenManager.ShowHomeScreen(); // Go to Home when done
        }
    }

    private void ShowCurrentStep()
    {
        for (int i = 0; i < onboardingSteps.Length; i++)
            onboardingSteps[i].SetActive(i == currentStepIndex); // Only show current step
    }
}
