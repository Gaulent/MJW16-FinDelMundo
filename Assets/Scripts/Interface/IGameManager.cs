public interface IGameManager
{
    void StartGame();
    bool GetGameStatus();
    float GetGameSpeed();
    bool CanLowerHand();
}