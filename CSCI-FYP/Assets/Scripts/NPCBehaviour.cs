using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCBehaviour : MonoBehaviour {

    private Animator anim;
    private Animator shopAnim;

    public Animator dialogueCanvas;
    public Text text;
    public Image image;

	// Use this for initialization
	void Start () {
        anim = gameObject.transform.parent.GetComponent<Animator>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.transform.name == "OuterCollider")
        {
            anim.SetBool("OuterCollider", true);
        }
        else if (gameObject.transform.name == "InnerCollider")
        {
            image.gameObject.SetActive(true);
            text.text = "What do you want buy?";
            shopAnim = ShopScript.shopScript.gameObject.GetComponent<Animator>();
            dialogueCanvas.SetBool("toggled", true);
            Invoke("showShop", 2f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (gameObject.transform.name == "OuterCollider")
        {
            anim.SetBool("OuterCollider", false);
            dialogueCanvas.SetBool("toggled", false);
        }
        else if (gameObject.transform.name == "InnerCollider")
        {
            shopAnim = ShopScript.shopScript.gameObject.GetComponent<Animator>();
            text.text = "See you next time!";
            shopAnim.SetBool("Open", false);
            anim.SetBool("InnerCollider", false);
        }
    }

    private void showShop() {
        shopAnim.SetBool("Open", true);
        anim.SetBool("InnerCollider", true);
    }
}
