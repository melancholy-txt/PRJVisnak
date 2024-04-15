using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpPage : MonoBehaviour
{
    public GameObject collectTextObj, intText, slender1, slender2, slenderSlider;
    public AudioSource pickupSound, ambienceLayer1, ambienceLayer2, ambienceLayer3, ambienceLayer4, ambienceLayer5, ambienceLayer6, ambienceLayer7, ambienceLayer8;
    public bool canInteract;
    public static int pagesCollected;
    public Text collectText;

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

    private void Update()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pagesCollected++;
                collectText.text = pagesCollected + "/8 pages";
                collectTextObj.SetActive(true);
                pickupSound.Play();
                switch (pagesCollected)
                {
                    default:
                        ambienceLayer1.Play();
                        
                        break;
                    case 1: 
                        ambienceLayer1.Play(); 
                        slender1.SetActive(false);
                        slender2.SetActive(true);
                        break;
                    case 2: ambienceLayer2.Play(); break;
                    case 3: ambienceLayer3.Play(); break;
                    case 4: ambienceLayer4.Play(); break;
                    case 5: ambienceLayer5.Play(); break;
                    case 6: ambienceLayer6.Play(); break;
                    case 7: ambienceLayer7.Play(); break;
                    case 8: 
                        ambienceLayer8.Play(); 
                        slender1.SetActive(false);
                        slender2.SetActive(true);
                        slenderSlider.SetActive(true);
                        break;                   
                }
                intText.SetActive(false);
                this.gameObject.SetActive(false);
                canInteract = false;

            }
        }
    }
}
