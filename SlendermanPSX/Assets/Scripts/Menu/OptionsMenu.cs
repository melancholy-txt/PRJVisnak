using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider, sensitivitySlider, fovSlider;
    // public FPSController player;
    public Camera playerCam;

    void Start()
    {
        if (!PlayerPrefs.HasKey("gameLaunched"))
        {
            PlayerPrefs.SetFloat("masterVolume", 1);
            PlayerPrefs.SetFloat("mouseSensitivity", 1.4f);
            PlayerPrefs.SetFloat("fieldOfView", 60);
            PlayerPrefs.SetInt("gameLaunched", 0);
            PlayerPrefs.SetInt("aspectRatio", 0);
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.HasKey("gameLaunched"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
            AudioListener.volume = PlayerPrefs.GetFloat("masterVolume");

            sensitivitySlider.value = PlayerPrefs.GetFloat("mouseSensitivity");
            // player.lookSpeed = PlayerPrefs.GetFloat("mouseSensitivity");

            if (PlayerPrefs.GetInt("aspectRatio") == 0)
            {
                Screen.SetResolution(320, 240, true);
            }
            else if (PlayerPrefs.GetInt("aspectRatio") == 1)
            {
                Screen.SetResolution(320, 180, true);
            }


            //fovSlider.value = PlayerPrefs.GetFloat("fieldOfView");
            //playerCam.fieldOfView = PlayerPrefs.GetFloat("fieldOfView");
        }
    }
   
    public void SetFOV()
    {
        //PlayerPrefs.SetFloat("fieldOfView", fovSlider.value);
        //PlayerPrefs.Save();
        //playerCam.fieldOfView = PlayerPrefs.GetFloat("fieldOfView");
    }
    public void SetVolume()
    {
        PlayerPrefs.SetFloat("masterVolume", volumeSlider.value);
        PlayerPrefs.Save();
        AudioListener.volume = PlayerPrefs.GetFloat("masterVolume");
    }
    // public void SetSensitivity()
    // {
    //     PlayerPrefs.SetFloat("mouseSensitivity", sensitivitySlider.value);
    //     PlayerPrefs.Save();
    //     player.lookSpeed = PlayerPrefs.GetFloat("mouseSensitivity");
    // }

    public void SetFourByThree()
    {
        PlayerPrefs.SetInt("aspectRatio", 0);
        PlayerPrefs.Save();
         Screen.SetResolution(320, 240, true);
    }

    public void SetSixteenByNine()
    {
        PlayerPrefs.SetInt("aspectRatio", 1);
        PlayerPrefs.Save();
        Screen.SetResolution(320, 180, true);
    }
}
