using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LookAtSlender : MonoBehaviour
{
    public RawImage staticImage;
    public Color color;
    public float drainRate, rechargeRate, health, healthDamage, healthRechargeRate, maxStaticAmount;
    public float audioIncreaseRate, audioDecreaseRate;
    public bool looking, canRecharge;
    public AudioSource staticSound;
    public string deathScene;

    void Start()
    {
        color.a = 0f;
        health = 100f;
    }
    void OnBecameVisible()
    {
        print("looking " + color.a);
        looking = true;
    }
    void OnBecameInvisible()
    {
        looking = false;
        print("not looking " + color.a);

    }
    void FixedUpdate()
    {
        
        if (color.a > maxStaticAmount)
        {

        }
        else if (color.a < maxStaticAmount)
        {
            staticImage.color = color;
        }
        if (looking == true)
        {
            color.a += Mathf.Clamp(drainRate * Time.deltaTime, 0f, 0.2f);
            health = health - healthDamage * Time.deltaTime;
            staticSound.volume += audioIncreaseRate * Time.deltaTime;
        }
        if (looking == false)
        {
            color.a = Mathf.Clamp(color.a - rechargeRate * Time.deltaTime, 0f, 0.5f);
            if (canRecharge == true)
            {
                health += healthRechargeRate * Time.deltaTime;
            }
            staticSound.volume -= audioDecreaseRate * Time.deltaTime;
        }
        if (health < 1)
        {
            //SceneManager.LoadScene(deathScene);
        }
    }
}
