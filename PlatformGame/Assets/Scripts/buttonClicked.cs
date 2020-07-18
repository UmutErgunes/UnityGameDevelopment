using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonClicked : MonoBehaviour
{
    #region Variables
    //Character
    public Rigidbody2D rigid;
    public GameObject OurBird;
    const float tapForce = 160;
    const float tilthSmooth = 5;
    Quaternion downRotation;
    Quaternion forwardRotation;
    Vector3 startPosition = new Vector3(-3, 0, 0);

    //Moving Objects
    public GameObject movingCloud;
    public GameObject fixCloud;
    public GameObject movingFire;
    public GameObject movingBirds;
    Quaternion moveCloud;
    public GameObject groundPart1;
    public GameObject groundPart2;

    //Views
    public Button startButton;
    public Button settingButton;
    public Button backButton;
    public Button replayButton;
    public Button mainMenuButton;
    public GameObject StartPage;
    public GameObject SettingsPage;
    public GameObject GameOverPage;
    public GameObject CloudCountDownPage;
    public Text DarkCloudText;
    bool GameStarted;

    //Song Variable
    public float setValue;
    public AudioSource song;
    public Slider sldr;
    public Text songValue;

    //Camera
    public GameObject Camera;

    //Other Variables
    float TimeLeft = 7;
    float testim = 10;

    //Objects Created OR Not
    bool birdsCreated;
    bool fireCreated;
    bool cloudCreated;

    enum Pages { Game, StartPage, SettingsPage}

    #endregion Variables

    #region Start
    void Start()
    {
        //Set Character position at start
        rigid.transform.position = startPosition;
        Time.timeScale = 0;

        rigid = GetComponent<Rigidbody2D>();
        //Button btn = startButton.GetComponent<Button>();
        StartPage.SetActive(true);
        SettingsPage.SetActive(false);
        GameOverPage.SetActive(false);
        CloudCountDownPage.SetActive(false);

        rigid = GetComponent<Rigidbody2D>();

        ResetPositions();

        //Set Down & Forward Rotations
        downRotation = Quaternion.Euler(0, 0, -15);
        forwardRotation = Quaternion.Euler(0, 0, 30);
        moveCloud = Quaternion.Euler(0, 0, -50);

        //StartCoroutine(everyfive());
    }

    #endregion Start

    #region Update
    void Update()
    {
        //Random Birds
        int rndBirds = UnityEngine.Random.Range(1, 101);
        LeftClickMouse();
        SongToSlider();
        CharachterMove();
        MovingCloud();
        MovingFire();
        MovingBirds();
        MovingGround();



    }
    #endregion Update

    #region Methods
    IEnumerator everyfive()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Wait IS OVER");
    }

    void CharachterMove()
    {
        if (GameStarted == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rigid.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
                transform.rotation = forwardRotation;
                
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tilthSmooth * Time.deltaTime);
        }
    }

    void LeftClickMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Mouse Left Clicked");
            startButton.onClick.AddListener(() => StartGame());
            settingButton.onClick.AddListener(() => Settings());
            backButton.onClick.AddListener(() => GoBack());
            replayButton.onClick.AddListener(() => RestartGame());
            mainMenuButton.onClick.AddListener(() => GoToStartMenu());
        }
    }

    void StartGame()
    {
        SettingsPage.SetActive(false);
        StartPage.SetActive(false);
        GameOverPage.SetActive(false);
        CloudCountDownPage.SetActive(false);

        rigid.gravityScale = 1;
        Time.timeScale = 1;

        rigid.transform.position = startPosition;
        rigid.velocity = Vector2.zero;
        GameStarted = true;

        
    }
    void StartMenu()
    {
        StartPage.SetActive(true);
        SettingsPage.SetActive(false);
        GameOverPage.SetActive(false);
        CloudCountDownPage.SetActive(false);
        Debug.Log("In Menu");
        GameStarted = false;
    }

    void Settings()
    {
        SettingsPage.SetActive(true);
        StartPage.SetActive(false);
        CloudCountDownPage.SetActive(false);
        Debug.Log("In Settings");
        GameStarted = false;
        //song.Stop();
    }

    void GoBack()
    {
        StartMenu();
    }

    void SongToSlider()
    {
        //een if!
        song.volume = sldr.value;
        
        songValue.text = (sldr.value*100).ToString("F0") + " %";
    }

    void RestartGame()
    {
        StartGame();
        //reset player position and force 
        rigid.transform.position = startPosition;
        rigid.velocity = Vector2.zero;
        ResetPositions();
    }

    void GoToStartMenu()
    {
        SettingsPage.SetActive(false);
        GameOverPage.SetActive(false);
        StartPage.SetActive(true);
        GameStarted = false;
    }

    void MovingCloud()
    {
        movingCloud.transform.position += new Vector3(-0.5f * Time.deltaTime, 0, 0);
        //movingCloud.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 15), Quaternion.Euler(0, 0, -15), Time.deltaTime);
        if (movingCloud.transform.position.x < -10)
        {
            //Destroy(movingCloud);
            movingCloud.transform.position = new Vector3(12, 5, 0);
        }
    }

    void MovingFire()
    {
        movingFire.transform.position += new Vector3(-2 * Time.deltaTime, 0, 0);
        if (movingFire.transform.position.x < -10)
        {
            //Destroy(movingCloud);
            movingFire.transform.position = new Vector3(12, -3.5f, 0);
        }
    }

    void MovingBirds()
    {
        movingBirds.transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
        if (movingBirds.transform.position.x > 12)
        {
            
            movingBirds.transform.position = new Vector3(-12, 2f, 0);
        }
    }
    
    void ResetPositions()
    {
        movingCloud.transform.position = new Vector3(10, 5, 0);
        movingFire.transform.position = new Vector3(10, -3.5f, 0);
        movingBirds.transform.position = new Vector3(-12, 2, 0);
    }


    void MovingGround()
    {
        if (GameStarted == true)
        {
            groundPart1.transform.position += new Vector3(-2.45f * Time.deltaTime, 0, 0);
            groundPart2.transform.position += new Vector3(-2.45f * Time.deltaTime, 0, 0);
            if (groundPart1.transform.position.x < -18)
            {
                Debug.Log("OutsideNOW Part 1");
                groundPart1.transform.position = new Vector3(17, 1.62f, 10);
            }
            if (groundPart2.transform.position.x < -17)
            {
                Debug.Log("OutsideNOW Part 2");
                groundPart2.transform.position = new Vector3(17, 1.62f, 10);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if player enter in SKY-BOX:
        if (collision.tag == "Sky" || collision.tag == "Fire" || collision.tag == "Ground")
        {
            SettingsPage.SetActive(false);
            StartPage.SetActive(false);
            GameOverPage.SetActive(true);
            GameStarted = false;
            Time.timeScale = 0;
            
        }
        if (collision.tag == "DarkCloud")
        {
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "DarkCloud")
        {
            
            TimeLeft -= Time.deltaTime ;
            CloudCountDownPage.SetActive(true);
            DarkCloudText.text = "Dark cloud will kill you \n in " + Math.Round(TimeLeft).ToString();
            if (TimeLeft <0)
            {
                SettingsPage.SetActive(false);
                StartPage.SetActive(false);
                CloudCountDownPage.SetActive(false);
                GameOverPage.SetActive(true);
                GameStarted = false;
                Time.timeScale = 0;
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "DarkCloud")
        {
            CloudCountDownPage.SetActive(false);
            TimeLeft = 7;
        }
    }

    #endregion Methods




    #region ExtraCodes
    //10 Seconds Bool 
    /*    
    testim -= Time.deltaTime;
    Debug.Log(Math.Round(testim));
    if (testim< 0)
    {
        testim = 10;
    }
    */
    #endregion ExtraCodes
}
