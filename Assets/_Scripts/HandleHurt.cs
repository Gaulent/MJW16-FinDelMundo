using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HandleHurt : MonoBehaviour
{
    [SerializeField] private float maxHurtTimer = 0.2f;
    private float hurtTimer = 0f;
    private PostProcessVolume myVolume;
    
    // Start is called before the first frame update
    void Start()
    {
        myVolume = GetComponent<PostProcessVolume>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hurtTimer <= 0)
        {
            myVolume.enabled = false;
        }
        else
        {
            myVolume.enabled = true;
        }

        hurtTimer -= Time.deltaTime;
    }

    public void EnableVignette()
    {
        hurtTimer = maxHurtTimer;
    }
}
