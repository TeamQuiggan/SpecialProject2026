using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<Ghosts> GhostS;
    public Pacman PacMan;
    public Transform Pellets;
    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 3;
    public int ghostMulti { get; private set; } = 1;
    public Movement movement;
    public AudioSource audioSource;
    public List<AudioClip> audio;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;
    public bool Level1 = true;
    public bool Level2 = false;
    public bool Level3 = false;
    public GameObject[] Level1Stuff;
    public GameObject[] Level2Stuff;
    public GameObject[] Level3Stuff;
    public GameObject DarkenBackgr;
    public bool Transitioning = false;
    public Transform PelletLevel2;
    public SceneTransition transition;
    public GameObject HighScoreBoard;
    public Text First, Second, Third, Fourth, Fifth, Sixth;
    private List<float> Records = new List<float>(6);
    public Transform PelletLevel3;
    public GameObject SettingPannel;
    void Start()
    {
        Level1 = true;
        Level2 = false;
        Level3 = false;
        NewGame(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (this.lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame && Level1)
        {
            Level1 = false;
            Level1to2();
            this.PacMan.gameObject.SetActive(false);
            this.movement.StartingPos = new Vector3(0.5f, -6.5f, -5f);
            StartCoroutine(transition.Transitioning());
            Invoke(nameof(NewRound), 3f);
            Level2 = true;
        }
        else if (Keyboard.current.spaceKey.wasPressedThisFrame && Level2)
        {   
            Level2 = false;
            Level2to3();
            this.PacMan.gameObject.SetActive(false);
            this.movement.StartingPos = new Vector3(0.5f, -7.5f, -5f);
            StartCoroutine(transition.Transitioning());
            Invoke(nameof(NewRound), 3f);
            Level3 = true;
        }
        else if (Keyboard.current.spaceKey.wasPressedThisFrame && Level3)
        {
            Level3 = false;
            Level3to1();
            this.PacMan.gameObject.SetActive(false);
            this.movement.StartingPos = new Vector3(0.5f, -7.5f, -5f);
            StartCoroutine(transition.Transitioning());
            Invoke(nameof(NewRound), 3f);
            Level1 = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingPannel.SetActive(true);
            // Add your pause or menu close logic here
        }
        //if (Level1)
        //{
        //    Level1 = false;
        //    Level2 = true;
        //}
        //else if (Level2)
        //{
        //    Level2 = false;
        //    Level1 = true;
        //}
    }
    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();

    }
    private void NewRound()
    {   
        DarkenBackgr.SetActive(false);
        gameOverText.enabled = false;
        SettingPannel.SetActive(false);
        HighScoreBoard.SetActive(false);
        if (Level1)
        {
            foreach (Transform pellet in this.Pellets)
            {
                pellet.gameObject.SetActive(true);
            }
        }
        else if (Level2)
        {
            foreach (Transform pellet in this.PelletLevel2)
            {
                pellet.gameObject.SetActive(true);
            }
        }
        else if (Level3)
        {
            foreach (Transform pellet in this.PelletLevel3)
            {
                pellet.gameObject.SetActive(true);
            }
        }
        ResetState();
    }
    private void GameOver()
    {
        gameOverText.enabled = true;
        DarkenBackgr.SetActive(true);
        for (int i = 0; i < this.GhostS.Count; i++)
        {
            if (this.GhostS[i].gameObject.activeSelf)
            {
                this.GhostS[i].gameObject.SetActive(false);
            }
            else
            {
                continue;
            }
            //this.GhostS[i].gameObject.SetActive(false);
        }
        this.PacMan.gameObject.SetActive(false);
        HighScore();
    }
    private void ResetState()
    {   
        ResetGhostMulti();
        movement.ResetState();
        //for (int i = 0; i < this.GhostS.Count; i++)
        //{

        //    //if (this.GhostS[i].gameObject.activeSelf)
        //    //{
        //    //    this.GhostS[i].ResetSttate();
        //    //}
        //    //else
        //    //{
        //    //    continue;
        //    //}
        //    //this.GhostS[i].ResetSttate();

        //}
        if (Level1)
        {
            for (int i = 0; i<3; i++)
            {
                this.GhostS[i].ResetSttate();
            }
        }
        else if (Level2)
        {
            for (int i = 3; i < 7; i++)
            {
                this.GhostS[i].ResetSttate();
            }
        }
        else if (Level3)
        {
            for (int i = 7; i < GhostS.Count; i++)
            {
                this.GhostS[i].ResetSttate();
            }
        }
        this.PacMan.ResetState();
        
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');
    }
    public void GhostEaten(Ghosts ghost)
    {
        SetScore(this.score + ghost.points * ghostMulti);
        this.ghostMulti++;
    }
    public void PacmanEaten()
    {   
        this.PacMan.gameObject.SetActive(false );
        SetLives(this.lives -1);
        if (this.lives > 0)
        {   
            Invoke(nameof(ResetState), 3f);
            //ResetState();
        }
        else
        {
            GameOver();
        }
    }
    public void PelletEaten(Pellet pell)
    {
        pell.gameObject.SetActive(false);
        audioSource.Play();
        SetScore(this.score + pell.points);
        if (!HasRemainingPellets())
        {   
            if (Level1)
            {
                Level1 = false;
                Level1to2();
                this.PacMan.gameObject.SetActive(false);
                this.movement.StartingPos = new Vector3(0.5f, -6.5f, -5f);
                StartCoroutine(transition.Transitioning());
                Invoke(nameof(NewRound), 3f);
                Level2 = true;
            }
            else if (Level2)
            {
                Level2 = false;
                Level2to3();
                this.PacMan.gameObject.SetActive(false);
                this.movement.StartingPos = new Vector3(0.5f, -7.5f, -5f);
                StartCoroutine(transition.Transitioning());
                Invoke(nameof(NewRound), 3f);
                Level3 = true;
            }
            else if (Level3)
            {
                Level3 = false;
                Level3to1();
                this.PacMan.gameObject.SetActive(false);
                this.movement.StartingPos = new Vector3(0.5f, -7.5f, -5f);
                StartCoroutine(transition.Transitioning());
                Invoke(nameof(NewRound), 3f);
                Level1 = true;
            }

        }

    }
    public void PowerPelletEaten(PowerPellet pell)
    {   
        for (int i = 0; i<GhostS.Count;i++)
        {

            if (this.GhostS[i].gameObject.activeSelf)
            {   
                if (this.GhostS[i].gameObject.tag == "Boulder")
                {
                    continue;
                }
                else
                {
                    this.GhostS[i].frighten.Enable(pell.dura);
                }

            }
            else
            {
                continue;
            }
            //this.GhostS[i].frighten.Enable(pell.dura);
        }
        PelletEaten(pell);
        CancelInvoke(nameof(ResetGhostMulti));
        Invoke(nameof(ResetGhostMulti), pell.dura);
    }
    private bool HasRemainingPellets()
    {   


        if (Level1)
        {
            foreach (Transform pellet in this.Pellets)
            {
                if (pellet.gameObject.activeSelf)
                {
                    return true;
                }
            }

        }
        else if (Level2)
        {

            foreach (Transform pellet in this.PelletLevel2)
            {
                if (pellet.gameObject.activeSelf)
                {
                    return true;
                }
            }
        }
        else if (Level3)
        {
            foreach (Transform pellet in this.PelletLevel3)
            {
                if (pellet.gameObject.activeSelf)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private void ResetGhostMulti()
    {
        this.ghostMulti = 1;
    }
    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
    }
    private void Level1to2()
    {
        //Level1 = false;
        Camera.main.orthographicSize = 15f;
        foreach (GameObject obj in Level1Stuff)
        {
            obj.SetActive(false);

        }
        foreach (GameObject obj in Level2Stuff)
        {
            obj.SetActive(true);
        }
        //Level2 = true;
    }
    private void Level2to3()
    {
        //Level2 = false;
        Camera.main.orthographicSize = 18f;
        foreach (GameObject obj in Level3Stuff)
        {
            obj.SetActive(true);

        }
        foreach (GameObject obj in Level2Stuff)
        {
            obj.SetActive(false);
        }
        //Level1 = true;
    }
    private void Level3to1()
    {
        //Level2 = false;
        Camera.main.orthographicSize = 15f;
        foreach (GameObject obj in Level1Stuff)
        {
            obj.SetActive(true);

        }
        foreach (GameObject obj in Level3Stuff)
        {
            obj.SetActive(false);
        }
        //Level1 = true;
    }
    public void HighScore()
    {
        Records.Add(this.score);
        Records.Sort((a, b) => b.CompareTo(a)); // sort descending
        if (Records.Count > 6) Records.RemoveAt(6); // keep top 6 only

        Text[] slots = { First, Second, Third, Fourth, Fifth, Sixth };
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < Records.Count)
                slots[i].text = Records[i].ToString("F2");
            else
                slots[i].text = "--";
        }
        HighScoreBoard.SetActive(true);
    }
    public void closeOptions()
    {
       SettingPannel.SetActive(false);
    }
}
