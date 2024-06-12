using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    private player1 player1;
    private player2 player2;
    private home[] homes;
    public int difficulty;
    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject obstacle3;
    public GameObject obstacle4;
    public GameObject obstacle5;
    public GameObject platform1;
    public GameObject platform2;
    public GameObject platform3;
    public GameObject platform4;
    public GameObject platform5;
    public GameObject platform6;
    public GameObject fadeout;
    public GameObject fadein;
    public GameObject locutor;
    public int scoreP1;
    public int scoreP2;
    public AudioSource fase1;
    public AudioSource fase2;
    public AudioSource fase3;
    public AudioSource fase4;
    public AudioSource fase5;


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
        scoreP1 = 0;
        scoreP2 = 0;
        Invoke(nameof(Initialize), 10f);
    }
    private void Initialize()
    {
        fadein.SetActive(false);
        locutor.SetActive(false);
    }
    private void NewLevel ()
    {
        if (scoreP1 > scoreP2)
        {
            SceneManager.LoadScene("P1Win", LoadSceneMode.Single);
        } else
        {
            SceneManager.LoadScene("P2Win", LoadSceneMode.Single);
        }
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
        scoreP1 = scoreP1 + 1;
        if (Cleared() || scoreP1 == 3)
        {
            fadeout.SetActive(true);
            Invoke(nameof(NewLevel), 3f);
        } else
        {
            Invoke(nameof(NewRoundOne), 1f);
        }
    }

    public void HomeOccupiedTwo()
    {
        player2.gameObject.SetActive(false);
        scoreP2 = scoreP2 + 1;
        if (Cleared() || scoreP2 == 3)
        {
            fadeout.SetActive(true);
            Invoke(nameof(NewLevel), 3f);
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
                    fase1.Stop();
                    fase2.Play();
                    obstacle1.SetActive(true);
                    obstacle2.SetActive(true);
                } else if (difficulty == 2)
                {
                    fase2.Stop();
                    fase3.Play();
                    platform1.SetActive(false);
                    platform2.SetActive(false);
                    platform3.SetActive(false);
                    platform4.SetActive(false);
                    platform5.SetActive(false);
                    platform6.SetActive(false);
                } else if (difficulty == 3)
                {
                    fase3.Stop();
                    fase4.Play();
                    obstacle3.SetActive(true);
                    obstacle4.SetActive(true);
                    obstacle5.SetActive(true);
                }
                else if (difficulty == 4)
                {
                    fase4.Stop();
                    fase5.Play();
                }
                return false;
            }
        }

        return true;
    }
}
