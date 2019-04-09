using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Enemy : MonoBehaviour {
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShot = 0.4f;
    [SerializeField] float maxTimeBetweenShot = 0.5f;
    [SerializeField] GameObject laserObject;
    [SerializeField] float laserSpeed = 10f;

    void Start() {
        shotCounter = Random.Range(minTimeBetweenShot, maxTimeBetweenShot);
    }

    void Update() {
        CountDownAndShot();
    }

    void CountDownAndShot() {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0) {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShot, maxTimeBetweenShot);
        }
    }

    void Fire() {
        GameObject laser = Instantiate(laserObject, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * laserSpeed);
    }

    void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) {
            return;
        }

        damageDealer.Hit();
        health -= damageDealer.GetDamage();
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}