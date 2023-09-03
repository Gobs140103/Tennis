using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pause;
    public GameObject pauseMenu; // Reference to the entire pause menu panel
    public GameObject playerText; // Reference to the player text element
    public GameObject botText; // Reference to the bot text element
    public void ResumeGame()
    {
        // Turn off the pause menu (including panels)
        pause.SetActive(false);
        pauseMenu.SetActive(true);

        // Turn on the player and bot text elements
        playerText.SetActive(false);
        botText.SetActive(false);

        // You may also want to resume game logic here if it was paused.


    }
}
