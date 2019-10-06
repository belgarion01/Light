using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject MainCam;
    public GameObject CommandeCam;
    public GameObject CreditCam;
    private GameObject currentCam;

    public void MoveToCam(GameObject cam) {
        cam.SetActive(true);
        currentCam.SetActive(false);
        currentCam = cam;
    }

    public void MoveToCredit() {
        MoveToCam(CreditCam);
    }

    public void MoveToMain() {
        MoveToCam(MainCam);
    }

    public void MoveToCommande() {
        MoveToCam(CommandeCam);
    }

    public void Launch() {
        SceneManager.LoadScene(SceneManager.GetSceneByName("SceneFinale").buildIndex);
    }

    public void Quit() {
        Application.Quit();
    }
}
