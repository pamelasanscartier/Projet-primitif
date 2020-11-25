using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Aller chercher les prefabs
    public GameObject enemyPrefabs;
    public GameObject player;

    //Pour la position du projectile lorsqu'il va être lancé
    private Vector3 offset = new Vector3(0, 0, 20);

    //Variables pour le invoke
    private float startDelay = 1;
    private float repeatRate = 2.0f;

    //Aller chercher le script du Player
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        //Aller chercher le script playerController
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        //Invoke la fonction pour spawner des ennemis
        InvokeRepeating("SpawnEnemy", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Fonction qui crée des ennemis
    void SpawnEnemy(){
     
        Vector3 spawnPos = player.transform.position + offset;
        Instantiate(enemyPrefabs, spawnPos, enemyPrefabs.transform.rotation);

    }
}
