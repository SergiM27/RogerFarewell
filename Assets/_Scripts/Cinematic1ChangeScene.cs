using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic1ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public float time = 18;
    void Start()
    {
        Invoke("Change", time);
    }

    // Update is called once per frame
    void Change()
    {
        LevelLoader.instance.PlayPress(1);
    }
}
