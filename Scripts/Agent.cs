using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

public class Agent : MonoBehaviour
{
    GameObject particle;
    NavMeshAgent agent;
    public bool activated = false;
    public bool loveFound = false;
    public GameObject firstFlame;
    public GameObject secondFlame;
    //public Transform target;
    Vector3 startPosition;
    GameObject[] targetlove;
    GameObject[] bluech;
    GameObject heart;
    HeartAnim heartAnimation;
    Animator anim;
    AnimatorClipInfo[] animatorinfo;
    bool chck = true;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        particle = GameObject.FindGameObjectWithTag("EndEffect");
        startPosition = RandomNavmeshLocation(50f);
        agent.destination = RandomNavmeshLocation(50f);
        bluech = GameObject.FindGameObjectsWithTag("blue");
    }

    // Update is called once per frame
    void Update()
    {
        heart = GameObject.FindGameObjectWithTag("heart");

        if (this.agent.remainingDistance < 0.5f && !loveFound)
        {
            agent.destination = RandomNavmeshLocation(50f);
        }

        if (loveFound)
        {
            chck = false;
            if (transform.gameObject.tag == "yellow") ;
            {
                targetlove = GameObject.FindGameObjectsWithTag("yellow");

                for (int i = 0; i < targetlove.Length; i++)
                {
                    if (targetlove[i] != gameObject)
                    {
                        Agent temp = targetlove[i].GetComponent<Agent>();
                        if (temp.loveFound == true && targetlove[i].tag ==this.tag)
                        {
                            agent.destination = targetlove[i].transform.position;
                        }
                    }
                }
            }

            if (transform.gameObject.tag == "red") ;
            {
                targetlove = GameObject.FindGameObjectsWithTag("red");
                for (int i = 0; i < targetlove.Length; i++)
                {
                    if (targetlove[i] != gameObject)
                    {
                        Agent temp = targetlove[i].GetComponent<Agent>();
                        if (temp.loveFound == true && targetlove[i].tag == this.tag)
                        {
                            agent.destination = targetlove[i].transform.position;
                        }
                    }
                }
            }
            if (transform.gameObject.tag == "blue") ;
            {
                targetlove = GameObject.FindGameObjectsWithTag("blue");
                for (int i = 0; i < targetlove.Length; i++)
                {
                    if (targetlove[i] != gameObject)
                    {
                        Agent temp = targetlove[i].GetComponent<Agent>();
                        if (temp.loveFound == true && targetlove[i].tag == this.tag)
                        {
                            anim = gameObject.GetComponent<Animator>();
                            anim.SetTrigger("love");
                            animatorinfo = this.anim.GetCurrentAnimatorClipInfo(0);
                            Debug.Log(animatorinfo[0].clip.name);
                            if (animatorinfo[0].clip.name == "Walking")
                            {
                                agent.speed = 2.8f;
                            }
                            agent.destination = targetlove[i].transform.position;
                        }
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // The collision function is commented out because the collider is being used.

        /*if(collision.gameObject.tag == this.gameObject.tag)
        {
            Agent collis = collision.gameObject.GetComponent<Agent>();
            if(this.loveFound == true&& collis.loveFound == true)
            {
                Vector3 temp = this.transform.position;
                temp.y += 1.4f;
                particle.transform.position = temp;
                heart.GetComponent<HeartAnim>().loveAnimation(this.gameObject.transform.position);
                particle.GetComponent<ParticleSystem>().Play();
                HeartAnim.score += 50;
                Destroy(gameObject);
            }
        }
        else if(collision.gameObject.name == "Arrow(Clone)")
        {
            this.TurnLover();
        }
        else
        {
            Vector3 newDestination = collision.transform.position + collision.transform.forward;
            agent.destination = newDestination;
            //agent.destination = RandomNavmeshLocation(50f);
            //Debug.Log(this.name + " " + collision.gameObject.name+" collide");
        }*/
    }
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        finalPosition.y = 1.06f;
        return finalPosition;
    }
    public void TurnLover()
    {
        if (activated)
            return;
        activated = true;
        loveFound = true;
        firstFlame.gameObject.SetActive(false);
        secondFlame.gameObject.SetActive(true);
        Debug.Log("turned lover");
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag != this.tag) && (other.tag != "sphere"))
        {
            if(other.tag=="yellow"||other.tag == "red"||other.tag == "blue")
            {
                other.GetComponent<Agent>().agent.SetDestination(RandomNavmeshLocation(50f));
            }
        }

        if(other.gameObject.name == "Arrow(Clone)")
        {
            this.TurnLover();
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == this.tag)
        {
            Debug.Log("sameeee");
            other.gameObject.TryGetComponent<Agent>(out Agent age);
            if (this.loveFound == true && age.loveFound == true)
            {
                Debug.Log("both lovefound");
                Vector3 temp = this.transform.position;
                temp.y += 1.4f;
                particle.transform.position = temp;
                heart.GetComponent<HeartAnim>().loveAnimation(this.gameObject.transform.position);
                particle.GetComponent<ParticleSystem>().Play();
                HeartAnim.score += 50;
                Destroy(gameObject);
            }
            else
            {
                agent.SetDestination(RandomNavmeshLocation(50f));
                other.GetComponent<Agent>().agent.SetDestination(RandomNavmeshLocation(50f));
            }
        }
        if(other.gameObject.TryGetComponent<Agent>(out Agent agen))
        {
            if (agen.loveFound&&this.loveFound)
            {
                Debug.Log("both lovefound");
                Vector3 temp = this.transform.position;
                temp.y += 1.4f;
                particle.transform.position = temp;
                heart.GetComponent<HeartAnim>().loveAnimation(this.gameObject.transform.position);
                particle.GetComponent<ParticleSystem>().Play();
                HeartAnim.score += 50;
                Destroy(gameObject);
            }
        }
    }
}
