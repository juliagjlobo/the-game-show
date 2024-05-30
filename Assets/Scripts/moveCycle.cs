using Unity.VisualScripting;
using UnityEngine;

public class moveCycle : MonoBehaviour
{
    public Vector2 direction = Vector2.right; //movement direction
    private float speedI;
    public float speed = 1f; //movement speed
    public float speedH = 1f;
    public int size = 1; //obstacle's vertical size
    public bool enabledStart;
    public GameObject activeGameObject;
    public int progress = 5;
    private gameManager script;

    private Vector3 leftEdge;
    private Vector3 rightEdge;

    private void Start()
    {
        speedI = speed;
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
    }

    private void Update()
    {
        script = FindObjectOfType<gameManager>();
        if (script.difficulty == 0)
        {
            speed = speedI;
            activeGameObject.SetActive(enabledStart);
        }
        if (script.difficulty == 4)
        {
            speed = speedH;
        }
        if (direction.x > 0 && (transform.position.x - size) > rightEdge.x)
        {
            Vector3 position = transform.position;
            position.x = leftEdge.x - size;
            transform.position = position;
        }
        else if (direction.x < 0 && (transform.position.x + size) < leftEdge.x)
        {
            Vector3 position = transform.position;
            position.x = rightEdge.x + size;
            transform.position = position;
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
