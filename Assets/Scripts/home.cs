using UnityEngine;

public class home : MonoBehaviour
{
    public GameObject frog;
    public GameObject flag;
    private BoxCollider2D boxCollider;
    public AudioClip flagSound;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        frog.SetActive(true);
        boxCollider.enabled = false;
    }

    private void OnDisable()
    {
        frog.SetActive(false);
        boxCollider.enabled = true;
        flag.GetComponent<Renderer>().material.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        enabled = true;
        AudioSource.PlayClipAtPoint(flagSound, transform.position);

        if (other.tag == "PlayerOne")
        {
            FindObjectOfType<gameManager>().HomeOccupiedOne();
            flag.GetComponent<Renderer>().material.color = Color.blue;
        } else
        {
            FindObjectOfType<gameManager>().HomeOccupiedTwo();
            flag.GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
