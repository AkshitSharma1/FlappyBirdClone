using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GC : MonoBehaviour
{
  static   GameObject Instance;
    public GameObject pillar,spawnPos,Player;
    [SerializeField] AudioClip vibration, flap;
    CanvasGroup cr;
    AudioSource ad;
    TextMeshProUGUI SCORE;
    CanvasGroup gameover;

    // Start is called before the first frame update
    void Awake()
    {
        
        if (Instance)
        {

            DestroyImmediate(gameObject); // destroy these
                                         

        }
        else
        {
          
            SceneManager.sceneLoaded += SceneLoaded;
            DontDestroyOnLoad(gameObject);
            Instance = gameObject;

        }
    }
    bool isGameRunning = false;
    private void SceneLoaded(Scene arg0, LoadSceneMode arg1)

    {

        reloadonce = false;
        startgamecalled = false;
        iscalledonce = false;
        gameover = GameObject.FindGameObjectWithTag("OVER").GetComponent<CanvasGroup>();
        SCORE = GameObject.FindGameObjectWithTag("SCORE").GetComponent<TextMeshProUGUI>();
        isGameRunning = true;

        ad= gameObject.GetComponent<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player");
        pillar = GameObject.FindGameObjectWithTag("Pillar");
        spawnPos = GameObject.FindGameObjectWithTag("spawnPos");
        SCORE.text = 0.ToString();
        cr = GameObject.FindGameObjectWithTag("START").GetComponent<CanvasGroup>();
        LeanTween.alphaCanvas(SCORE.gameObject.GetComponent<CanvasGroup>(), 0, 1f);
       // SpawnPillar();
        LeanTween.alphaCanvas(cr, 1, 0.001F);

    }

   public void SpawnPillar()
    {
        if (isGameRunning) {
            GameObject pn=Instantiate(pillar, spawnPos.transform,false);
            pn.transform.localPosition = Vector3.right * 0;
            pn.transform.position += Vector3.up * UnityEngine.Random.RandomRange(-1, 2);
            Destroy(pn, 10);
           //   Invoke("SpawnPillar", 2f);
                }
    }
    public void IncreaseScore()
    {
        ad.PlayOneShot(flap);
        int score = int.Parse(SCORE.text);
        score++;
        SCORE.text = score.ToString();
    }
    private void Start()
    {
        
    }
    // Update is called once per frame
    public int HighScore()
    {
     //   Debug.LogError("HIGH SCORE IS :" + PlayerPrefs.GetInt("HIGHSCORE"));
        if (PlayerPrefs.HasKey("HIGHSCORE")) { return PlayerPrefs.GetInt("HIGHSCORE"); } else { return 0; }

    }
    void Update()
    {
        
    }
    bool startgamecalled = false;
   public void StartGame()
    {
        if(startgamecalled==true) { return; }
        startgamecalled = true;
        Player = GameObject.FindGameObjectWithTag("Player");
        LeanTween.alphaCanvas(SCORE.gameObject.GetComponent<CanvasGroup>(), 1, 1f);
        Debug.Log("STARTGAME CALLED");
        LeanTween.alphaCanvas(cr, 0, 1f);
        // StartGame_Proceed();
        Player.GetComponent<SpriteRenderer>().enabled = true;
        Invoke("StartGame_Proceed", 1f);
    }

     void StartGame_Proceed()
    {
        Debug.Log("sTARTGAME PROCEED HAS BEEN CALLED");
        GameObject[] p = GameObject.FindGameObjectsWithTag("Pillar");
        foreach (GameObject i in p)
        {
            i.GetComponent<Pillar>().isAllowed = true;
        }
  
            Player.GetComponent<ICanFly>().jumpAllowed = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().isKinematic = false;
 

    }

    bool iscalledonce = false;
    public void GameOver()
    {
       // Reload();
        if (iscalledonce) { return; }

        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().isKinematic = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>().enabled=false;
        gameover.gameObject.GetComponent<Canvas>().sortingLayerID = 10;
   
        iscalledonce = true;
        isGameRunning = false;
        GameObject[] p = GameObject.FindGameObjectsWithTag("Pillar");
        ad.PlayOneShot(vibration);
        Debug.Log("Game over called");
        int score = int.Parse(SCORE.text);
        foreach (GameObject i in p)
        {
            i.GetComponent<Pillar>().isAllowed = false;
        }
        if(PlayerPrefs.HasKey("HIGHSCORE"))
        {
            int cloud = PlayerPrefs.GetInt("HIGHSCORE");
           
            if(score>cloud)
            {
                
                PlayerPrefs.SetInt("HIGHSCORE", score);
            }
        } else
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);
        }
        PlayerPrefs.Save();
        LeanTween.alphaCanvas(SCORE.gameObject.GetComponent<CanvasGroup>(), 0, 1f);
     //   LeanTween.al
     

        FindObjectOfType<CameraShake>().shakeDuration = 0.3f;
        LeanTween.alphaCanvas(gameover, 1, 0.6f);
        Invoke("Reload",0.8f);
    }
    bool reloadonce = false;
    void Reload()
    {
        if(reloadonce==true) { return; }
        reloadonce = true;
        Debug.Log("RELOADING SCENE");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Restart()
    {
        
    }
   
}
