using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2f;
    int wayPointIndex = 0;


    void Start() {
        transform.position = waypoints[wayPointIndex].transform.position;
    }

    void Update() {
        Move();
    }

    void Move() {
        if (wayPointIndex < waypoints.Count) {
            var targetPosition = waypoints[wayPointIndex].transform.position;
            transform.position = Vector2.MoveTowards(transform.position, 
                targetPosition,
                moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition) {
                wayPointIndex++;
            }
        }
        else {
            Destroy(gameObject);
        }
    }
}