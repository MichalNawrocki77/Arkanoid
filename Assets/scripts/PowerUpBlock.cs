using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBlock : MonoBehaviour
{
    //Power-ups    
    [SerializeField] GameObject[] PowerUpPrefabs;
    //Identyfikator wzmocnienia
    public int powerUpId;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag ==StaticConstants.ballTag)
        {
            DropRandomPowerUp();
            Destroy(this.gameObject);           
        }
    }
    void DropRandomPowerUp()
    {
        powerUpId = Random.Range(0,5);
        GameObject tempPrefab = Instantiate(PowerUpPrefabs[powerUpId], this.gameObject.transform.position, Quaternion.identity);
        tempPrefab.GetComponent<PowerUp>().powerUpType = (PowerUpType)powerUpId;
    }
}
