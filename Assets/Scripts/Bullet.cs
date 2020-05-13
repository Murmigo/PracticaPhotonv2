using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviourPun
{
    public float LifeTime;
    public int damage = 1;
    public float bulletSpeed = 20f;

    PhotonView pv;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * 100f * bulletSpeed);



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collided with Player");
            pv.RPC("DamagePlayer", RpcTarget.All, damage);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (LifeTime >= 0f)
            LifeTime -= Time.deltaTime;
        else
        {
            pv.RPC("DestroyBullet", RpcTarget.All);
        }

    }


    [PunRPC]
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
