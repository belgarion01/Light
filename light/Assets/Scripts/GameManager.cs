using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Checkpoint spawn;
    private bool dead = false;
    private bool pause = false;

    public GameObject DeathCanvas;
    public GameObject PauseCanvas;

    private void Start()
    {
        if (GlobalGameManager.Instance.GetActiveCheckpoint() == Vector3.zero)
        {
            GlobalGameManager.Instance.SetActiveCheckpoint(spawn.position);
        }

        player.transform.position = GlobalGameManager.Instance.GetActiveCheckpoint();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            ReloadToCheckpoint();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            ShowDeathMenu(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }

    public void ShowDeathMenu(bool value) {
        DeathCanvas.SetActive(value);
        Pause(value);
        player.SetActive(false);
        dead = true;
    }

    public void PauseMenu() {
        if (dead) return;
        PauseCanvas.SetActive(!pause);
        Pause(!pause);
        pause = !pause;
    }

    public void ResetCheckpoint()
    {
        GlobalGameManager.Instance.SetActiveCheckpoint(spawn.position);
    }

    public void ReloadToCheckpoint() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReloadScene() {
        ResetCheckpoint();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause(bool value) {
        if (value) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }
}
