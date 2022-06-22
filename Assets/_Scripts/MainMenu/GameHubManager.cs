using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameHubManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Image painting, parachute, fireworks, dj, roger;
    public GameObject checkParachute, checkFireworks, checkDJ, checkStreet;
    public Image background;
    public Button paintingButton, parachuteButton, fireworksButton, djButton;

    public Sprite paintingGrey, parachuteGrey, fireworksGrey, djGrey, darkBackground, ghostRoger;

    void Start()
    {
        if(AudioManager.instance.WhatAudioMusic()!= "The-Ice-Cream-Man")
        {
            AudioManager.instance.PlayMusic("Music");
        }
        if (GameManager.DJDone)
        {
            dj.sprite = djGrey;
            checkDJ.SetActive(true);
            djButton.enabled = false;
        }
        if (GameManager.paintingDone)
        {
            painting.sprite = paintingGrey;
            checkStreet.SetActive(true);
            paintingButton.enabled = false;
        }
        if (GameManager.ringsDone)
        {
            parachute.sprite = parachuteGrey;
            checkParachute.SetActive(true);
            parachuteButton.enabled = false;
        }
        if (GameManager.fireworksDone)
        {
            fireworks.sprite = fireworksGrey;
            checkFireworks.SetActive(true);
            fireworksButton.enabled = false;
        }
        if (GameManager.gamesCompleted >= 2)
        {
            background.sprite = darkBackground;
            roger.sprite = ghostRoger;
            roger.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1.5f, 1);
            GameManager.isGhost = true;
        }
    }
}
