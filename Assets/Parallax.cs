using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Vector3 playerDefaultPosition;
    private Vector3 backgroundDefaultPosition;
    private GameObject player;
    private RectTransform picture;
    [SerializeField] private float horizontalMovement = 5f;
    [SerializeField] private float verticalMovement = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerDefaultPosition = player.transform.position;
        picture = GetComponent<RectTransform>();
        backgroundDefaultPosition = picture.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = (player.transform.position - playerDefaultPosition);
        Vector3 moveAmount = new Vector3(-delta.x*horizontalMovement, delta.y*-verticalMovement, 0);
        Debug.Log(moveAmount);
        picture.position = backgroundDefaultPosition + moveAmount;
    }
}
