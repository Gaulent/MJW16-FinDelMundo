using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetCloser : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position += GameManager.Game.GameSpeed * Time.deltaTime * Vector3.back;
    }
}
