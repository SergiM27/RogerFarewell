using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ParachuteGame.instance.AddPoint();
            anim.Play();
            AudioManager.instance.PlaySFX("GetRing");
        }
    }

    
}
