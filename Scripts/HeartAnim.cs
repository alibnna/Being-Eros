using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HeartAnim : MonoBehaviour
{
    public Transform quivPos;
    public static Vector3 quivPosition;
    public static int score = 0;
    public float time = 120f;
    public TMP_Text timer;
    public Text scoree;
    public GameObject particleSys;
    public GameObject heartParticlePrefab;
    public GameObject yellowLoverFemale;
    public GameObject yellowLoverMale;
    public GameObject redLoverFemale;
    public GameObject redLoverMale;
    public GameObject blueLoverFemale;
    public GameObject blueLoverMale;
    GameObject[] banksForBlues;
    Vector3 startPosition;
    float maleRotation;
    float femaleRotation;
    // Number of heart particles to spawn
    public int numParticles = 10;
    public ProgressBarCircle bar;

    // Duration of the animation (in seconds)
    public float duration = 2.0f;

    void Start()
    {
        score = 0;
        startPosition = new Vector3(20.0f, 0.0f, 1.0f);
        banksForBlues = GameObject.FindGameObjectsWithTag("Bank");
        quivPosition = quivPos.position;
        Debug.Log(quivPosition);
        //bar = bar.GetComponent<ProgressBarCircle>();
    }
    
    private void Update()
    {
        time -= Time.deltaTime;

        if(time < 0)
        {
            SceneManager.LoadScene("LeaderBoard");
        }
        GameObject[] reds = GameObject.FindGameObjectsWithTag("red");
        if (reds.Length == 0)
        {
            startPosition.x = Random.Range(10, 20);
            GameObject redMale = Instantiate(redLoverMale, startPosition, Quaternion.identity);
            GameObject redFemale = Instantiate(redLoverFemale, startPosition, Quaternion.identity);
        }

        GameObject[] yellows = GameObject.FindGameObjectsWithTag("yellow");
        if (yellows.Length == 0)
        {
            startPosition.x = Random.Range(-20, -10);
            GameObject yellowMale = Instantiate(yellowLoverMale, startPosition, Quaternion.identity);
            GameObject yellowFemale = Instantiate(yellowLoverFemale, startPosition, Quaternion.identity);
        }

        GameObject[] blues = GameObject.FindGameObjectsWithTag("blue");
        if (blues.Length == 0)
        {
            GameObject tempObj = banksForBlues[Random.Range(4, 7)];
            GameObject tempObjF = banksForBlues[Random.Range(0, 3)];
            Vector3 temp = tempObj.transform.position;
            Vector3 tempF = tempObjF.transform.position;
            temp.y = -0.3f;
            tempF.y = -0.3f;
            maleRotation = CheckRotation(tempObj);
            femaleRotation = CheckRotation(tempObjF);
            GameObject blueMale = Instantiate(blueLoverMale, temp, Quaternion.Euler(0, maleRotation, 0));
            GameObject blueFemale = Instantiate(blueLoverFemale, tempF, Quaternion.Euler(0,femaleRotation,0));
        }
    }

    IEnumerator DestroyParticles(float time)
    {
        yield return new WaitForSeconds(time);
        
        // Destroy all of the particles
        GameObject[] particles = GameObject.FindGameObjectsWithTag("HeartParticle");
        foreach (GameObject particle in particles)
        {
            yield return new WaitForSeconds(0.01f);
            Destroy(particle);
        }
    }

    public void loveAnimation(Vector3 targetPosition)
    {
        for (int i = 0; i < numParticles; i++)
        {
            Vector3 temp = targetPosition;
            temp.y = (temp.y + 0.7f);
            particleSys.transform.position = temp;
            ParticleSystem particles = particleSys.GetComponent<ParticleSystem>();
            //particles.Play();
            targetPosition.y = targetPosition.y + 2;
            // Instantiate the heart particle at the player's position
            int j = 0;
            while (j < 100)
            {
                GameObject particle = Instantiate(heartParticlePrefab, targetPosition, Quaternion.identity);

                // Set the particle's velocity in a random direction
                Rigidbody rb = particle.GetComponent<Rigidbody>();
                rb.velocity = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50,50));
                j++;
            }
        }
        Debug.Log("destroy");
        // Use a coroutine to destroy the particles after a set amount of time
        StartCoroutine(DestroyParticles(duration));
    }

    private float CheckRotation(GameObject obj)
    {
        Debug.Log(obj.name);
        if(obj.name == "Cube.003"||obj.name == "Cube.002")
        {
            if(obj.name == "Cube.002")
            {
                Vector3 temp = obj.transform.position;
                temp.x += 0.5f;
                obj.transform.position = temp;
            }
            if (obj.name == "Cube.003")
            {
                Vector3 temp = obj.transform.position;
                temp.x += 0.5f;
                obj.transform.position = temp;
            }
            return -90f;
        }
        if (obj.name == "Cube.008"|| obj.name == "Cube.005")
        {
            if (obj.name == "Cube.005")
            {
                obj.transform.position = setObjPosX(obj.transform.position, -0.5f);
            }
            if (obj.name == "Cube.008")
            {
                obj.transform.position = setObjPosX(obj.transform.position, -0.5f);
            }
            return 90f;
        }
        if (obj.name == "Cube.007" || obj.name == "Cube.001")
        {
            obj.transform.position = setObjPosZ(obj.transform.position, 0.5f);
            return 180f;
        }
        if (obj.name == "Cube.006")
        {
            Vector3 temp = obj.transform.position;
            temp.z -= 0.5f;
            obj.transform.position = temp;
            return 0f;
        }
        else
            return 0f;
    }    

    private Vector3 setObjPosZ(Vector3 temp, float value)
    {
        temp.z += value;
        return temp;
    }
    private Vector3 setObjPosX(Vector3 temp, float value)
    {
        temp.x += value;
        return temp;
    }

}