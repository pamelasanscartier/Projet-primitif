using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void CommencerPartie(){
        //Va chercher la prochaine scène dans l'index build (j'ai mis la scène du jeu, donc ça va la chercher)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitterPartie(){
        //Fais quitter l'application quand c'est un joueur, mais nous on le voit pas donc on met un message log
        Debug.Log("Vous avez quitté !");
        Application.Quit();
    }
    public void RejouerPartie(){
        //Va chercher la scène d'avant dans l'index build (j'ai mis la scène du jeu, donc ça va la chercher)
        SceneManager.LoadScene(1);
    }
}
