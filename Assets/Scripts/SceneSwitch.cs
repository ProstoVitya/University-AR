using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void LoadScene()
    {
        var sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene((sceneIndex + 1) % 2);
    }
}
