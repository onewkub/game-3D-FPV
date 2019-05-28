using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPointer : MonoBehaviour
{
    private string itemTag = "CollectionItem";
    public Transform PlayerBody;
    public EnermyScript enermy;
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
        RaycastHit hit;
        if(Physics.Raycast(PlayerBody.transform.position, PlayerBody.transform.forward, out hit, range))
        {

            if (hit.transform.CompareTag(itemTag))
            {
                enermy.itemIsCollected();
                Destroy(hit.transform.gameObject, 0.2f);
            }
        }
        
    }
}
