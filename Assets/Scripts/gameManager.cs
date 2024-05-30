using UnityEngine;

public class gameManager : MonoBehaviour
{
    private player1 player1;
    private player2 player2;
    private home[] homes;
    public int difficulty;
    public GameObject barrel1;
    public GameObject barrel2;
    public GameObject platform1;
    public GameObject platform2;
    public GameObject platform3;
    public GameObject platform4;
    public GameObject platform5;
    public GameObject platform6;
    public GameObject tomato;
    private int score;

    private void NewGame ()
    {
        NewLevel();
    }

    private void Awake()
    {
        homes = FindObjectsOfType<home>();
        player1 = FindObjectOfType<player1>();
        player2 = FindObjectOfType<player2>();
        difficulty = 0;
    }
    private void NewLevel ()
    {
        difficulty = 0;
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
        score = score - 1;
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
        score = score + 1;
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
                difficulty++;
                if (difficulty == 1)
                {
                    barrel1.SetActive(true);
                    barrel2.SetActive(true);
                } else if (difficulty == 2)
                {
                    platform1.SetActive(false);
                    platform2.SetActive(false);
                    platform3.SetActive(false);
                    platform4.SetActive(false);
                    platform5.SetActive(false);
                    platform6.SetActive(false);
                } else if (difficulty == 3)
                {
                    tomato.SetActive(true);
                }
                return false;
            }
        }

        return true;
    }
}
