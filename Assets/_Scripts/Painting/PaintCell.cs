using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCell : MonoBehaviour
{
    public PaintingGame.CellColor color;

    public Material paintedMaterial;
    public Material notPaintedMaterial;
    

    public bool isPainted;

    private new MeshRenderer renderer;
    private Animation anim;


    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        anim = GetComponent<Animation>();
    }

    void Start()
    {
        switch (color)
        {
            case PaintingGame.CellColor.None:
                break;
            case PaintingGame.CellColor.Red:
                notPaintedMaterial = PaintingGame.instance.materials[0];
                paintedMaterial = PaintingGame.instance.materials[1];
                break;
            case PaintingGame.CellColor.Green:
                notPaintedMaterial = PaintingGame.instance.materials[2];
                paintedMaterial = PaintingGame.instance.materials[3];
                break;
            case PaintingGame.CellColor.Blue:
                notPaintedMaterial = PaintingGame.instance.materials[4];
                paintedMaterial = PaintingGame.instance.materials[5];
                break;
            default:
                break;
        }
        renderer.material = notPaintedMaterial;
        isPainted = false;
    }

    public void Paint()
    {
        if (isPainted)
        {
            //renderer.material = notPaintedMaterial;
        }
        else
        {
            renderer.material = paintedMaterial;
            isPainted = true;
            anim.Play();
        }
       
    }
}
