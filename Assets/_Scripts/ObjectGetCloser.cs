using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGetCloser : MonoBehaviour
{
    private GameManager myGM;

    // Start is called before the first frame update
    void Start()
    {
        myGM = FindObjectOfType<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += myGM.GetGameSpeed() * Time.deltaTime * Vector3.back;
    }
}
