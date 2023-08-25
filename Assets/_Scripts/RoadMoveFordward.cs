using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMoveFordward : MonoBehaviour
{
    //[SerializeField] private float speed = 1f;
    private MeshRenderer myMR;
    private GameManager myGM;

    // Start is called before the first frame update
    void Start()
    {
        myMR = GetComponent<MeshRenderer>();
        myGM = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = myMR.material.mainTextureOffset;
        offset.y -= myGM.GetGameSpeed()/10f * Time.deltaTime;
        myMR.material.mainTextureOffset = offset;
    }
}
