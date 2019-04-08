using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditorInternal.VR;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float padding = 1f;
    [SerializeField] GameObject laserPrefab;
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

    IEnumerator FireContinuously() {
        while (true) {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
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