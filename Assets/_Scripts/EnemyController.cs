using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 0.5f;
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
        if(mySR.flipX)
            transform.position += walkSpeed * Time.deltaTime * Vector3.right;
        else
            transform.position += walkSpeed * Time.deltaTime * Vector3.left;
        
        transform.position += GameManager.Game.GameSpeed * Time.deltaTime * Vector3.back;
    }
}
