using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMoveFordward : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Material myMaterial;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = myMaterial.mainTextureOffset;
        offset.y += speed * Time.deltaTime;
        myMaterial.mainTextureOffset = offset;
    }
}
