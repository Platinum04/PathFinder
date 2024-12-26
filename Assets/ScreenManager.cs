using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject splashScreen;
    public GameObject onboardingScreen;
    public GameObject homeScreen;
    public GameObject resultScreen;
    public GameObject arScreen;

    private void Start()
    {
        ShowSplashScreen();
    }

    public void ShowSplashScreen()
    {
        ActivateScreen(splashScreen);
        Invoke(nameof(ShowOnboardingScreen), 3f); // Show splash for 3 seconds
    }

    public void ShowOnboardingScreen()
    {
        ActivateScreen(onboardingScreen);
        // You can add logic here if needed after onboarding
    }

    public void ShowHomeScreen()
    {
        ActivateScreen(homeScreen);
    }

    public void ShowResultScreen()
    {
        ActivateScreen(resultScreen);
    }

    public void ShowARScreen()
    {
        ActivateScreen(arScreen);
    }

    private void ActivateScreen(GameObject screen)
    {
        splashScreen.SetActive(false);
        onboardingScreen.SetActive(false);
        homeScreen.SetActive(false);
        resultScreen.SetActive(false);
        arScreen.SetActive(false);

        screen.SetActive(true);
    }
}
