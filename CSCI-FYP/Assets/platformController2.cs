using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformController2 : MonoBehaviour {

    // Use this for initialization
    public float speed;
    private Vector3 location1;
    private Vector3 location2;
    public float totalLength;
    public float startTime;
    public int triggered;
    private bool movingLock;

    // Use this for initialization
    void Start()
    {
        location1 = new Vector3(138.0f, 4.4f, 204.12f);
        location2 = new Vector3(138.0f, 4.4f, 222.12f);
        totalLength = Vector3.Distance(location1, location2);
        triggered = 0;
        //speed = 1.0f;
        movingLock = false;
    }

    // Update is called once per frame
    void Update()
    {
        print(Vector3.Distance(transform.position, location1));
        if (triggered == 1)
        {
            Lerp();
        }
        if (triggered == -1)
        {
            LerpBack();
        }

        if (Vector3.Distance(transform.position, location1) < 0.05f && movingLock == false)
        {
            triggered = 1;
            startTime = Time.time;
            movingLock = true;
        }
        if (Vector3.Distance(transform.position, location2) < 0.05f && movingLock == true)
        {
            triggered = -1;
            startTime = Time.time;
            movingLock = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            other.gameObject.transform.parent.gameObject.transform.parent = transform;
        }


    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
            other.gameObject.transform.parent.gameObject.transform.parent = null;
    }

    public void Lerp()
    {

        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / totalLength;
        transform.position = Vector3.Lerp(location1, location2, fracJourney);
        print("this lerp move:");
        print(fracJourney);
    }

    public void LerpBack()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / totalLength;
        transform.position = Vector3.Lerp(location2, location1, fracJourney);
    }
}
