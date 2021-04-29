using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class k_EnemyBullet : MonoBehaviour
{

    public float speed;
    public int attackDamage;
    public AudioSource audio;

    public AudioClip hit;

    private Transform player;
    public float lifeTimer = 1f;

    void OnTriggerEnter(Collider other)
    {
        player = GameHandler.instance.GetPlayer();
        if (other.gameObject.tag == "Player")
        {
            audio = GetComponent<AudioSource>();
            audio.clip = hit;
            audio.Play();

            player.GetComponent<PlayerStats>().DecreaseHealth(attackDamage);
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
