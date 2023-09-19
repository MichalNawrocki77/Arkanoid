using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //ball movement
    Rigidbody2D ballRigidbody;
    public static float currentBallSpeed;
    //Scripts of Game Objects
    GameManager gameManagerScript;
    AudioManager audioManager;
    private void Start()
    {
        currentBallSpeed = StaticConstants.BallStartSpeed;
        this.ballRigidbody = GetComponent<Rigidbody2D>();
    }
    public void FirstBallPush()
    {
        ballRigidbody.velocity = new Vector2(0, currentBallSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag ==StaticConstants.bottomBorderTag)
        {
            gameManagerScript.LifeLost();
        }        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag ==StaticConstants.BlockTag)
        {
            audioManager.PlayBallHitsBlockSFX();
            Destroy(collision.gameObject);
        }
    }
    public void AssignVariables(GameManager gameManagerScript, AudioManager audioManager)
    {
        this.audioManager = audioManager;
        this.gameManagerScript = gameManagerScript;
    }
}
