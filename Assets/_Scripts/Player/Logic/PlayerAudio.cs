using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerAudio
{
    void Footsteps(PlayerData playerData, IPlayerInput playerInput);
}

public class PlayerAudio : MonoBehaviour, IPlayerAudio
{
    private float timer;
    public void Footsteps(PlayerData playerData, IPlayerInput playerInput)
    {
        if(playerInput.UnsmoothedPlayerMovementInput != Vector2.zero)
        {
            if(timer <= 0)
            {
                int i = Random.Range(0, playerData.FootstepClips.Length);
                SoundManager.Instance.Play2DSoundEffect(playerData.FootstepClips[i], playerData.FootstepsVolume);

                if (playerInput.playerRunInput)  timer = playerData.FootstepsRunningTime;
                else timer = playerData.FootstepWalkingTime;
            }
            timer -= Time.deltaTime;
        }
        else timer = 0;
    }
}