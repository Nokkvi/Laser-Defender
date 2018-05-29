using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject laserPrefab;
    public float speed;
    public float padding = 0.5f;
    public float projectileSpeed;
    public float firingRate = 0.2f;
    public float health = 250;

	float xmin;
	float xmax;

	// Use this for initialization
	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
		}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

        //Restrict player to placespace
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
}
