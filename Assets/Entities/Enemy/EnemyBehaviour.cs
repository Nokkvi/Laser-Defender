using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public GameObject laserPrefab;
    public float projectileSpeed;
    public float health = 150;
    public float shotsPerSeconds = 0.5f;
    public int killReward = 100;

    private ScoreKeeper scoreKeeper;

    public AudioClip fireSound;
    public AudioClip deathSound;

    void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }


    void OnTriggerEnter2D (Collider2D collider)
    {
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if(health <= 0)
            {
                Destroy(gameObject);
                scoreKeeper.Score(killReward);
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
            }
        }
    }

    void Update()
    {
        float probability = Time.deltaTime * shotsPerSeconds;
        if(Random.value < probability)
        {
            Fire();
        }
    }

    void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }
}
