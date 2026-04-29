using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] private Options options;

    public void PlayButton()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    public void OpenOptions()
    {
        options.ShowOptions();
    }
}
