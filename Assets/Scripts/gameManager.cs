using UnityEngine;

public class gameManager : MonoBehaviour
{
    private player1 player1;
    private player2 player2;
    private home[] homes;
    private int score;
    private int lives;
    private void NewGame ()
    {
        SetScore(0);
        SetLives(3);
        NewLevel();
    }

    private void Awake()
    {
        homes = FindObjectsOfType<home>();
        player1 = FindObjectOfType<player1>();
        player2 = FindObjectOfType<player2>();
    }

    private void NewLevel ()
    {
        for (int i = 0; i < homes.Length; i++)
        {
            homes[i].enabled = false;
        }
        NewRound();
    }

    private void NewRound()
    {
        player1.Respawn();
        player2.Respawn();
    }

    private void NewRoundOne ()
    {
        player1.Respawn();
    }

    private void NewRoundTwo()
    {
        player2.Respawn();
    }

    public void HomeOccupiedOne()
    {
        player1.gameObject.SetActive(false);

        if (Cleared())
        {
            Invoke(nameof(NewLevel), 1f);
        } else {
            Invoke(nameof(NewRoundOne), 1f);
        }
    }

    public void HomeOccupiedTwo()
    {
        player2.gameObject.SetActive(false);

        if (Cleared())
        {
            Invoke(nameof(NewLevel), 1f);
        }
        else
        {
            Invoke(nameof(NewRoundTwo), 1f);
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i <homes.Length;i++)
        {
            if (!homes[i].enabled)
            {
                return false;
            }
        }

        return true;
    }

    private void SetScore (int score)
    {
        this.score = score;
    }
    private void SetLives (int lives)
    {
        this.lives = lives;
    }
}
