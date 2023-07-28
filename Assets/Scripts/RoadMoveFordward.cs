using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMoveFordward : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private MeshRenderer myMR;

    // Start is called before the first frame update
    void Start()
    {
        myMR = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = myMR.material.mainTextureOffset;
        offset.y -= speed * Time.deltaTime;
        myMR.material.mainTextureOffset = offset;
    }
}
