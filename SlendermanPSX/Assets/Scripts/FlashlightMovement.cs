using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightMovement : MonoBehaviour
{
    public Animator flashAnim;

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
    }
}
