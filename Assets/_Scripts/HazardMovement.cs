using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardMovement : MonoBehaviour
{
    //[SerializeField] private float speed = 1f;
    private GameManager myGM;

    // Start is called before the first frame update
    void Start()
    {
        myGM = FindObjectOfType<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Delete if it's outside of the screen
        if (transform.position.z < -2)
        {
            Destroy(gameObject);
        }

        transform.position += myGM.GetGameSpeed() * Time.deltaTime * Vector3.back;
    }
}
