using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastSlender : MonoBehaviour
{
    public GameObject playerObj;
    public Transform slenderTransform;
    public bool detected;
    public Vector3 offset;

    void Update()
    {
        Vector3 direction = (playerObj.transform.position - slenderTransform.position).normalized;

        if (Physics.Raycast(slenderTransform.position + offset, direction, out RaycastHit hit, Mathf.Infinity))
        {
            Debug.DrawLine(slenderTransform.position + offset, hit.point, Color.red, Mathf.Infinity);
            if (hit.collider.gameObject == playerObj)
            {
                detected = true;
            }
            else
            {
                detected = false;
            }
        }
    }
}
