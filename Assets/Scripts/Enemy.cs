using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Variables pour le son
    public AudioClip deathSound;
    private AudioSource playerAudio;

    //Variables pour le mouvement 
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //On va chercher le rigidbody du player
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        //On va chercher la source audio
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Mouvement de l'ennemi
        Vector3 direction = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(direction * speed);
    }

    //Si l'ennemi entre en collision avec un projectile, il y a un effet , de son et ça invoke la fonction pour le Destroy.
    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Projectile")){
            Invoke("DestroyEnemy", 0.25f);
            playerAudio.PlayOneShot(deathSound, 1f);
        }
    }

    //Détruit l'ennemi
    public void DestroyEnemy(){
        Destroy(gameObject);
    }
}
