using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessController : MonoBehaviour
{
    public GameObject alive;
    public GameObject dead;

    private void Start()
    {
        if (GameManager.isGhost)
        {
            dead.SetActive(true);
            alive.SetActive(false);
        }
        else
        {
            dead.SetActive(false);
            alive.SetActive(true);
        }
    }
}
