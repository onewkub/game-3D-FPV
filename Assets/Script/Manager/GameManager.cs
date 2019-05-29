using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Item system")]
    public List<GameObject> itemOnMap;
    public List<GameObject> itemInInventory;

    [Header("Time Keeper")]
    public float timeLimit;
    public float timePassed;

    [Header("Player and Enemy Reference")]
    public GameObject player;
    public List<GameObject> enemyList;
    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
    }

    public void pickUpItem(GameObject item)
    {
        // validate first then move to inventory (also hide object)
        if (itemOnMap.Exists(x => item))
        {
            Debug.Log("I pickup");
            item.SetActive(false);
            itemOnMap.Remove(item);
            itemInInventory.Add(item);
            //tell all enemy when player pick something up
            BrodcastPositionToEnemy();
        }
    }

    public void BrodcastPositionToEnemy()
    {
        // use for tell all enemy the player position
        foreach (GameObject enemy in enemyList)
        {
            EnermyScript script = enemy.GetComponent<EnermyScript>();
            script.SetPos();
        }
    }
}
