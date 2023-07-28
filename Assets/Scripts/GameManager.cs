using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    bool gameStatus = false;
    int dopamina = 100;

    public void Update()
    {
        if (dopamina < 0)
        {
            GameOver();
        }
    }

    public void StartGame()
    {
        dopamina = 100;
        InternalGameStatus(true);
    }

    public void GameOver()
    {
        InternalGameStatus(false);
    }

    private void InternalGameStatus(bool status)
    {
        gameStatus = status;
        GameObject.Find("Player").GetComponent<IPlayerController>().EnableMovement(status);
        GameObject.Find("SpawnManager").GetComponent<ISpawnManager>().SetSpawnStatus(status);
    }

    public bool GetGameStatus()
    {
        return gameStatus;
    }

}
