using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDrownScript : MonoBehaviour {

    private Camera cam;
    public float turningSpeed = 0.01f;
    public float flapVelocity = 0.0f;
    public float drag = 2;
    public bool inited;
    public bool isInLava = false;
    private float lavaSurfaceY = 4.2f;
    private Rigidbody rb;
    private Animator anim;
    private FogBehaviour lavaFogScript;
    private Health healthScript;
    private AudioManager audioMangaer;

    // Start is always called after any Awake functions.
    void Start()
    {
        cam = GetComponent<CharacterChangeController>().cam;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        lavaFogScript = cam.GetComponent<FogBehaviour>();
        healthScript = GetComponent<Health>();
        audioMangaer = FindObjectOfType<AudioManager>();
    }

    // Update is used to set features regardless the active behaviour.
    void Update()
    {
        // Toggle fly by input, only if there is no overriding state or temporary transitions.
        if (isInLava)
        {
            rb.useGravity = false;
            rb.drag = drag;
        }

        if (IsUnderLava())
        {
            lavaFogScript.lava = true;
            lavaFogScript.fog = true;
        }
        else {
            lavaFogScript.lava = false;
            lavaFogScript.fog = false;
        }
    }


    bool IsUnderLava()
    {
        return cam.transform.position.y < lavaSurfaceY;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            if (!inited)
            {
                isInLava = true;
                audioMangaer.Play("Splash");
                healthScript.Drown();
                Debug.Log("we are in lava");
                inited = true;
            }
        }
        if (other.gameObject.CompareTag("LavaSoftKill")) {
            if (gameObject.GetComponent<ElementCombineBehaviour>().elementName != "Lava") {
                if (!inited)
                {
                    isInLava = true;
                    audioMangaer.Play("Splash");
                    healthScript.Drown();
                    Debug.Log("we are in lava");
                    inited = true;
                }
            }
        }
    }
}
