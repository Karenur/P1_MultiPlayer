using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public int playerNum;
    float mySpeed = 10f;

    void Update()
    {
        //Move
        transform.position += transform.up * mySpeed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Incluir RigidBody no prefab Bullet
        if (collision.collider.CompareTag("Player") && collision.collider.GetComponent<PlayerMovimento>().myNumber != playerNum)
        {
            collision.gameObject.SetActive(false);
            Debug.Log("colidi no player");
        }

        if (collision.collider.CompareTag("parede"))
        {
            Destroy(gameObject);
            Debug.Log("colidi na parede");
        }


    }
}
