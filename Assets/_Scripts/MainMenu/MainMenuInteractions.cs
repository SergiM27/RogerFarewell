using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuInteractions : MonoBehaviour
{

    public GameObject howToPlayMenu;
    public GameObject creditsMenu;
    public GameObject mainMenu;

    private void Start()
    {
        AudioManager.instance.PlayMusic("Music");
    }
    public void HowToPlayPressed()
    {
        howToPlayMenu.gameObject.GetComponent<Animator>().SetBool("HowToPlay", true);
        mainMenu.gameObject.GetComponent<Animator>().SetInteger("MainMenu", 2);
        AudioManager.instance.PlaySFX("ButtonClick");
    }

    public void ReturnHowToPlayPressed()
    {
        howToPlayMenu.gameObject.GetComponent<Animator>().SetBool("HowToPlay", false);
        mainMenu.gameObject.GetComponent<Animator>().SetInteger("MainMenu", 4);
        AudioManager.instance.PlaySFX("ButtonClick");
    }

    public void CreditsPressed()
    {
        creditsMenu.gameObject.GetComponent<Animator>().SetBool("Credits", true);
        mainMenu.gameObject.GetComponent<Animator>().SetInteger("MainMenu", 1);
        AudioManager.instance.PlaySFX("ButtonClick");
    }

    public void ReturnCreditsPressed()
    {
        creditsMenu.gameObject.GetComponent<Animator>().SetBool("Credits", false);
        mainMenu.gameObject.GetComponent<Animator>().SetInteger("MainMenu", 3);
        AudioManager.instance.PlaySFX("ButtonClick");
    }

}
