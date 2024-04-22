using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifTrigger : MonoBehaviour
{
    public GameObject notif;
 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RubyController"))
        {
            notif.SetActive(false);
        }
    }
 
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RubyController"))
        {
            notif.SetActive(true);
        }
    }
 
}
