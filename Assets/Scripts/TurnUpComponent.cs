// #define DEBUG_ROTATOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnUpComponent : MonoBehaviour
{

    private Quaternion OriginalRotator;
    [SerializeField] bool isRotanding = false;
    [SerializeField] private float Offset;
    [SerializeField] private float rotatorSpeed = 5f;
    [SerializeField] private float openAngle = -0.5f;
    [SerializeField] private float refreshRate = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        //OpeningMobile();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OpeningMobile()
    {
            StartCoroutine(RotatorEngine(openAngle));
        
    }

    public void ClosingMobile()
    {
        StartCoroutine(RotatorEngine(-openAngle));
    }

    IEnumerator RotatorEngine( float targetRotator )
    {
        if (isRotanding){yield return null;}
        isRotanding = true;
        while(
                (targetRotator > 0 && transform.rotation.x <= (targetRotator + Offset) )
                || ( targetRotator < 0 && transform.rotation.x >= (targetRotator - Offset ))
            )
        {

            #if DEBUG_ROTATOR
            Debug.Log("Debug Rotator:"+ transform.rotation.x);
            #endif

            yield return new WaitForSeconds(refreshRate);
            transform.rotation = 
                Quaternion.Lerp(transform.rotation, 
                    new Quaternion(
                            targetRotator ,
                            transform.rotation.y,
                            transform.rotation.z, 
                            transform.rotation.w),
                            rotatorSpeed * Time.deltaTime
                        );
        }
        isRotanding = false;
    }


}
