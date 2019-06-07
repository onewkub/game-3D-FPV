using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingGame : MonoBehaviour
{
    public GameObject gameManger;
    private GameManager gameManagerData;
    private void Start()
    {
        gameManagerData = gameManger.GetComponent<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(gameManagerData.itemInInventory.Count == 1 && other.transform.name == "Player")
        {
            Debug.Log("Im quit");
            Application.Quit();
        }
    }
}
