using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlenderAI : MonoBehaviour
{
    public NavMeshAgent ai;

    public SkinnedMeshRenderer slenderMesh;

    public float m_speed, staticIncreaseRate, staticDecreaseRate, soundIncreaseRate, soundDecreaseRate, healthIncreaseRate, healthDecreaseRate, catchDistance, playerHealth = 100;

    public Slider healthSlider;
    public Image sliderFill, medical;

    // public FPSController playerScript;

    public Transform slenderMainTransform, playerTransform;

    public GameObject jumpscareCam, blackscreen;

    Vector3 dest;

    int randNum, randNum2;

    int teleportChance; 

    int token, token3, token4;

    public bool usingHealthSlider, enableCursorAfterDeath;
    
    public string scenename;

    float aiDistance;

    float staticAmount;

    float staticVolume;

    public List<Transform> teleportDestinations;

    public raycastSlender raycastScript;

    public RawImage staticscreen;

    public Color staticOpacity;

    public AudioSource staticSound, jumpscareSound;

    public Camera playerCam;
    public GameObject slender2;
    public Animator slenderAnim;
    

    // private GameObject player;
    

    void Start()
    {
        //log audio listener
        Debug.Log("Audio Listener: " + AudioListener.pause);
        // AudioListener.pause = false; 
        // playerScript = GameObject.Find("Player").GetComponent<FPSController>();
        // playerHealth = playerScript.playerHealth;
        // playerHealth = player.GetComponent<FPSController>().playerHealth;
        // player.SetActive(true);
    }

    void ResetSlender()
    {
        teleportChance = Random.Range(0, 3);
        if (teleportChance != 0)
        {
            randNum = Random.Range(0, teleportDestinations.Count);
            slenderMainTransform.position = teleportDestinations[randNum].position;
        }
    }

    IEnumerator KillPlayer()
    {
        yield return new WaitForSeconds(1.5f);
        blackscreen.SetActive(true);

        // AudioListener.pause = true;
        // DontDestroyOnLoad(playerCam.gameObject);
        // yield return new WaitForSeconds(1f);
        if (enableCursorAfterDeath == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        SceneManager.LoadScene(scenename);
    }

    void Update()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCam);

        if (GeometryUtility.TestPlanesAABB(planes, slenderMesh.bounds) && playerHealth > 0)
        {
            ai.enabled = false;
            ai.speed = 0;
            slenderAnim.ResetTrigger("moving");
            slenderAnim.SetTrigger("idle");

            ai.SetDestination(transform.position);
            if (raycastScript.detected == true)
            {
                token3 = 0;
                if (token4 == 0)
                {
                    randNum2 = Random.Range(0, 4);
                    if (randNum2 == 0)
                    {
                        jumpscareSound.Play();
                    }
                    token4 = 1;
                }
                slenderMainTransform.position = slenderMainTransform.position;
                staticVolume += soundIncreaseRate * Time.deltaTime;
                staticAmount += staticIncreaseRate * Time.deltaTime;
                playerHealth -= healthDecreaseRate * Time.deltaTime;
                if (staticVolume > 1)
                {
                    staticVolume = 1;
                }
                if (staticAmount > 0.7f)
                {
                    staticAmount = 0.7f;
                }
            }
        }

        if (!GeometryUtility.TestPlanesAABB(planes, slenderMesh.bounds) && playerHealth > 0 || raycastScript.detected == false && playerHealth > 0)
        {
            ai.speed = m_speed;
            ai.enabled = true;
            // slenderAnim.ResetTrigger("idle");
            // slenderAnim.SetTrigger("moving");
            if (token3 == 0)
            {
                ResetSlender();
                token3 = 1;
            }
            dest = playerTransform.position;
            token4 = 0;
            ai.destination = dest;
            staticAmount -= staticDecreaseRate * Time.deltaTime;
            staticVolume -= soundDecreaseRate * Time.deltaTime;
            playerHealth += healthIncreaseRate * Time.deltaTime;
            if (staticVolume < 0)
            {
                staticVolume = 0;
            }
            if (staticAmount < 0)
            {
                staticAmount = 0;
            }
            if (playerHealth > 100)
            {
                playerHealth = 100;
            }
        }

        if (usingHealthSlider == true)
        {
            healthSlider.value = playerHealth;
            
        }
        staticSound.volume = staticVolume;
        staticOpacity.a = staticAmount;
        staticscreen.color = staticOpacity;

        this.transform.LookAt(new Vector3(playerTransform.position.x, this.transform.position.y, playerTransform.position.z));

        aiDistance = Vector3.Distance(this.transform.position, playerTransform.position);
        // Debug.Log(aiDistance);

        if (playerHealth <= 0)
        {
            StartCoroutine(KillPlayer());
            // staticVolume += soundIncreaseRate * Time.deltaTime;
            // staticAmount += staticIncreaseRate * Time.deltaTime;
            // if (staticVolume > 1)
            // {
            //     staticVolume = 1;
            // }
            // if (staticAmount > 0.9f)
            // {
            //     staticAmount = 0.9f;
            // }
            staticAmount = 0;
            staticOpacity.a = 0;
            playerTransform.gameObject.SetActive(false);
            jumpscareCam.SetActive(true);
            ai.speed = 0;
            ai.SetDestination(transform.position);
            slenderAnim.Play("Scream");
        }
        if (aiDistance <= catchDistance)
        {
            if (token == 0)
            {
                playerHealth = 0;
                token = 1;
            }
        }
    }


}
