using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ISpawnManager
{
    public UnityEvent OnWaveEndedSignal { get; }
    void SetSpawnStatus(bool status);
    void SetEnemiesWaves(List<GameObject> test, int values);
}