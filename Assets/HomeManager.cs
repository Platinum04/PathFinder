using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    public InputField searchInputField; // Assign this in Inspector
    public Button searchButton; // Assign this in Inspector
    private ScreenManager screenManager;

    void Start()
    {
        screenManager = FindObjectOfType<ScreenManager>();

        // Assign button listener
        if (searchButton != null)
        {
            searchButton.onClick.AddListener(SearchResults);
        }
    }

    void SearchResults()
    {
        string query = searchInputField.text;

        // Implement your search logic here

        screenManager.ShowResultScreen(); // Transition to results page after searching
    }
}
