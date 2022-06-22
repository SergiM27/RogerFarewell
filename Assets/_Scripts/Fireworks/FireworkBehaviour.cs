using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkBehaviour : MonoBehaviour
{
    public bool floorCollision;
    private Vector3 _velocity;
    public float timeToDestroy;

    public Transform firework;
    public ParticleSystem fireworkPS;
    public ParticleSystem mecha;
    public ParticleSystem launch;

    public Animator anim;
    public float fireworkSpeed;
    bool isFireing;

    Coroutine destroy;
    public bool isDisapearing;
    public Animator lightAnim;
    public AudioSource source;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Start()
    {
        fireworkPS.Stop();
        mecha.Stop();
        launch.Stop();
        Invoke("DestroyFireworkFunc", timeToDestroy);
        floorCollision = false;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (floorCollision == false)
        {
            UpdateVelocity();
            UpdatePosition();
        }
    }

    public void UpdateVelocity()
    {
        _velocity += (Physics.gravity * Time.deltaTime) / 1.5f;
    }

    public void UpdatePosition()
    {
        transform.position += _velocity * Time.deltaTime;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (floorCollision == false)
        {
            if (collision.gameObject.tag == "GridFloor")
            {
                floorCollision = true;
                if (GameManager.isGhost)
                {
                    this.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
                this.gameObject.transform.parent = collision.gameObject.transform;
            }
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (floorCollision == false)
        {
            if (collision.gameObject.name == "PlayerController")
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(1000, 0, 0);
            }
        }
    }

    void DestroyFireworkFunc()
    {
        destroy = StartCoroutine(DestroyFirework());
    }
    private IEnumerator DestroyFirework()
    {
        isDisapearing = true;
        anim.SetTrigger("disapear");
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(1.34f);
        Destroy(this.gameObject);
        yield return null;
    }

    internal void Fire()
    {
        if (!isFireing && !isDisapearing)
        {
            isFireing = true;
            StartCoroutine(FireCoroutine());
        }
       
    }

    private IEnumerator FireCoroutine()
    {
        firework.parent = transform;

        //tssssss
        AudioManager.instance.PlaySFX("FireworkFuse", source);
        anim.SetTrigger("fire");
        mecha.Play();
        yield return new WaitForSeconds(1.6f);
        mecha.Stop();
        yield return new WaitForSeconds(0.4f);
        //fiuuuuuuuuuuuu
        AudioManager.instance.PlaySFX("FireworkWhistle", source);
        GetComponent<Collider>().enabled = false;
        mecha.Stop();
        launch.Play();
        fireworkPS.Play();
        firework.GetComponent<Rigidbody>().velocity = Vector3.up * fireworkSpeed;
        yield return new WaitForSeconds(2);
        //booooooooom
        launch.Stop();
        fireworkPS.Stop();
        AudioManager.instance.PlaySFX("FireworkBoom", source);
        lightAnim.gameObject.GetComponent<Light>().color = UnityEngine.Random.ColorHSV(0,1,1,1,1,1,1,1);
        lightAnim.SetTrigger("boom");
        FindObjectOfType<ActivateFirework>().FireworkExplosion();
        yield return new WaitForSeconds(5);
        Destroy(gameObject);


    }
}