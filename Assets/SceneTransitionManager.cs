using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    // Method to load the AR Scene
    public void LoadARScene()
    {
        SceneManager.LoadScene("ARNavigationScene");  // Ensure this matches your AR scene name
    }
}
