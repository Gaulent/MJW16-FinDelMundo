using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;

public class PlayerDamage : MonoBehaviour
{
    private int hitPoints = 0;
    [SerializeField] private AudioClip getHurtAudioClip;
    private SpriteRenderer damageSpriteRenderer;
    [SerializeField] private Sprite[] damageSprites;
    private PostProcessVolume myVolume;
    

    private void Start()
    {
        damageSpriteRenderer = GameObject.FindWithTag("DamageSprite").GetComponent<SpriteRenderer>();
        myVolume = GetComponentInChildren<PostProcessVolume>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Hazard"))
            GetHurt();
    }

    void GetHurt()
    {
        AudioSource.PlayClipAtPoint(getHurtAudioClip, transform.position); // TODO: Buscar otra forma de hacerlo
        StartCoroutine(nameof(HurtAnimation));

        hitPoints++;
        damageSpriteRenderer.sprite = damageSprites[hitPoints];
        
        if (hitPoints >= damageSprites.Length-1)
            GameManager.Game.GameOver();
    }
    
    IEnumerator HurtAnimation()
    {
        myVolume.enabled = true;
        yield return new WaitForSeconds(0.15f);
        myVolume.enabled = false;
    }
}
