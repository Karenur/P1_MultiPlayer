using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class PlayerMovimento : NetworkBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform saidaTiro;

    int speed = 5;
    int rotationspeed = 20;

    public int myNumber;

    //Similar a um Start ou Awaken, mas funciona melhor em rede
    public override void OnNetworkSpawn()
    {
        myNumber = (int)NetworkObject.NetworkObjectId;
    }

    void Update()
    {
        if (!IsOwner)
            return;


        if (IsServer)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                NetworkManager.Singleton.Shutdown();
            }
        }

        //Move
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0f,0f, rotationspeed * Time.deltaTime * rotationspeed));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0f,0f, rotationspeed * Time.deltaTime * -rotationspeed));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += speed * Time.deltaTime * transform.up;
        }

        //Fire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Fire();
            FireServerRpc();
        }
    }
    void Fire()
    {
        BulletMovement bullet = Instantiate(bulletPrefab, saidaTiro.position, saidaTiro.rotation).GetComponent<BulletMovement>();
        bullet.playerNum = myNumber;
    }

    //Informacao de tiro mandada para o servidor/host
    [ServerRpc]
    void FireServerRpc()
    {
        FireClientRpc();
    }

    //Informacao de tiro mandada para todo cliente (incluindo host)
    [ClientRpc]
    void FireClientRpc()
    {
        Fire();
    }
}
