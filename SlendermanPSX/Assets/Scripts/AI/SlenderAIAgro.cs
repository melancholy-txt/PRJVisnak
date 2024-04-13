using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlenderAIAgro : MonoBehaviour
{
    public Rigidbody playerRb;
    public NavMeshAgent ai;

    public SkinnedMeshRenderer slenderMesh;

    public float m_speed;

    public float staticIncreaseRate, staticDecreaseRate;

    public float soundIncreaseRate, soundDecreaseRate;

    public float healthIncreaseRate, healthDecreaseRate;
    
    public float catchDistance;

    public float playerHealth = 100;

    public Slider healthSlider;

    // public GameObject player;

    public Transform slenderMainTransform, playerTransform;

    public GameObject jumpscareCam;
    
    public GameObject blackscreen;

    Vector3 dest;

    int randNum, randNum2;

    int teleportChance; 

    int token, token3, token4;

    public bool usingHealthSlider;

    public bool enableCursorAfterDeath;
    
    public string scenename;

    float aiDistance;

    float staticAmount;

    float staticVolume;

    public List<Transform> teleportDestinations;

    public raycastSlender raycastScript;

    public RawImage staticscreen;

    public Color staticOpacity;

    public AudioSource staticSound;

    public AudioSource jumpscareSound;

    public Camera playerCam;
    public GameObject slender2;
    public Animator slenderAnim;
    public bool attackRunning = false;


    void Start()
    {
        AudioListener.pause = false; 
        // playerHealth = player.GetComponent<FPSController>().playerHealth;
        // player.SetActive(true);

    }

    void resetSlender()
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
        yield return new WaitForSeconds(3.5f);
        blackscreen.SetActive(true);
        AudioListener.pause = true;
        yield return new WaitForSeconds(6f);
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

        // if (GeometryUtility.TestPlanesAABB(planes, slenderMesh.bounds) && playerHealth > 0)
        // {
        //     ai.enabled = false;
        //     ai.speed = 0;
        //     slenderAnim.ResetTrigger("walking");
        //     slenderAnim.SetTrigger("idle");

        //     ai.SetDestination(transform.position);
        //     if (raycastScript.detected == true)
        //     {
        //         token3 = 0;
        //         if (token4 == 0)
        //         {
        //             randNum2 = Random.Range(0, 2);
        //             if (randNum2 == 0)
        //             {
        //                 jumpscareSound.Play();
        //             }
        //             token4 = 1;
        //         }
        //         slenderMainTransform.position = slenderMainTransform.position;
        //         staticVolume += soundIncreaseRate * Time.deltaTime;
        //         staticAmount += staticIncreaseRate * Time.deltaTime;
        //         playerHealth -= healthDecreaseRate * Time.deltaTime;
        //         if (staticVolume > 1)
        //         {
        //             staticVolume = 1;
        //         }
        //         if (staticAmount > 0.9f)
        //         {
        //             staticAmount = 0.9f;
        //         }
        //     }
        // }

        // if (!GeometryUtility.TestPlanesAABB(planes, slenderMesh.bounds) && playerHealth > 0 || raycastScript.detected == false && playerHealth > 0)
        // {
            if (aiDistance > catchDistance)
                ai.speed = m_speed;
            ai.enabled = true;
            slenderAnim.ResetTrigger("idle");
            slenderAnim.SetTrigger("moving");

            dest = playerTransform.position;
            ai.destination = dest;
            if (playerHealth > 100)
            {
                playerHealth = 100;
            }
        // }

        if (usingHealthSlider == true)
        {
            healthSlider.value = playerHealth;
        }
        staticSound.volume = staticVolume;
        staticOpacity.a = staticAmount;
        staticscreen.color = staticOpacity;

        transform.LookAt(new Vector3(playerTransform.position.x, this.transform.position.y, playerTransform.position.z));

        aiDistance = Vector3.Distance(this.transform.position, playerTransform.position);
        // Debug.Log(aiDistance);

        if (playerHealth <= 0)
        {
            StartCoroutine(KillPlayer());
            staticVolume += soundIncreaseRate * Time.deltaTime;
            staticAmount += staticIncreaseRate * Time.deltaTime;
            if (staticVolume > 1)
            {
                staticVolume = 1;
            }
            if (staticAmount > 0.9f)
            {
                staticAmount = 0.9f;
            }
            playerTransform.gameObject.SetActive(false);
            jumpscareCam.SetActive(true);
            ai.speed = 0;
            ai.SetDestination(transform.position);
        }
        if (aiDistance <= catchDistance)
        {
            if(!attackRunning)
                StartCoroutine(AttackPlayer0());


            // slenderAnim.Play("Attack 0");
            // ai.speed = 0;
            // new WaitForSeconds(3.5f);
            // playerHealth -= 1;
            // slenderAnim.Play("Run");
            // if (token == 0)
            // {
            //     playerHealth = 0;
            //     token = 1;
            // }
        }
    }
    IEnumerator AttackPlayer0()
    {
        attackRunning = true;
        ai.enabled = false;
        // ai.speed = 0;
        slenderAnim.Play("Attack 0");
        yield return new WaitForSeconds(1.5f);
        if (Vector3.Distance(transform.position, playerTransform.position) <= catchDistance)
        {
            playerHealth -= 10;
            // playerTransform.position -= playerTransform.forward * 0.1f;
            // playerTransform.position += playerTransform.up * 0.5f;
            // playerRb.AddForce(-playerTransform.forward * 1000);
            // playerRb.AddForce(playerTransform.up * 1000);

        }
        ai.enabled = true;
        ai.speed = m_speed;
        attackRunning = false;
    }


}
