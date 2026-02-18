
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        //Singleton setUp
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //End of Singleton
    }
    public void OnGoTitle()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            SceneManager.LoadScene(sceneName: "Gameplay");
        }
        else
        {
            SceneManager.LoadScene(sceneName: "Title");
        }
    }
    public void OnExit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
