using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour {

    public static BirdScript instance;

    

    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private Animator anim;

    //[SerializeField]
    private float forwardSpeed = 2.5f;

    //[SerializeField]
    private float bounceSpeed = 7.5f;

    private bool didFlap;

    public bool isAlive;

    private Button flapButton;

    [SerializeField]
    private AudioSource audioSource;


    [SerializeField]
    private AudioClip flapClick, pointClip, diedClip;

    public int score;

    void Start()
    {
        
    }
    

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        isAlive = true;

        score = 0;

    

        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        flapButton.onClick.AddListener(() =>  FlapTheBird());

        SetCamerasX();
    }

  
	
	// Update is called once per frame, but "FixedUpdate() is called ever few frames
	void FixedUpdate () {
        if (isAlive)
        {
            // get the current position of our bird
            Vector3 temp = transform.position;
            temp.x += forwardSpeed * Time.deltaTime;
            transform.position = temp;
            

            if (didFlap)
            {
                didFlap = false;
                myRigidBody.velocity = new Vector2(0, bounceSpeed);
                audioSource.PlayOneShot(flapClick);
                anim.SetTrigger("Flap");
            }

            if(myRigidBody.velocity.y >= 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            } else
            {
                float angle = 0;
                angle = Mathf.Lerp(0, -90, -myRigidBody.velocity.y / 45);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

        
        }
	}

    public void SetCamerasX()
    {
        CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x) - 1f;
    }

    public float GetPositionX()
    {
        return transform.position.x;
    }

    public void FlapTheBird()
    {
        didFlap = true;
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Ground" || target.gameObject.tag == "Pipe")
        {
            if (isAlive)
            {
                isAlive = false;
                anim.SetTrigger("Bird Died");
                audioSource.PlayOneShot(diedClip);
                GameplayController.instance.PlayerDiedShowScore(score);
                AdsController.instance.deaths++;
                if (AdsController.instance.deaths % AdsController.DEATHS_TILL_AD == 0)
                {
                    AdsController.instance.ShowInterstitial();
                }
                
            
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "PipeHolder")
        {
            //Debug.Log("IN THE PIPE HOLDER");
            score++;
            GameplayController.instance.SetScore(score);
            audioSource.PlayOneShot(pointClip);
        }
    }
}
