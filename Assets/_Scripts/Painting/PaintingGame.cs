using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintingGame : MonoBehaviour
{
    public static PaintingGame instance;

    public enum CellColor {None, Red, Green, Blue};
    [Header("References")]
    public GameObject player;
    public GameObject grid;
    private PaintCell[,] cells;
    public List<PaintCell> cellList;
    public Timer timer;
    private PaintingPatterns patterns;

    [Space]
    [Header("Colors")]
    public List<Material> materials;

    

    public CellColor currentColor;
    public List<Material> colors;
    public TMPro.TMP_Text ui_text;
    public int paintCharges;
    public TMPro.TMP_Text ui_charge;
    public bool ghostPainting;
    public float ghostTime;
    private float ghostTimer;
    bool gameisOver;


    public Animator normAnim;
    public Animator ghostAnim;

    public MeshRenderer brush;
    public MeshRenderer brushGhost;

    int cellsDone;

    public List<Sprite> colorsSprite;
    public Image uiColors;

    private void Awake()
    {
        gameisOver = false;
        instance = this;
        timer = GetComponent<Timer>();
        patterns = GetComponent<PaintingPatterns>();
    }

    private void Start()
    {
        
        cells = new PaintCell[5, 5];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                foreach (var cell in cellList)
                {
                    if (cell.gameObject.name.StartsWith(i.ToString()) && cell.gameObject.name.EndsWith(j.ToString()))
                    {
                        cells[i, j] = cell;
                        break;
                    }
                }
                
            }
        }

        currentColor = CellColor.None;
        paintCharges = 0;
        cellsDone = 0;

        brush.material = materials[6];
        brushGhost.material = materials[6];
        timer.StartTimer();

        PopulatePattern();


    }

    private void PopulatePattern()
    {
        
        int[,] pattern = patterns.patterns[UnityEngine.Random.Range(1, patterns.patterns.Count-1)];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                cells[i, j].color = (CellColor)pattern[i,j];
            }
        }
    }

    private void Update()
    {
        uiColors.sprite = colorsSprite[(int)currentColor];
        if (GameManager.isGhost)
        {
            //if(Input.GetKeyDown(KeyCode.E) && paintCharges > 0){
            //    ghostPainting = true;
            //    paintCharges = 0;
            //    ghostTimer = ghostTime;
            //}

            if (ghostPainting)
            {
                ghostTimer -= Time.deltaTime;
                if (ghostTimer < 0)
                {
                    ghostPainting = false;
                    brushGhost.material = materials[6];
                }
                PaintCell cell = player.GetComponent<PlayerController>().collision.GetComponent<PaintCell>();
                if (cell != null)
                {
                    if (cell.color == currentColor && !cell.isPainted)
                    {
                        cell.Paint();
                        cellsDone++;

                        AudioManager.instance.PlaySFX("Paint");
                        if (gameisOver == false && cellsDone == 25)
                        {
                            Win();
                            gameisOver = true;
                        }
                    }
                }
                ui_charge.text = Mathf.Round(ghostTimer).ToString();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) && paintCharges > 0)
            {
                PaintCell cell = player.GetComponent<PlayerController>().collision.GetComponent<PaintCell>();
                if (cell != null)
                {
                    if (cell.color == currentColor && !cell.isPainted)
                    {
                        normAnim.SetTrigger("Pickup");
                        cell.Paint();
                        paintCharges--;
                        AudioManager.instance.PlaySFX("Paint");
                        cellsDone++;
                        if (paintCharges == 0)
                        {
                            brush.material = materials[6];
                        }

                        
                        if (gameisOver == false && cellsDone == 25)
                        {
                            Win();
                            gameisOver = true;
                        }
                    }
                }
            }
            ui_charge.text = paintCharges.ToString();
        }
        
        
    }

    internal void PickPaint(CellColor color)
    {
        
        normAnim.SetTrigger("Pickup");
        ghostAnim.SetTrigger("Pickup");

        if (GameManager.isGhost)
        {
            ghostTimer = ghostTime;
            ghostPainting = true;
        }
        

        switch (color)
        {
            case CellColor.None:
                break;
            case CellColor.Red:
                brush.material = materials[1];
                brushGhost.material = materials[1];
                break;
            case CellColor.Green:
                brush.material = materials[3];
                brushGhost.material = materials[3];
                break;
            case CellColor.Blue:
                brush.material = materials[5];
                brushGhost.material = materials[5];
                break;
            default:
                break;
        }
    }

    private void Win()
    {
        timer.StopTimer();
        float time = timer.timer;
        AudioManager.instance.PlaySFX("GameCompleted");
        GameManager.paintingDone = true;
        GameManager.gamesCompleted++;
        StartCoroutine(Win1());
        
    }
    IEnumerator Win1()
    {
        yield return new WaitForSeconds(1);
        LevelLoader.instance.PlayPress(1);
        yield return null;
    }
}



