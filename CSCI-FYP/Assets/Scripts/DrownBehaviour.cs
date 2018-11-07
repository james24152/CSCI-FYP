using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrownBehaviour : MonoBehaviour
{

    public float swimSpeed = 1.0f;                 // Default flying speed.
    public Camera cam;
    public float turningSpeed = 0.01f;
    public float flapVelocity = 0.0f;
    public float drag = 2;
    private int swimBool;                          // Animator variable related to swimming.
    private bool swim = false;                     // Boolean to determine whether or not the player activated fly mode.
    private bool isInWater = false;
    private float waterSurfaceY = -2.0f;
    private Rigidbody rb;
    private Animator anim;
    private FogBehaviour fogScript;
    private Health healthScript;

    // Start is always called after any Awake functions.
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        fogScript = cam.GetComponent<FogBehaviour>();
        healthScript = GetComponent<Health>();
    }

    // Update is used to set features regardless the active behaviour.
    void Update()
    {
        // Toggle fly by input, only if there is no overriding state or temporary transitions.
        if (IsUnderWater())
        {
            rb.useGravity = false;
            rb.drag = drag;
        }

        if (IsUnderWater())
        {
            fogScript.fog = true;
        }
        else
            fogScript.fog = false;
    }


    bool IsUnderWater()
    {
        return cam.transform.position.y < waterSurfaceY;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            isInWater = true;
            healthScript.Drown();
            Debug.Log("we are in water");
        }
    }
}
