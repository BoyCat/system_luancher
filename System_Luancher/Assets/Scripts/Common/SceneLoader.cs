using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Title,
    Lobby,
    Game,    
    Credits
}

public class SceneLoader : SingletonBehaviour<SceneLoader>
{
    public void LoadScene(SceneType sceneType)
    {
        string sceneName = sceneType.ToString();
        Logger.Log($"Loading scene: {sceneName}");

        Time.timeScale = 1f; 
        SceneManager.LoadScene(sceneName);        
    }

    public void ReloadScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Logger.Log($"Reloading scene: {currentSceneName}");

        Time.timeScale = 1f; 
        SceneManager.LoadScene(currentSceneName);
    }
}
