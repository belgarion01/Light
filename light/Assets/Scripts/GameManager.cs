using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Checkpoint spawn;

    public GameObject DeathCanvas;

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
    }

    public void ShowDeathMenu(bool value) {
        DeathCanvas.SetActive(value);
        Pause(value);
        player.SetActive(false);
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
