using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {
    List<Transform> wayPoints;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] WaveConfig waveConfig;
    int wayPointIndex = 0;


    void Start() {
        wayPoints=waveConfig.GetWayPoints();
        transform.position = wayPoints[wayPointIndex].transform.position;
    }

    void Update() {
        Move();
    }

    void Move() {
        if (wayPointIndex < wayPoints.Count) {
            var targetPosition = wayPoints[wayPointIndex].transform.position;
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