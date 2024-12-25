using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{
    public float displayTime = 4f;  // Time to display the splash screen
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

        // Transition to the onboarding scene after the display time
        Invoke(nameof(LoadOnboardingScene), displayTime);
    }

    void LoadOnboardingScene()
    {
        sceneTransitionManager.LoadOnboardingScene();
    }
}
