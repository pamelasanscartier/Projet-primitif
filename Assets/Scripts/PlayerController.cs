using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Variables pour faire bouger de droite à gauche le player
    private float horizontalInput;
    private float forwardInput;
    public float speed = 5;
    private float xLimit = 2.7f;
    private float zLimit = 69;
    private float winningLine = 66;

    //Variables pour le rigidbody, le saut
    private Rigidbody playerRb;
    public float jumpForce = 200;
    public float gravityModifier = 3;
    public bool isOnGround = true;
    

    //Variables pour le son
    public AudioClip victorySound;
    public AudioClip jumpSound;
    public AudioClip bonusSound;
    public AudioClip collisionSound;
    public AudioClip defeatSound;
    private AudioSource playerAudio;

    //Variables pour les particules
    public ParticleSystem dirtParticule;
    public ParticleSystem explosionParticule;

    //Variables pour le menu de fin
    bool bonusUn = false;
    bool bonusDeux = false;
    bool bonusTrois = false;

    //Variables pour les projectiles
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        //On va chercher le rigidbody du player
        playerRb = GetComponent<Rigidbody>();
        
        //Rendre le saut plus "réaliste"
        Physics.gravity *= gravityModifier;

        //Aller chercher la source audio
        playerAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        //Faire bouger de gauche à droite le player
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput, Space.World);
        forwardInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput, Space.World);

        // si la position x depasse la limite du background, on le fait ''arrêter'' en le remettant exactement à la place ou le background commence. Pareil pour l'extrémité en z
        if ( transform.position.x > xLimit)  {
            transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
        } 
        else if ( transform.position.x < -xLimit)  {
            transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
        }
        else if ( transform.position.z > zLimit)  {
            transform.position = new Vector3(transform.position.x, transform.position.y, zLimit);
        }

        //Si le player dépasse la ligne d'arrivée, on ajoute les sons et on affiche la scène de fin nécessaire
        if(transform.position.z > winningLine){
            //Et qu'il a eu une étoile, on invoke la scène finale de victoire une étoile
            if((bonusUn == true && bonusDeux == false && bonusTrois == false) || (bonusUn == false && bonusDeux == true && bonusTrois == false) || (bonusUn == false && bonusDeux == false && bonusTrois == true)){
                Debug.Log("Bravo");Invoke("VictoireUnEtoile", 1);
                playerAudio.PlayOneShot(victorySound, 1f);
                return;
            }
            //Et qu'il n'a eu aucune étoile, on invoke la fin défaite
            else if ((bonusUn == false && bonusDeux == false && bonusTrois == false)){
                Invoke("Defaite", 1);
                playerAudio.PlayOneShot(defeatSound, 1f);
                return;
            }
            //Et qu'il a eu deux étoiles, on invoke la scène finale de victoire deux étoiles
            else if((bonusUn == true && bonusDeux == true && bonusTrois == false) ||(bonusUn == true && bonusDeux == false && bonusTrois == true) ||
            (bonusUn == false && bonusDeux == true && bonusTrois == true)){
                Debug.Log("WOW");
                Invoke("VictoireDeuxEtoiles", 1);
                playerAudio.PlayOneShot(victorySound, 1f);
                return;
            }

            //Et qu'il a eu trois étoiles, on invoke la scène finale de victoire trois étoiles
            else if ((bonusUn == true && bonusDeux == true && bonusTrois == true)){
                Invoke("VictoireTroisEtoiles", 1);
                playerAudio.PlayOneShot(victorySound, 1f);
                return;
            }
        }

        //Si le player est sur le sol (isOnGround) et que la touche S est appuyé, le player saute
        if (Input.GetKeyDown(KeyCode.S) && isOnGround)
        {
            //Rigidbody.AddForce(Vector3.force, ForceMode mode = ForceModeMode.Force);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSound, 1f);
            dirtParticule.Stop();
            //Préciser que quand il saute, il n'est plus sur le sol
            isOnGround = false;
        } 

        //Lorsque la touche A est appuyée, on lance des projectiles.
        if (Input.GetKeyDown(KeyCode.A))
        {
           Instantiate(projectilePrefab , transform.position + new Vector3(0,-0.25f,0), transform.rotation);
        } 
    }

    //Scène victoire une étoile
    public void VictoireUnEtoile(){
        SceneManager.LoadScene("OneStarGO");
    }
    //Scène victoire deux étoiles
    public void VictoireDeuxEtoiles(){
        SceneManager.LoadScene("TwoStarsGO");
    }
    //Scène victoire trois étoiles
    public void VictoireTroisEtoiles(){
        SceneManager.LoadScene("ThreeStarsGO");
    }
    //Scène défaite
    public void Defaite(){
        SceneManager.LoadScene("GameOverPerdu");
    }

    //Si le player entre en collision
    private void OnCollisionEnter(Collision collision){
        //Avec le sol ou un obstacle
        if(collision.gameObject.CompareTag("Gazon") || collision.gameObject.CompareTag("Obstacle")){
            isOnGround = true;
            dirtParticule.Play();
        }
        //Avec un ennemi
        if(collision.gameObject.CompareTag("Ennemi")){
            explosionParticule.Play();
            Invoke("Defaite", 1);
            playerAudio.PlayOneShot(collisionSound, 1f);
        }
        //Avec le bonus 1
        if (collision.gameObject.CompareTag("Bonus")){
            playerAudio.PlayOneShot(bonusSound, 1f);
            bonusUn = true;
        }
        //Avec le bonus 2
        if (collision.gameObject.CompareTag("Bonus2")){
            playerAudio.PlayOneShot(bonusSound, 1f);
            bonusDeux = true;
        }
        //Avec le bonus 3
        if (collision.gameObject.CompareTag("Bonus3")){
            playerAudio.PlayOneShot(bonusSound, 1f);
            bonusTrois = true;
        }
    }
}
