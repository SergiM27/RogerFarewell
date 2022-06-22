using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBucket : MonoBehaviour
{
    public PaintingGame.CellColor color;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            PaintingGame.instance.currentColor = color;
            PaintingGame.instance.paintCharges = 3;
            PaintingGame.instance.PickPaint(color);
            AudioManager.instance.PlaySFX("GetPaint");
            
        }
    }
}
