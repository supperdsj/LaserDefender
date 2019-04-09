using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] List<WaveConfig> waveConfigs;

    int startingWave = 0;

    // Start is called before the first frame update
    void Start() {
        // var currentWave = waveConfigs[startingWave];
        // StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        StartCoroutine(SpawnAllWave());
    }

    // Update is called once per frame
    void Update() {
    }

    IEnumerator SpawnAllWave() {
        for (var i = startingWave; i < waveConfigs.Count; i++) {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
    
    IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig) {
        for (var i = 0; i < waveConfig.GetNumberOfEnemies(); i++) {
            var enemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWayPoints()[0].transform.position,
                Quaternion.identity);
            enemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpans());
        }
    }
}