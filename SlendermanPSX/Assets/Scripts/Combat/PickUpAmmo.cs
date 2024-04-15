using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : MonoBehaviour
{
    public GameObject intText;
    public GunScript gunScript;
    public AudioSource pickupSound;
    public bool canInteract;
    public int ammoValue = 20;

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
                gunScript.currentMagazineSize += ammoValue;
                intText.SetActive(false);
                gameObject.SetActive(false);
                canInteract = false;
            }
        }
    
    }
}
