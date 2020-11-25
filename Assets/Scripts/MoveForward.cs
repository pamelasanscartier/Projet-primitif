using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    //Variable pour la vitesse 

    public float speed = 40;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Faire avancer rapidement le projectile lorsqu'il est instantié
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
