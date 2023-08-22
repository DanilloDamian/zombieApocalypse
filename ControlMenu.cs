using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{
   public GameObject ExitButton;

    void Start()
    {
        #if UNITY_STANDALONE || UNITY_EDITOR
        ExitButton.SetActive(true);
        #endif
    }

    public void PlayGame ()
    {
        StartCoroutine(ChangeScene("Scene1"));
    }

    public void LeaveGame()
    {
        StartCoroutine(Leave());
    }

    IEnumerator ChangeScene(string sceneName)
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator Leave()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}
