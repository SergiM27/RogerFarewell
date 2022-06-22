using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    public Animator transition;

    public float transitionTime = 1f;
    public float transitionTimeRewind = 0.5f;

    private void Awake()
    {
        instance = this;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayPress(int level)
    {
        
        if (level == 1)
        {
            if (GameManager.gamesCompleted == 2 && GameManager.secondCinematicDone == false)
            {
                GameManager.secondCinematicDone = true;
                StartCoroutine(LoadLevel_NormalTransition(7));
            }
            else if (GameManager.gamesCompleted == 4)
            {
                StartCoroutine(LoadLevel_NormalTransition(8));
            }
            else
            {
                StartCoroutine(LoadLevel_NormalTransition(level));
            }
        }
        else
        {
            StartCoroutine(LoadLevel_NormalTransition(level));
            AudioManager.instance.PlaySFX("ButtonClick");
        }

    }

    public void PlayPressFromMainMenu(int level)
    {
        GameManager.fireworksDone = false;
        GameManager.paintingDone = false;
        GameManager.DJDone = false;
        GameManager.zooDone = false;
        GameManager.ringsDone = false;
        GameManager.gamesCompleted = 0;
        GameManager.isGhost = false;
        AudioManager.instance.PlaySFX("ButtonClick");
        StartCoroutine(LoadLevel_NormalTransition(level));
    }

    public void BackToMenu()
    {
        //GameManager.resetVariables();
        AudioManager.instance.PlaySFX("ButtonClick");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(LoadLevel_NormalTransition(0));
    }

    IEnumerator LoadLevel_NormalTransition(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

       
    }
}
