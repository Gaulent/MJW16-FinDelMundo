using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private bool backgroundMode;
    private SpriteRenderer mySR;

    // Start is called before the first frame update
    void Start()
    {
        mySR = GetComponent<SpriteRenderer>();
        if (transform.position.x < 0)
            mySR.flipX = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(backgroundMode)
        {
            transform.position += speed * Time.deltaTime * Vector3.back;
            return;
        }

        if(mySR.flipX)
            transform.position += speed * Time.deltaTime * Vector3.right;
        else
            transform.position += speed * Time.deltaTime * Vector3.left;
    }
}