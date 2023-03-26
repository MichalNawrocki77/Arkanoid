using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpType powerUpType;
    Rigidbody2D powerUpsRigidbody;
    [SerializeField] GameManager gameManagerScript;
    private void Start()
    {
        gameManagerScript = GameObject.FindGameObjectsWithTag(StaticConstants.gameManagerTag)[0].GetComponent<GameManager>();
        if (gameManagerScript == null)
        {
            Debug.Log("PowerUp's gameManagerScript field is NULL");
        }
        float tempInitialHorizontalPushForce = Random.Range(-1f, 1f);
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(tempInitialHorizontalPushForce, 5), ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == StaticConstants.platfromTag)
        {   
            gameManagerScript.ZebranoWzmocnienie(powerUpType);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == StaticConstants.bottomBorderTag)
        {
            Destroy(this.gameObject);
        }
    }
}
