using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ActivateFirework : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject currentGrid;
    public int score;
    public int pointsForFirework;
    public TextMeshProUGUI scoreText;
    public Timer timer;

    public Animator animNorm;
    public Animator animGhost;

    void Start()
    {
        Invoke("StartTimer", 2.0f);
    }

    public void StartTimer()
    {
        timer.StartTimer();
    }

    public void StopTimer()
    {
        timer.StopTimer();
    }
    // Update is called once per frame
    void Update()
    {
        if (FireworksManager.fireworksOver == false) 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Activate();
                animNorm.SetTrigger("Interact");
                animGhost.SetTrigger("Interact");
            }
            if (timer.timer <= 0)
            {
                StopTimer();
                timer.timer = 0;
                FireworksManager.fireworksOver = true;
                timer.uiTimer.text = "00:00";
                FindObjectOfType<PlayerController>().GetComponent<PlayerController>().enabled = false;
                StartCoroutine(EndLevel());
            }
        }

    }

    private IEnumerator EndLevel()
    {
        animGhost.SetFloat("speed", 0);
        animNorm.SetFloat("speed", 0);
        GameManager.fireworksDone = true;
        GameManager.gamesCompleted++;
        AudioManager.instance.PlaySFX("GameCompleted");
        //score
        yield return new WaitForSeconds(5);
        LevelLoader.instance.PlayPress(1);
        yield return null;
    }

    public void Activate()
    {
        currentGrid = FindObjectOfType<PlayerController>().GetComponent<PlayerController>().collision;
        if (currentGrid.transform.Find("Firework") == true)
        {
            GameObject firework = currentGrid.transform.Find("Firework").gameObject;
            //firework.GetComponent<Renderer>().material.SetColor("_Color",Color.blue);
            firework.GetComponent<FireworkBehaviour>().CancelInvoke("DestroyFireworkFunc");
            firework.GetComponent<FireworkBehaviour>().Fire();
        }
        else
        {
            return;
        }
    }

    public void FireworkExplosion()
    {
        score += pointsForFirework;
        scoreText.text = score.ToString(); 
    }
}
