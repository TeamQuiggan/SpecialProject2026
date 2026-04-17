using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Ghosts[] GhostS;
    public Pacman PacMan;
    public Transform Pellets;
    public int score { get; private set; }
    public int lives { get; private set; }
    public int ghostMulti { get; private set; } = 1;
    public Movement movement;
    
    void Start()
    {
        NewGame(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (this.lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
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
        foreach (Transform pellet in this.Pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }
    private void GameOver()
    {
        for (int i = 0; i < this.GhostS.Length; i++)
        {
            this.GhostS[i].gameObject.SetActive(false);
        }
        this.PacMan.gameObject.SetActive(false);
    }
    private void ResetState()
    {   
        ResetGhostMulti();
        movement.ResetState();
        for (int i = 0; i < this.GhostS.Length; i++)
        {
            this.GhostS[i].ResetSttate();
        }
        this.PacMan.ResetState();
        
    }
    private void SetScore(int score)
    {
        this.score = score;
    }
    private void SetLives(int lives)
    {
        this.lives = lives;
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
        SetScore(this.score + pell.points);
        if (!HasRemainingPellets())
        {
            this.PacMan.gameObject.SetActive(false);

            Invoke(nameof(NewRound), 3f);
        }
    }
    public void PowerPelletEaten(PowerPellet pell)
    {
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
}
