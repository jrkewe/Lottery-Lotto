using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private int randomVectorRange = 5;
    private float forceDragIntoChamber = 5f;
    private float rotationSpeed;
    public int ballNumber;

    private Rigidbody ballRb;
    private Timer timerScript;
    private CountBalls countBallsScript;

    private bool ballIsNotCatched = true;

    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
        timerScript = GameObject.Find("GameManager").GetComponent<Timer>();
        countBallsScript = GameObject.Find("GameManager").GetComponent<CountBalls>();
    }

    // Update is called once per frame
    void Update()
    {
        //State of machine
        if (!timerScript.machineJumble)
        {
            FallDown();
        }
        if (timerScript.machineJumble)
        {
            Draw();
            KeepAwayFromContainers();
        }
        if (timerScript.machineDraws) 
        {
            KeepAwayFromContainers();

            if (countBallsScript.allBallsCatched == false)
            {
                Draw();
                DragIntoChamber();
            }
        }

        //State of ball
        //Ball is caught
        if (transform.position.y > 29.60f && timerScript.machineDraws)
        {
            Collider ballCollider = GetComponent<Collider>();
            ballRb.position = new Vector3(transform.position.x, 29.70f, transform.position.z);
            ballRb.constraints = RigidbodyConstraints.FreezeAll;
            if (ballIsNotCatched)
            {
                countBallsScript.AddBall(1, ballNumber);
                ballIsNotCatched = false;
            }
        }

        //Ball isnt caught but lottery is over
        if (countBallsScript.allBallsCatched && ballIsNotCatched)
        {
            FallDown();
        }
    }


    Vector3 GenerateRandomVector()
    {
        float randomX = Random.Range(-randomVectorRange, randomVectorRange);
        float randomY = Random.Range(-randomVectorRange, randomVectorRange);
        float randomZ = Random.Range(-randomVectorRange, randomVectorRange);
        Vector3 randomVector = new Vector3(randomX, randomY, randomZ).normalized;
        return randomVector;
    }

    void Draw()
    {
        ballRb.AddForce(GenerateRandomVector(), ForceMode.Impulse);
        Collider ball_Collider = GetComponent<Collider>();
        ball_Collider.material.bounciness = 1.0f;
        rotationSpeed = 200.0f;
        Rotate();
    }

    void FallDown()
    {
        ballRb.AddForce(Vector3.up * (-1), ForceMode.VelocityChange);
        Collider ball_Collider = GetComponent<Collider>();
        ball_Collider.material.bounciness = 0.0f;
        rotationSpeed = 50.0f;
        Rotate();

    }

    void KeepAwayFromContainers() 
    {
        float boundLeftZ = -3.952f;
        float boundRightZ = 4.43f;

        if (transform.position.z < boundLeftZ || transform.position.z > boundRightZ) 
        {
            if (transform.position.z < boundLeftZ)
            {
                Collider ball_Collider = GetComponent<Collider>();
                ballRb.position = new Vector3(transform.position.x, transform.position.y, boundLeftZ);
            }
            else 
            {
                Collider ball_Collider = GetComponent<Collider>();
                ballRb.position = new Vector3(transform.position.x, transform.position.y, boundRightZ);
            }
        }
    }

    void Rotate() 
    {
        Vector3 ballVelocity = ballRb.velocity;
        Vector3 movementDirection = new Vector3(ballVelocity.x, ballVelocity.y, ballVelocity.z).normalized;

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void DragIntoChamber() 
    {
        float boundY = 27.7f;
        float boundLeftX = -1.8f;
        float boundRightX = -1.10f;
        float boundLeftZ = -3.0f;
        float boundRightZ = 3.0f;

        if (transform.position.y > boundY)
        {
            if (transform.position.x > boundLeftX && transform.position.x < boundRightX)
            {
                if (transform.position.z > boundLeftZ && transform.position.z < boundRightZ)
                {
                    Collider ball_Collider = GetComponent<Collider>();
                    ballRb.velocity = Vector3.up * forceDragIntoChamber;
                }
            }
            
        }
    }

}
