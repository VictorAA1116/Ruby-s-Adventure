using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollectible : MonoBehaviour
{
    public AudioClip starClip;
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();
        
        if (controller != null)
        {
            if (controller.starCollected == false)
            {
                controller.PowerUp();
                Destroy(gameObject);
                controller.PlaySound(starClip);
            }
        } 
    }
}
