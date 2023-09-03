using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class rules : MonoBehaviour
{
    public void StartGame()
    {
        // Load the game scene when the "Play" button is clicked.
        SceneManager.LoadScene("Rules"); // Replace "GameScene" with your actual game scene name.
    }
}
