using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameManager : MonoBehaviour
{
    public static GlobalGameManager Instance;
    [HideInInspector]
    public Vector3 RespawnPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);          
        }

        else {
            Destroy(gameObject);
        }
    }

    public void SetActiveCheckpoint(Vector3 checkpoint) {
        RespawnPosition = checkpoint;
    }

    public Vector3 GetActiveCheckpoint()
    {
        if (RespawnPosition != Vector3.zero) return RespawnPosition;
        else return Vector3.zero;
    }
}
