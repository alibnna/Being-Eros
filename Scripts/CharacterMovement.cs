using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public bool activated;
    public ParticleSystem hitParticle;
    public Transform flameQuad;
    private Vector3 offset;
    Animator m_anim;
    float forwardSpeed = 5.0f;
    float moveSpeed = 7.0f;
    float limitValue = 4.5f;
    float positionX;
    // Start is called before the first frame update
    void Start()
    {
        offset.y = 6.5f;
        m_anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        flameQuad.position = transform.position+offset;
        forwardMovement();
        //Debug.Log(gameObject.transform.position.z);
        if (gameObject.transform.position.x > 60 || gameObject.transform.position.x < -40)
        {
            if (gameObject.transform.position.z > 0 && gameObject.transform.position.x > 60)
            {
                m_anim.SetTrigger("TurnRight");
            }
            else
                m_anim.SetTrigger("TurnLeft");
            //Debug.Log("first left");
        }
        if (gameObject.transform.position.z >60 || gameObject.transform.position.z < -60)
        {
            if (gameObject.transform.position.x < 10 && gameObject.transform.position.z > 60)
            {
                m_anim.SetTrigger("TurnRight");
            }
            else
                m_anim.SetTrigger("TurnLeft");
           // Debug.Log("second left");
        }
        
        setPosition();

    }
    private void forwardMovement()
    {
        transform.Translate(0, 0, forwardSpeed * Time.deltaTime * 0.7f);
    }
    private void forwardMovementx()
    {
        transform.Translate(forwardSpeed * Time.deltaTime * 0.7f, 0, 0);
    }

    public void setPosition()
    {
        if (Input.touchCount > 0)
        {
            Touch fing = Input.GetTouch(0);
            float horizontal = fing.deltaPosition.x * moveSpeed * Time.fixedDeltaTime * 3 / 10;
            positionX = transform.position.x + horizontal;
            positionX = Mathf.Clamp(positionX, -limitValue, limitValue);
            transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
        }
    }
    public void TurnLover()
    {
        if (activated)
            return;
        activated = true;

        hitParticle.Play();
        flameQuad.gameObject.SetActive(true);
    }
}
