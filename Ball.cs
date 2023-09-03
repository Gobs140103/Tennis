using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    Vector3 initialPos; // ball's initial position
    public string hitter;
    int PlayerScore;
    int BotScore;
    [SerializeField] Text PlayerScoreText;
    [SerializeField] Text BotScoreText;
    public bool playing = true;
    Animator Chuckanimator;
    public AudioSource hitSound;
    public GameObject ChuckgameObject;
    private MeshRenderer meshRenderer; // Reference to the MeshRenderer component
    public Transform Whistle;
    private void Start()
    {
        Chuckanimator = ChuckgameObject.GetComponent<Animator>();
        initialPos = transform.position; // default it to where we first place it in the scene
        PlayerScore = 0;
        BotScore = 0;
        gameObject.SetActive(false);

        // Get the MeshRenderer component
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall")) // if the ball hits a wall
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero; // reset its velocity to 0 so it doesn't move anymore
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            GameObject.Find("player").GetComponent<Player>().Reset();
            Whistle.gameObject.SetActive(true);
            hitSound.Play();

            // Disable the MeshRenderer
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }

            if (playing)
            {
                if (hitter == "player")
                {
                    PlayerScore++;
                }
                else if (hitter == "bot")
                {
                    BotScore++;
                }
                playing = false;
                updateScore();
            }
        }
        else if (collision.transform.CompareTag("net")) // if the ball hits a wall
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero; // reset it's velocity to 0 so it doesn't move anymore
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            GameObject.Find("player").GetComponent<Player>().Reset();
            Whistle.gameObject.SetActive(true);
            hitSound.Play();

            // Disable the MeshRenderer
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }

            if (playing)
            {
                if (hitter == "player")
                {
                    BotScore++;
                }
                else if (hitter == "bot")
                {
                    PlayerScore++;
                }
                playing = false;
                updateScore();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Out") && playing)
        {
            Whistle.gameObject.SetActive(true);
            hitSound.Play();

            // Disable the MeshRenderer
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }

            if (hitter == "player")
            {
                BotScore++;
            }
            else if (hitter == "bot")
            {
                PlayerScore++;
            }
            playing = false;
            updateScore();
        }
    }

    public void updateScore()
    {
        Chuckanimator.SetTrigger("right");
        PlayerScoreText.text = "Player: " + PlayerScore;
        BotScoreText.text = "Bot: " + BotScore;
        if (PlayerScore >= 21)
        {
            SceneManager.LoadScene("GameOver");
        }
        else if (BotScore >= 21)
        {
            SceneManager.LoadScene("GameOv");
        }
    }
}
