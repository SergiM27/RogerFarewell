
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteGame : MonoBehaviour
{
    public static ParachuteGame instance;

    public GameObject player;
    public Transform playerRender;
    public GameObject normal;
    public GameObject ghost;
    float movement;
    public float speed;
    public float maxDistance;

    public float levelSpeed;
    public Vector2 distanceBetweenRings;
    public GameObject ringPrefab;
    public GameObject level;
    public float currentLevelPos;
    public float lastRingPos;
    public float ringSpawnPos;
    private float nextRing;


    public int points;
    public TMPro.TMP_Text uiPoints;

    public TMPro.TMP_Text uiDistance;
    public float levelDistance;
    bool gameRunning;
    bool gameisOver;
    bool gameEnd;
    Transform cam;

    [Space]
    private Quaternion targetAngle;
    public float rotationSmooth;
    private Vector3 rotationVelocity;

    float timer;
    Animator anim;

    public List<GameObject> clouds;
    float newCloud;
    float lastCloud;
    private bool soundPlayed;
    bool soundComplete;

    private void Awake()
    {
        instance = this;
        playerRender = player.transform.GetChild(0);
        anim = player.GetComponent<Animator>();
    }

    void Start()
    {
        gameEnd = false;
        soundPlayed = false;
        gameisOver = false;
        gameRunning = false;
        SpawnCloud();
        currentLevelPos = 0;
        points = 0;
        cam = Camera.main.transform;
        PreWarmClouds();
        if (GameManager.isGhost)
        {
            normal.SetActive(false);
            ghost.SetActive(true);
        }
        else
        {
            ghost.SetActive(false);
            normal.SetActive(true);
        }
    }

    private void SpawnRing()
    {
        if (gameRunning)
        {
            GameObject ring = Instantiate(ringPrefab, new Vector3(Random.Range(-maxDistance, maxDistance), -50, 0), Quaternion.Euler(-90, 0, 0));
            ring.transform.parent = level.transform;
            lastRingPos = currentLevelPos;
            nextRing = Random.Range(distanceBetweenRings.x, distanceBetweenRings.y);
            
        }

    }

    void PreWarmClouds()
    {
        for (int i = 0; i < 40; i++)
        {
            float x = Random.Range(-120, 120);
            float z = Random.Range(20, 150);
            float y = Random.Range(-250, 50);
            float size = Mathf.Clamp(Random.Range(3, z / 3), 0, 20);
            GameObject cloud = Instantiate(
                clouds[Random.Range(0, clouds.Count - 1)],
                new Vector3(x, y, -z),
                Quaternion.Euler(Vector3.up * Random.Range(0, 360) + Vector3.right * -90)
            );
            cloud.transform.localScale = Vector3.one * size;
            cloud.transform.parent = level.transform;
        }
        
    }

    void SpawnCloud()
    {
        float x = Random.Range(-120, 120);
        float z = Random.Range(20, 150);
        float size = Mathf.Clamp(Random.Range(3, z / 3), 0, 20);
        GameObject cloud = Instantiate(
            clouds[Random.Range(0, clouds.Count - 1)], 
            new Vector3(x, -250, -z), 
            Quaternion.Euler(Vector3.up * Random.Range(0, 360) + Vector3.right * -90)
        );
        cloud.transform.localScale = Vector3.one * size;
        cloud.transform.parent = level.transform;
        newCloud = Random.Range(1, 9);
        lastCloud = currentLevelPos;
    }

    void Update()
    {
        uiPoints.text = points.ToString();
        uiDistance.text = $"{levelDistance - Mathf.Round(currentLevelPos)} m";
        if (currentLevelPos > 20 && currentLevelPos < 100) 
        {
            if (!gameRunning) SpawnRing();
            gameRunning = true;
        }
        if (gameRunning)
        {
            if (levelDistance < currentLevelPos)
            {
                gameRunning = false;     
            }

            movement = -Input.GetAxis("Horizontal");

            player.transform.position += Vector3.right * movement * speed * Time.deltaTime;     
        }
        if (movement != 0) targetAngle = Quaternion.LookRotation(movement * Vector3.right);
        playerRender.rotation = SmoothDampQuaternion(playerRender.rotation, targetAngle, ref rotationVelocity, rotationSmooth);

        if (GameManager.isGhost)
        {
            if(gameEnd==false)
            currentLevelPos += levelSpeed * Time.deltaTime - Input.GetAxis("Jump") * levelSpeed * 2 * Time.deltaTime;
        }
        else
        {
            currentLevelPos += levelSpeed * Time.deltaTime;
        }

        level.transform.position = Vector3.up * currentLevelPos;
        if (currentLevelPos > lastRingPos + nextRing)
        {
            SpawnRing();
        }
        if (currentLevelPos > lastCloud + newCloud)
        {
            SpawnCloud();
        }

        if (currentLevelPos>60) EndGame();
        
    }

    private void EndGame()
    {
        
        if (!gameRunning)
        {
            if (!soundComplete) AudioManager.instance.PlaySFX("GameCompleted");
            soundComplete = true;

            uiDistance.text = "0 M";
            timer += Time.deltaTime;
            gameEnd = true;
            
            if (player.transform.position.x * player.transform.position.x > 0.5f)
            {
                player.transform.position -= Vector3.right * player.transform.position.x * Time.deltaTime;

                playerRender.eulerAngles = Vector3.zero;

            }

            if(timer > 1 && cam.rotation.eulerAngles.x > 0)
            {
                uiDistance.text = "0 M";
                cam.eulerAngles -= Vector3.right * cam.eulerAngles.x * Time.deltaTime / 2.5f;
            }

            if(timer > 2)
            {
                anim.SetBool("deploy", true);
                if(!soundPlayed)AudioManager.instance.PlaySFX("Parachute");
                soundPlayed = true;
                cam.transform.position -= timer * Time.deltaTime * Vector3.up;
            }
            if (timer>2.1f)
            {
                playerRender.eulerAngles = Vector3.right * -100;
            }
            if (timer > 8) 
            {
                if (gameisOver == false)
                {
                    GameManager.ringsDone = true;
                    GameManager.gamesCompleted++;
                    LevelLoader.instance.PlayPress(1);
                    gameisOver = true;
                }

            }
        }
    }

    private void LateUpdate()
    {
        player.transform.position = Vector3.right * Mathf.Clamp(player.transform.position.x , -maxDistance, maxDistance);
    }


    public void AddPoint()
    {
        points++;
    }

    

    public Quaternion SmoothDampQuaternion(Quaternion current, Quaternion target, ref Vector3 currentVelocity, float smoothTime)
    {
        Vector3 c = current.eulerAngles;
        Vector3 t = target.eulerAngles;
        return Quaternion.Euler(
          Mathf.SmoothDampAngle(c.x, t.x, ref currentVelocity.x, smoothTime),
          Mathf.SmoothDampAngle(c.y, t.y, ref currentVelocity.y, smoothTime),
          Mathf.SmoothDampAngle(c.z, t.z, ref currentVelocity.z, smoothTime)
        );
    }
}
