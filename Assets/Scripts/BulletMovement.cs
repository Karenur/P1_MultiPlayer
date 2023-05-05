using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public int playerNum;
    float mySpeed = 10f;

    void Update()
    {
        transform.position += transform.up * mySpeed * Time.deltaTime;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && collision.collider.GetComponent<PlayerMovimento>().myNumber != playerNum)
        {
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }

        if (collision.collider.CompareTag("parede"))
        {
            Destroy(gameObject);
        }
    }

}
