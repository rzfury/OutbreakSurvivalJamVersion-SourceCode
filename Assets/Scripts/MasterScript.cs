using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MasterScript : MonoBehaviour
{
    public List<GameObject> triggeredZombie;
    public GameObject pausePanel;
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public AudioClip hitSFX;
    public AudioClip healSFX;
    public AudioClip shootSFX;
    public Text zombieCountUi;
    public Text waveCountUi;
    public Text zombieKilledGameOver;

    public int zombieKilled = 0;
    public int wave = 1;

    AudioSource audioSrc;

    void Start() {
        audioSrc = GetComponent<AudioSource>();
        Time.timeScale = 0;
    }

    void Update() {
        zombieKilledGameOver.text = "ZOMBIE KILLED: " + zombieKilled.ToString();
        zombieCountUi.text = zombieKilled.ToString();
        waveCountUi.text = wave.ToString();

        if(Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }
    }

    public void PlaySound(string sfx) {
        if(sfx == "Hit") {
            audioSrc.clip = hitSFX;
            audioSrc.Play();
        }
        else if(sfx == "Heal") {
            audioSrc.clip = healSFX;
            audioSrc.Play();
        }
        else if(sfx == "Shoot") {
            audioSrc.clip = shootSFX;
            audioSrc.Play();
        }
    }

    public void UnstartleAllZombie() {
        foreach(GameObject zombie in triggeredZombie) {
            ZombieStats stats = zombie.GetComponent<ZombieStats>();
            stats.Unstartle();
        }
    }
    public int RegisterStartledZombie(GameObject zombie) {
        triggeredZombie.Add(zombie);
        return triggeredZombie.Count - 1;
    }
    public void UnregisterZombie(GameObject zombie) {
        triggeredZombie.Remove(zombie);
    }

    public void PauseGame() {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame() {
        startPanel.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver() {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void PlayAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
