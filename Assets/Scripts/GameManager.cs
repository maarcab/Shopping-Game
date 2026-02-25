using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Scene Names")]
    public string titleSceneName = "Title";
    public string gameplaySceneName = "Gameplay";

    private void Awake()
    {
        // Singleton setUp
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        // End of Singleton
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleEscapeKey();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            HandleEnterKey();
        }
    }

    private void HandleEscapeKey()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == titleSceneName)
        {
            OnExit();
        }
        else
        {
            OnGoTitle();
        }
    }

    private void HandleEnterKey()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == titleSceneName)
        {
            SceneManager.LoadScene(gameplaySceneName);
        }
        else
        {
            RestartGame();
        }
    }

    public void OnGoTitle()
    {
        SceneManager.LoadScene(titleSceneName);
    }

    public void OnExit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }
}