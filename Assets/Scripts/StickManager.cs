using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManager : MonoBehaviour,IStickManager
{
    bool activated = false;
    TurnUpComponent turnUpComponent;
    MovieLoad movieLoadComponent;

    // Start is called before the first frame update
    void Start()
    {
        turnUpComponent = this.gameObject.GetComponent<TurnUpComponent>();
        movieLoadComponent = this.gameObject.GetComponentInChildren<MovieLoad>();
    }

    // Update is called once per frame
    void Update()
    {
     if (Input.GetButtonDown("Fire1"))
     {
        SetActivation(!activated);
     }   
    }

    public void SetActivation(bool status)
    {
        activated = status;
        //this.gameObject.SetActive(status);

        if (status)
        {
            turnUpComponent.OpeningMobile();
            movieLoadComponent.TriggerVideo();
            return;
        }
        Debug.Log("I am here");
        turnUpComponent.ClosingMobile();
        movieLoadComponent.StopVideo();
        return;
    }
}
