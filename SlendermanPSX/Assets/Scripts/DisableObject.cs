using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public GameObject Obj;
    public float activeTime;
    
    void Update()
    {
        if (Obj.activeSelf == true)
        {
            StartCoroutine(DisableObj());
        }
    }

    IEnumerator DisableObj()
    {
        yield return new WaitForSeconds(activeTime);
        Obj.SetActive(false);
    }
}
