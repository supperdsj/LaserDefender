using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = true;

    // Start is called before the first frame update
    IEnumerator Start() {
        // var currentWave = waveConfigs[startingWave];
        // StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        do {
            yield return StartCoroutine(SpawnAllWave());
        } while (looping);
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