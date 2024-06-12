using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1 : MonoBehaviour
{
    private bool cooldown;
    private Vector3 spawnPosition;
    public Animator animator;
    public AudioClip deathSound;
    public AudioClip waterSound;

    private void Awake()
    {
        spawnPosition = transform.position;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            animator.Play("PB", -1, 0f);
            Move(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            animator.Play("P1", -1, 0f);
            Move(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            animator.Play("P2", -1, 0f);
            Move(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            animator.Play("P2", -1, 0f);
            Move(Vector3.right);
        }
    }

    private void Move(Vector3 direction)
    {
        if (cooldown)
        {
            return;
        }

        Vector3 destination = transform.position + direction; //prevê para onde o jogador vai se movimentar

        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("barrier"));
        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("platform"));
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("obstacle"));
        Collider2D water = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("water"));

        if (barrier != null) //checa se há colisão com barreira
        {
            return; //cancela o movimento
        }

        if (platform != null)
        {
            transform.SetParent(platform.transform); //sobe na plataforma
        }
        else
        {
            transform.SetParent(null); //desce da plataforma
        }

        if (obstacle != null && platform == null)
        {
            transform.position = destination;
            Death();
        } else if (water != null && platform == null) {
            transform.position = destination;
            AudioSource.PlayClipAtPoint(waterSound, transform.position);
            Death();
        } else {
            StartCoroutine(Leap(destination));
        }

        StartCoroutine(Leap(destination)); //animação do pulo
    }

    private IEnumerator Leap(Vector3 destination)
    {
        cooldown = true;
        Vector3 startPosition = transform.position;

        float elapsed = 0f;
        float duration = 0.125f; //velocidade do pulo


        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, destination, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = destination;
        cooldown = false;
    }

    private void Death()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        animator.Play("Pmorte", -1, 0f);
        StopAllCoroutines();
        transform.rotation = Quaternion.identity;
        enabled = false;

        Invoke(nameof(Respawn), 1f);
    }

    public void Respawn()
    {
        StopAllCoroutines();

        transform.rotation = Quaternion.identity;
        transform.position = spawnPosition;
        gameObject.SetActive(true);
        enabled = true;
        cooldown = false;
        animator.Play("PD", -1, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enabled && other.gameObject.layer == LayerMask.NameToLayer("obstacle") && transform.parent == null)
        {
            Death();
        }
    }
}

