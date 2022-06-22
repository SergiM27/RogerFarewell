using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksSpawner : MonoBehaviour
{

    public GameObject[] spawners;
    public GameObject fireworkPrefab;
    public float timeBetweenSpawns;
    public float timer;

    int[] lastFireworks;

    // Start is called before the first frame update
    void Start()
    {
        lastFireworks = new int[5] { -1, -1, -1, -1, -1 };
        timer = timeBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timeBetweenSpawns;
            SpawnFirework();
        }
    }

    void AddPos(int x)
    {
        lastFireworks[0] = lastFireworks[1];
        lastFireworks[1] = lastFireworks[2];
        lastFireworks[2] = lastFireworks[3];
        lastFireworks[3] = lastFireworks[4];
        lastFireworks[4] = x;
    }

    int NewPos()
    {
        int pos;
        pos = Random.Range(0, 25);
        while (lastFireworks[0] == pos || lastFireworks[1] == pos || lastFireworks[2] == pos || lastFireworks[3] == pos || lastFireworks[4] == pos)
        {
            pos = Random.Range(0, 25);
        }
        AddPos(pos);
        return pos;
    }

    public void SpawnFirework()
    {
        if (FireworksManager.fireworksOver == false)
        {
            int rnd = NewPos();

            if (spawners[rnd].transform.parent.transform.childCount == 1)
            {
                GameObject firework = Instantiate(fireworkPrefab, spawners[rnd].transform.position, Quaternion.identity);
                firework.name = "Firework";
            }
            else
            {
                SpawnFirework();
            }
        }
    }
}
