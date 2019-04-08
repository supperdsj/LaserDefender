using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] WaveConfig currentWave;

    int startingWave = 0;

    // Start is called before the first frame update
    void Start() {
        currentWave = waveConfigs[startingWave];
        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    // Update is called once per frame
    void Update() {
    }

    IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig) {
        for (var i = 0; i < waveConfig.GetNumberOfEnemies(); i++) {
            var enemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWayPoints()[0].transform.position,
                Quaternion.identity);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpans());
        }
    }
}