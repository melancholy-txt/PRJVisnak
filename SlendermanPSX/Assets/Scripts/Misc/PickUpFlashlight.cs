using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFlashligjt : MonoBehaviour
{
    public GameObject intText, flashlight;
    public bool canInteract;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            // intText.SetActive(true);
            canInteract = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            // intText.SetActive(false);
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
                flashlight.SetActive(true);
                intText.SetActive(false);
                gameObject.SetActive(false);
                canInteract = false;
            }
        }
    
    }
}
