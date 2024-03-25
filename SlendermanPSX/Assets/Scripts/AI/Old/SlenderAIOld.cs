using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlenderAIOld : MonoBehaviour
{
    public Transform dest1, dest2, dest3, player;
    bool teleporting = true;
    public float teleportRate;
    int randNum;

    void Start()
    {
        StartCoroutine(Teleport());
    }
    void Update()
    {
        this.transform.LookAt(new Vector3(player.position.x, this.transform.position.y, player.position.z));
    }
    IEnumerator Teleport()
    {
        while (teleporting == true)
        {
            yield return new WaitForSeconds(teleportRate);
            randNum = Random.Range(0, 2);
            switch (randNum)
            {
                default:
                    break;
                case 0:
                    this.transform.position = dest1.position;
                    break;
                case 1:
                    this.transform.position = dest2.position;
                    break;
                case 2:
                    this.transform.position = dest3.position;
                    break;
            }
            
        }
    }
}
