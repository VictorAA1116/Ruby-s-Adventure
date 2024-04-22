using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
   public AudioClip loseSound;
   AudioSource audioSource;
   
    RubyController rubyController;
    [SerializeField] GameObject Ruby;
    
    TimerTrigger timerTrigger;
    [SerializeField] GameObject Cobrot;

    public GameObject gameOver;
    TextMeshProUGUI gameOverText;
    bool gameOverBool = false;
    
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    // Start is called before the first frame update
    void Awake()
    {
        rubyController = Ruby.GetComponent<RubyController>();
        gameOverText = gameOver.GetComponent<TextMeshProUGUI>();
        timerTrigger = Cobrot.GetComponent<TimerTrigger>();
       
    }

    void Start()
    {
       this.GetComponent<Countdown>().enabled=false;
       audioSource = GetComponent<AudioSource>();
        
    }
    // Update is called once per frame
    void Update()
    {
       

       if (Input.GetKeyDown(KeyCode.R))
        {
            if (gameOverBool == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
       
       if(remainingTime > 0)
       {
        remainingTime -= Time.deltaTime;
       }
       else if(remainingTime < 0)
       {

        remainingTime = 0;
        gameOverBool = true;
        gameOver.SetActive(true);
        gameOverText.text = "You Lost! Press R to restart!";
        timerText.color = Color.red; 
        rubyController.speed = 0;
        audioSource.PlayOneShot(loseSound);

        
    

       }
       
       int minutes = Mathf.FloorToInt(remainingTime / 60);
       int seconds = Mathf.FloorToInt(remainingTime % 60);
       timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
