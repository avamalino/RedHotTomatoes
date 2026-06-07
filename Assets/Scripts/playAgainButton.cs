using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playAgainButton : MonoBehaviour
{
    [SerializeField] private Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MM");
    }
}
