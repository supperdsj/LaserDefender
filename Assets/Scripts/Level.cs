using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {
    [SerializeField] float delayInSeconds = 3f;
    public void LoadStartMenu() {
        SceneManager.LoadScene(0);
    }
    public void LoadMainGame() {
        SceneManager.LoadScene("Game");
    }

    public void LoadGameOver() {
        StartCoroutine(WaitAndLoad());
    }

    public void QuitGame() {
        Application.Quit();
    }

    IEnumerator WaitAndLoad() {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("GameOver");
    }
}
