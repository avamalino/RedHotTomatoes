using UnityEngine;
using UnityEngine.SceneManagement;

public class MMManager : MonoBehaviour
{
    public static MMManager _;
    public enum MMButtons { Play, Credits, Quit };
    public enum CreditsBack { Back };

    [SerializeField] private GameObject _MainMenuContainer;
    [SerializeField] private GameObject _CreditsMenuContainer;

    [SerializeField] private string _sceneToLoadAfterClickingPlay;
    [SerializeField] private string _sceneToLoadAfterClickingBack;

    public void Awake()
    {
        if (_ == null)
        {
            _ = this;
        }
        else
        {
            Debug.LogError("There are more than 1 MainMenuManager's in the Scene");
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OpenMenu(_MainMenuContainer); 
    }

    // Update is called once per frame
    public void MMButtonClicked(MMButtons button)
    {
        switch (button)
        {
            case MMButtons.Play:
                PlayClicked();
                break;
            case MMButtons.Credits:
                Debug.Log("clicking credits button");
                CreditsClicked();
                break;
            case MMButtons.Quit:
                QuitGame();
                break;
            default:
                Debug.LogError("Invalid button clicked: " + button.ToString());
                break;
        }
    }
    public void ReturnToMM()
    {
        SceneManager.LoadScene(_sceneToLoadAfterClickingBack);
    }

    public void CreditsButtonClicked(CreditsBack button)
    {
        switch (button)
        {
            case CreditsBack.Back:
                SceneManager.LoadScene(_sceneToLoadAfterClickingBack);
                break;
        }
    }

    public void PlayClicked()
    {
        SceneManager.LoadScene(_sceneToLoadAfterClickingPlay);
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
        #else
               Application.Quit();
        #endif

    }

    public void CreditsClicked()
    {
        OpenMenu(_CreditsMenuContainer);
    }

    public void OpenMenu(GameObject menu)
    {
        Debug.Log("Opening menu: " + menu.name);
        _MainMenuContainer.SetActive(menu == _MainMenuContainer);
        _CreditsMenuContainer.SetActive(menu == _CreditsMenuContainer);
    }
}
