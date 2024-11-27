using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction 
{
    // get keyboard input and move bow
    public void Move(float offsetX, float offsetY);
    public void Shoot();
    public int GetScore();
    public int GetTargetScore();
    // get left arrow num
    public int GetResidumNum();
    public void Restart();
    public void StartGame();
    // when u click right enter this mode
    public void AimMode();
    public bool GameOver();

    public void SwitchCamera();
}
