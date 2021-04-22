using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class k_EnemyBullet : MonoBehaviour
{

    public float speed;
    public float attackDamage;
    public AudioSource audio;

    public AudioClip hit;

    private Transform player;
    private float lifeTimer = 1f;

    void OnTriggerEnter(Collider other)
    {
        player = GameHandler.instance.GetPlayer();
        if (other.gameObject.tag == "Player")
        {
            audio = GetComponent<AudioSource>();
            audio.clip = hit;
            audio.Play();

            player.GetComponent<PlayerStats>().DecreaseHealth(attackDamage);
            Debug.Log("bullet hit");
        }
    }

        // Update is called once per frame
        void Update()
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0f)
        {
            Destroy(this.gameObject);
        }


        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
