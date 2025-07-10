using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuToRunner : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene("Runner");
    }
}
