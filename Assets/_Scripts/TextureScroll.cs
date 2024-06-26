using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroll : MonoBehaviour
{
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
        offset.y -= GameManager.Game.GameSpeed/10f * Time.deltaTime;
        myMR.material.mainTextureOffset = offset;
    }
}
