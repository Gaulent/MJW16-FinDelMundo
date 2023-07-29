using UnityEngine.Events;

public interface IPlayerController{
  public UnityEvent OnGameOverSignal { get; } 
  //void EnableMovement(bool movement);
  bool GetIsJumping();
  bool GetIsPhoneDown();
}