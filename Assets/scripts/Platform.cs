using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    //[SerializeField] Rigidbody2D platformRigidbody;
    public float platformSpeed;
    float currentXPosition;
    [SerializeField] AudioManager audioManager;
    [SerializeField] Camera cameraObject;
    void Update()
    {
        UserInputKeyboard();
        //UserInputMouse();
    }
    void UserInputKeyboard()
    {
        currentXPosition = this.gameObject.transform.position.x;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentXPosition += Vector2.left.x*platformSpeed*Time.deltaTime;
            currentXPosition = Mathf.Clamp(currentXPosition, -5f, 2.3f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            currentXPosition += Vector2.right.x * platformSpeed * Time.deltaTime;
            currentXPosition = Mathf.Clamp(currentXPosition, -5f, 2.3f);            
        }
        this.gameObject.transform.position = new Vector2(currentXPosition, this.gameObject.transform.position.y);
    }
    void UserInputMouse()
    {
        currentXPosition = cameraObject.ScreenToWorldPoint(Input.mousePosition).x;
        currentXPosition = Mathf.Clamp(currentXPosition, -5f, 2.3f);
        this.gameObject.transform.position = new Vector2(currentXPosition, this.gameObject.transform.position.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag ==StaticConstants.ballTag)
        {
            Rigidbody2D ballRb = collision.rigidbody;
            Vector2 collisionPoint = collision.GetContact(0).point;
            Vector2 centerOfPlatform = this.gameObject.transform.position;
            ballRb.velocity = Vector2.zero;
            ballRb.velocity = new Vector2(-(centerOfPlatform.x - collisionPoint.x), 1).normalized * Ball.currentBallSpeed;
            audioManager.PlayBallHitsPlatformSFX();
        }
    }
}
