using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public PlayerController pController;
    public void PlayWalkSound() => pController.OnFoot.Invoke();
    public void PlayJumpSound() => pController.OnJump.Invoke();
}
