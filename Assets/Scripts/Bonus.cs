using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    //Variables pour les particules
    public ParticleSystem bonusParticule;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Si le player entre en collision avec le bonus, le bonus disparait avec un son;
    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Player")){
            bonusParticule.Play();
            Invoke("Destroy", 0.5f);
            
        }
    }
    public void Destroy(){
        Destroy(gameObject);
    }
}
