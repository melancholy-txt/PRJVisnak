using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightMovement : MonoBehaviour
{
    public Animator flashAnim;
    public Light flashLight;
    public AudioSource flashlightOn, flashlightOff;
    private bool IsOn{
        get{
            return flashLight.enabled;
        }
        set{
            flashLight.enabled = value;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                flashAnim.ResetTrigger("walk");
                flashAnim.SetTrigger("sprint");
            }
            else
            {
                flashAnim.ResetTrigger("sprint");
                flashAnim.SetTrigger("walk");
            }
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            IsOn = !IsOn;
            if (IsOn)
            {
                flashlightOn.Play();
            }
            else
            {
                flashlightOff.Play();
            }
        }
    }
}
