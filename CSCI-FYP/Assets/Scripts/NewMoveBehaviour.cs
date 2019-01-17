using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMoveBehaviour : MonoBehaviour {

    public float moveSpeed = 4;

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero) {
            transform.eulerAngles = Vector3.up * Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
        }

        float speed = moveSpeed * inputDir.magnitude;

        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

        float animSpeed = inputDir.magnitude;
        anim.SetFloat("Speed", animSpeed);
	}
}
