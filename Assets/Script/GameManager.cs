using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

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
    public GameObject[] Level1Stuff;
    public GameObject[] Level2Stuff;
    public GameObject DarkenBackgr;
    void Start()
    {
        Level1 = true;
        Level2 = false;
        NewGame(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (this.lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }
        if (Input.GetKeyDown(KeyCode.Return) && Level1)
        {
            Level1to2();
            this.PacMan.gameObject.SetActive(false);
            this.PacMan.gameObject.transform.position = new Vector3(0.5f, -7.5f, -5f);
            Invoke(nameof(NewRound), 3f);
        }
        if (Input.GetKeyDown(KeyCode.Return) && Level2)
        {
            Level2to1();
            this.PacMan.gameObject.SetActive(false);
            this.PacMan.gameObject.transform.position = new Vector3(0.5f, -6.5f, -5f);
            Invoke(nameof(NewRound), 3f);
        }
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
        foreach (Transform pellet in this.Pellets)
        {
            pellet.gameObject.SetActive(true);
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
    }
    private void ResetState()
    {   
        ResetGhostMulti();
        movement.ResetState();
        for (int i = 0; i < this.GhostS.Count; i++)
        {

            if (this.GhostS[i].gameObject.activeSelf)
            {
                this.GhostS[i].ResetSttate();
            }
            else
            {
                continue;
            }
            //this.GhostS[i].ResetSttate();
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
                Level1to2();
                this.PacMan.gameObject.SetActive(false);

                Invoke(nameof(NewRound), 3f);
            }
            else if (Level2)
            {
                Level2to1();
                this.PacMan.gameObject.SetActive(false);

                Invoke(nameof(NewRound), 3f);
            }

        }

    }
    public void PowerPelletEaten(PowerPellet pell)
    {   
        for (int i = 0; i<GhostS.Count;i++)
        {

            if (this.GhostS[i].gameObject.activeSelf)
            {
                this.GhostS[i].frighten.Enable(pell.dura);
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
        foreach (Transform pellet in this.Pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
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
        Level1 = false;
        Camera.main.orthographicSize = 15f;
        foreach (GameObject obj in Level1Stuff)
        {
            obj.SetActive(false);

        }
        foreach (GameObject obj in Level2Stuff)
        {
            obj.SetActive(true);
        }
        Level2 = true;
    }
    private void Level2to1()
    {
        Level2 = false;
        Camera.main.orthographicSize = 18f;
        foreach (GameObject obj in Level1Stuff)
        {
            obj.SetActive(true);

        }
        foreach (GameObject obj in Level2Stuff)
        {
            obj.SetActive(false);
        }
        Level1 = true;
    }
}
