using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //For archiving purposes, this script must be accompanied by a box collider on CubeGrazer in order to work properly with interaction with enemy bullets.

    public AudioSource deathAudio;
    public GameObject model;

    [Header("Health")]
    public int health = 5;
    public RawImage healthUI;
    public Texture[] healthImages;
    

    [Header("Lives")]
    public int lives = 3;
    public RawImage livesUI;
    public Texture[] livesImages;
    public GameObject crosshair;
    public CinemachineSmoothPath path;
    public CinemachineDollyCart ghostDolly;

    private Collider healthCollider;
    private CinemachineDollyCart dollyCart;
    private SkinnedMeshRenderer mr;
    private float startingDollySpeed;

    public List<Transform> sections;
    public List<SpeedTrigger> speedTrigs;

    public static int checkpointNum;

    public bool respawning;
    private readonly int respawnCycles = 10;
    
    public bool flickering;
    private readonly int flickerCycles = 3;

    public void Start()
    {
        healthCollider = this.GetComponent<Collider>();
        dollyCart = transform.parent.parent.GetComponent<CinemachineDollyCart>();
        mr = GetComponentInChildren<SkinnedMeshRenderer>();
        startingDollySpeed = dollyCart.m_Speed;
        checkpointNum = 0;

        SetBeginningValues();
    }

    public void Damage(int amount)
    {
        if (health > 0)
        {
            health -= amount;

            healthUI.texture = healthImages[health];
        }

        if (health <= 0 && lives > 0)
        {
            lives--;
            livesUI.texture = livesImages[lives];

            if(lives != 0)
            {
                StartCoroutine(Respawn());
            }
            else if(lives == 0)
            {
                mr.enabled = false;
                Weapon.alive = false;
                StartCoroutine(ResetToCheckpoint(checkpointNum));
            }
        }
        else if(!flickering) StartCoroutine(Flicker()); 
    }

    void SetBeginningValues()
    {
        Weapon.alive = true;
        mr.enabled = true;
        respawning = false;

        health = 5;
        lives = 3;
        healthUI.texture = healthImages[health];
        livesUI.texture = livesImages[lives];
        dollyCart.m_Speed = startingDollySpeed;
        dollyCart.m_Path = path;
    }

    void ResetEnemies(Transform section)
    {
        if (!section)
            return;

        for (int j = 0; j < section.childCount; j++)
        {
            Transform child = section.GetChild(j);

            for (int i = 0; i < child.childCount; i++)
            {
                Transform childsChild = child.transform.GetChild(i);
                childsChild.gameObject.SetActive(true);
                childsChild.GetComponent<EnemyMainPath>().ResetEnemy();
            }
        }
    }

    private void ResetBoss(Transform section)
    {
        section.GetComponent<BossController>().SelfReset();
    }

    void ResetGhosts()
    {
        ghostDolly.m_Position = 0;
    }

    void ResetSpeedTriggers()
    {
        foreach(SpeedTrigger t in speedTrigs)
        {
            t.TurnOnCollider();
        }
    }

    public IEnumerator Respawn()
    {
        respawning = true;

        healthCollider.enabled = false;

        for (int i = 0; i < respawnCycles; i++)
        {
            model.SetActive(false);
            yield return new WaitForSeconds(.2f);
            model.SetActive(true);
            yield return new WaitForSeconds(.2f);
        }

        health = 5;
        healthUI.texture = healthImages[health];

        healthCollider.enabled = true;
        respawning = false;
    }

    //Needed for when player hits an enemy. 
    public IEnumerator Flicker()
    {
        flickering = true;
        healthCollider.enabled = false;

        for (int i = 0; i < flickerCycles; i++)
        {
            model.SetActive(false);
            yield return new WaitForSeconds(.2f);
            model.SetActive(true);
            yield return new WaitForSeconds(.2f);
        }

        yield return new WaitForSeconds(.25f);

        healthCollider.enabled = true;
        flickering = false;
    }

    public IEnumerator ResetToCheckpoint(int cn)
    {
        yield return new WaitForSeconds(3f);

        SetBeginningValues();
        ResetSpeedTriggers();

        int s = SceneManager.GetActiveScene().buildIndex;
        switch(s)
        {
            case 1:
                switch(cn)
                {
                    case 0:
                        dollyCart.m_Position = 0;
                        break;
                    case 1:
                        dollyCart.m_Position = 475;
                        break;
                    case 2:
                        dollyCart.m_Position = 1175;
                        break;
                    case 3:
                        dollyCart.m_Position = 1850;
                        break;
                }
                break;
            case 2:
                switch (cn)
                {
                    case 0:
                        dollyCart.m_Position = 0;
                        break;
                    case 1:
                        dollyCart.m_Position = 925;
                        break;
                    case 2:
                        dollyCart.m_Position = 2975;
                        break;
                    case 3:
                        dollyCart.m_Position = 4425;
                        break;
                }
                break;
            case 3:
                switch (cn)
                {
                    case 0:
                        dollyCart.m_Position = 0;
                        break;
                    case 1:
                        dollyCart.m_Position = 750;
                        break;
                    case 2:
                        dollyCart.m_Position = 2150;
                        break;
                    case 3:
                        dollyCart.m_Position = 2925;
                        break;
                }
                break;
            case 4:
                switch (cn)
                {
                    case 0:
                        dollyCart.m_Position = 0;
                        break;
                    case 1:
                        dollyCart.m_Position = 2450;
                        break;
                    case 2:
                        dollyCart.m_Position = 3100;
                        break;
                    case 3:
                        dollyCart.m_Position = 4150;
                        break;
                }
                break;
            case 5:
                switch (cn)
                {
                    case 0:
                        dollyCart.m_Position = 0;
                        break;
                    case 1:
                        dollyCart.m_Position = 750;
                        break;
                    case 2:
                        dollyCart.m_Position = 3750;
                        break;
                    case 3:
                        dollyCart.m_Position = 5260;
                        break;
                }
                break;
            default:
                Debug.Log("Not valid scene");
                break;
        }
        if(s == 4 && cn == 2)
            ResetGhosts();
        else if (cn != 3)
            ResetEnemies(sections[cn]);
        else
            ResetBoss(sections[cn]);
    }

    public IEnumerator Restart()
    {
        Weapon.alive = false;
        healthCollider.enabled = false;
        deathAudio.Play();
        model.SetActive(false);
        crosshair.SetActive(false);
        //Destroy(gameObject);

        yield return new WaitForSeconds(3f);
        Weapon.alive = true;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
   
}