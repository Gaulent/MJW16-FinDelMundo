using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    int dotamina = 100;

    public void StartGame()
    {
        dotamina = 100;
    }

}
