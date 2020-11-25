using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Si le projectile entre en contact avec un ennemi, le projectile est détruit.
    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Ennemi")){
            Destroy(gameObject);
        }
    }
}
