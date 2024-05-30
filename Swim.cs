using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour
{
    private Animator anim;
    private int swim,grounded;
    void Start()
    {
        anim = GetComponent<Animator>();
        swim = Animator.StringToHash("swim");
        grounded = Animator.StringToHash("Grounded");
        anim.SetBool(swim, false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
           
            if (anim != null)
            {
                // play Bounce but start at a quarter of the way though
                anim.SetBool(swim, true);
                anim.SetBool(grounded, false);
            }
            FindObjectOfType<AudioManager>().Play("Inside Water");
            FindObjectOfType<AudioManager>().Play("Splash");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            anim.SetBool(swim, false);
            anim.SetBool(grounded, true);
            FindObjectOfType<AudioManager>().Stop("Inside Water");
        }
    }
}
