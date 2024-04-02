using System.Collections;
using UnityEngine;

public class frogger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Sprite leapSprite;
    public Sprite deadSprite;
    private bool cooldown;
    private Vector3 spawnPosition;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPosition = transform.position;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Move(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            Move(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            Move(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
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

        if (barrier != null) //checa se há colisão com barreira
        {
            return; //cancela o movimento
        }

        if (platform != null)
        {
            transform.SetParent(platform.transform); //sobe na plataforma
        } else {        
            transform.SetParent(null); //desce da plataforma
        }

        if (obstacle != null && platform == null)
        {
            transform.position = destination;
            Death();
        }
        else
        {
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

        spriteRenderer.sprite = leapSprite;
        
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, destination, t);
            elapsed += Time.deltaTime;
            yield return null; 
        }

        transform.position = destination;
        spriteRenderer.sprite = idleSprite;
        cooldown = false;
    }

    private void Death()
    {
        StopAllCoroutines();
        transform.rotation = Quaternion.identity;
        spriteRenderer.sprite = deadSprite;
        enabled = false;

        Invoke(nameof(Respawn), 1f);
    }

    public void Respawn ()
    {
        StopAllCoroutines();

        transform.rotation = Quaternion.identity;
        transform.position = spawnPosition;
        spriteRenderer.sprite = idleSprite;
        gameObject.SetActive(true);
        enabled = true;
        cooldown = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (enabled && other.gameObject.layer == LayerMask.NameToLayer("obstacle") && transform.parent == null)
        {
            Death();
        }
    }
}
