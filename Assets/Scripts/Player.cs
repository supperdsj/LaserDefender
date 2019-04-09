using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditorInternal.VR;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Player")] [SerializeField] float moveSpeed = 10;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.7f;

    [Header("Projectile")] [SerializeField]
    GameObject laserPrefab;

    [SerializeField] float laserSpeed = 20f;
    [SerializeField] float laserPeriod = 0.1f;
    [SerializeField] Coroutine fireCoroutine = null;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    void Start() {
        SetupMoveBoundaries();
    }

    void Update() {
        Move();
        Fire();
    }

    void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) {
            Debug.Log("no damage");
            return;
        }

        damageDealer.Hit();
        health -= damageDealer.GetDamage();
        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        FindObjectOfType<Level>().LoadGameOver();
    }

    void Move() {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    void Fire() {
        if (Input.GetButtonDown("Fire1")) {
            fireCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1")) {
            StopCoroutine(fireCoroutine);
            // StopAllCoroutines();
        }
    }

    public int GetHealth() {
        return health;
    }
    
    IEnumerator FireContinuously() {
        while (true) {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(laserPeriod);
        }
    }

    void SetupMoveBoundaries() {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}