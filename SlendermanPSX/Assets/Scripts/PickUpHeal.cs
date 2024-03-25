using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpHeal : MonoBehaviour
{
    public GameObject intText, slender1, slender2;
    public AudioSource pickupSound;
    public bool canInteract;
    public int healValue = 0;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(true);
            canInteract = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(false);
            canInteract = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(slender1.activeSelf)
                {
                    slender1.GetComponent<SlenderAI>().playerHealth += healValue;
                }
                else if(slender2.activeSelf)
                {
                    slender2.GetComponent<SlenderAIAgro>().playerHealth += healValue;
                }
                pickupSound.Play();               
                intText.SetActive(false);
                gameObject.SetActive(false);
                canInteract = false;
            }
        }
    
    }
}
