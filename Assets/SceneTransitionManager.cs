using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneTransitionManager : MonoBehaviour
{
    // Method to load the onboarding scene
    public void LoadOnboardingScene()
    {
        SceneManager.LoadScene("OnboardingScene"); // Replace with your actual scene name
    }

    // Method to load the home screen
    public void LoadHomeScene()
    {
        SceneManager.LoadScene("HomeScene"); // Replace with your actual scene name
    }

    // Method to load the result screen
    public void LoadResultScene()
    {
        SceneManager.LoadScene("ResultScene"); // Replace with your actual scene name
    }

    // Method to load the AR mode scene
    public void LoadARScene()
    {
        SceneManager.LoadScene("ARNavigationScene"); // Replace with your actual scene name
    }

    // Example method to handle input for transitioning scenes
    public void OnTransitionInput(InputAction.CallbackContext context)
    {
        if (context.performed) // Check if the action was performed
        {
            LoadHomeScene(); // Call the desired scene loading method here
        }
    }
}
