using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{

    float speed = 20; // moveSpeed
    Animator animator;
    public Transform ball;
    public Transform aimTarget; // aiming gameObject
    public GameObject ChuckgameObject;
    public Transform[] targets; // array of targets to aim at

    float force = 13; // ball impact force
    Vector3 targetPosition; // position to where the bot will want to move
    public AudioSource hitSound;
    ShotManager shotManager; // shot manager class/component
    Animator Chuckanimator;

    void Start()
    {
        targetPosition = transform.position; // initialize the targetPosition to its initial position in the court
        animator = GetComponent<Animator>(); // reference to our animator for animations 
        shotManager = GetComponent<ShotManager>(); // reference to our shot manager to acces shots
        Chuckanimator = ChuckgameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Move(); // calling the move method
    }

    void Move()
    {
        targetPosition.x = ball.position.x; // update the target position to the ball's x position so the bot only moves on the x axis
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); // lerp it's position
    }

    Vector3 PickTarget() // picks a random target from the targets array to be aimed at
    {
        int randomValue = Random.Range(0, targets.Length); // get a random value from 0 to length of our targets array-1
        return targets[randomValue].position; // return the chosen target
    }

    Shot PickShot() // picks a random shot to be played
    {
        int randomValue = Random.Range(0, 2); // pick a random value 0 or 1 since we have 2 shots possible currently
        if (randomValue == 0) // if equals to 0 return a top spin shot type
            return shotManager.topSpin;
        else                   // else return a flat shot type
            return shotManager.flat;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) // if it collides with the ball
        {
            // Determine if the bot will miss the shot randomly
            bool willMiss = Random.Range(0f, 1f) < 0.2f; // Adjust the chance (0.2f) as needed

            if (willMiss)
            {
                Debug.Log("Bot missed the shot!"); // Optional: Log that the bot missed the shot
            }
            else
            {
                Shot currentShot = PickShot(); // pick a random shot to be played

                Vector3 dir = PickTarget() - transform.position; // get the direction to where to send the ball
                other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0); // set force to the ball

                Vector3 ballDir = ball.position - transform.position; // get the ball direction from the bot's position
                if (ballDir.x >= 0) // if it is on the right
                {
                    animator.Play("forehand"); // play a forehand animation
                    Chuckanimator.SetTrigger("right");
                }
                else
                {
                    animator.Play("backhand"); // otherwise play a backhand animation
                    Chuckanimator.SetTrigger("right");
                }
                hitSound.Play();
                ball.GetComponent<Ball>().hitter = "bot";
            }
        }
    }
}
