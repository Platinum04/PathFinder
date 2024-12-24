using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    /// <summary>
    /// Loads the AR Navigation Scene.
    /// </summary>
    public void LoadARScene()
    {
        SceneManager.LoadScene("ARNavigationScene"); // Replace with your AR Scene name
    }

    /// <summary>
    /// Loads the Home Scene.
    /// </summary>
    public void LoadHomeScene()
    {
        SceneManager.LoadScene("HomeScene"); // Replace with your Home Scene name
    }
}
