using UnityEngine;

public class Ball : MonoBehaviour
{
    //Configuration params
    [SerializeField] Paddle paddle;
    [SerializeField] float velocityX = 2f;
    [SerializeField] float velocityY = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    //State params
    [SerializeField] bool hasStarted = false;
    Vector2 paddleToBallVector;

    //Cached component ref
    AudioSource audioSource;
    Rigidbody2D rb;

    void Start()
    {
        paddleToBallVector = transform.position - paddle.transform.position;
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            rb.velocity = new Vector2(velocityX, velocityY);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));

        if(hasStarted)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            audioSource.PlayOneShot(clip);
            rb.velocity += velocityTweak;
        }     
    }
}
