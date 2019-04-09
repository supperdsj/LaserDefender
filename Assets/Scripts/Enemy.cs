using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Enemy : MonoBehaviour {
    [Header("Enemy Stats")] [SerializeField]
    int score = 100;
    [SerializeField] float health = 100;

    [Header("Shooting")] [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShot = 3f;
    [SerializeField] float maxTimeBetweenShot = 4f;
    [SerializeField] GameObject laserObject;
    [SerializeField] float laserSpeed = 10f;

    [Header("Effects")] [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.7f;

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
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) {
            return;
        }

        damageDealer.Hit();
        health -= damageDealer.GetDamage();
        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        FindObjectOfType<GameSession>().AddToScore(score);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
}