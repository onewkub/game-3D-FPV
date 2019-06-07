﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPointer : MonoBehaviour
{
    private string itemTag = "CollectionItem";
    public Transform PlayerBody;
    public EnemyScript enermy;
    public float range = 20f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SelectIt();
        }       
    }

    void SelectIt()
    {
        if(Physics.Raycast(PlayerBody.transform.position, PlayerBody.transform.forward, out RaycastHit hit, range))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag(itemTag))
            {
                GameManager.Instance.pickUpItem(hit.transform.gameObject);
            }
        }
        
    }
}
