using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerTrigger : MonoBehaviour
{
  public GameObject TimerText;

  
  [SerializeField] GameObject Canvas;
  Countdown countdown;
 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RubyController"))
        {
            TimerText.SetActive(false);
        }
    }
 
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RubyController"))
        {
            TimerText.SetActive(true);
            TimerText.GetComponent<Countdown>().enabled = true;
        }
    }
}
