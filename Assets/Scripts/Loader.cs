
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private string currentScene;

    public void LoadScene(string sceneName)
    {
       

        Debug.Log($"Loading scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
        currentScene = sceneName;
    }

    public void TurnOffGame()
    {
       
        Application.Quit();
    }
}

