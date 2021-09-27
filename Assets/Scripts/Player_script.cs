using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_script : MonoBehaviour
{
    GameManager_script gm;
    GameObject seafloor;
    FishingPole_script fp;
    public GameObject bobberPrefab;
    private GameObject bobber;
    public GameObject bobberSpawn;
    float x_dim;
    float z_dim;

    public Slider powerSlider;
    bool sliderReverse; 

    bool isCast = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_script>();
        seafloor = GameObject.FindGameObjectWithTag("SeaFloor");
        fp = GameObject.FindGameObjectWithTag("FishingPole").GetComponent<FishingPole_script>();
        SpawnNewBobber();
        x_dim = 5;
        z_dim = 5;
    }

    void Spawn()
    {
        if (isCast == false)
        {
            bobber = Instantiate(bobberPrefab, Vector3.zero, Quaternion.identity) as GameObject;

            float x_rand = Random.Range(-x_dim, x_dim);
            float z_rand = Random.Range(-z_dim, z_dim);

            bobber.transform.position = new Vector3(x_rand, 0, z_rand);

            gm.addBobber(bobber);

            isCast = true;
        }
        else
        {
            if (bobber.GetComponent<Bobber_script>().getFishNear() == true)
            {
                bobber.GetComponent<Bobber_script>().catchFish();
            }
            gm.removeBobber(bobber);
            Destroy(bobber);
            isCast = false;
        }
    }



    // Update is called once per frame
    void Update()
    {
        switch (fp.state)
        {
            case (FishingPole_script.fishinCondition.Idle):
                if (Input.GetButtonDown("Fire1"))
                {
                    fp.state = FishingPole_script.fishinCondition.Aim;
                }
                break;
            case (FishingPole_script.fishinCondition.Aim):
                if (Input.GetButtonDown("Fire1"))
                {
                    fp.state = FishingPole_script.fishinCondition.Charge;
                }
                break;
            case (FishingPole_script.fishinCondition.Charge):
                if (Input.GetButton("Fire1"))
                {
                    Debug.Log("Holding");
                    ChargeSlider();
                }
                if (Input.GetButtonUp("Fire1"))
                {
                    fp.state = FishingPole_script.fishinCondition.Swing;
                }
                break;
            case (FishingPole_script.fishinCondition.Swing):
                fp.state = FishingPole_script.fishinCondition.Cast;
                Cast();
                break;
            case (FishingPole_script.fishinCondition.Cast):
                if (Input.GetButtonDown("Fire1"))
                {
                    Cast();
                    ResetSlider();
                    fp.state = FishingPole_script.fishinCondition.Idle;
                    SpawnNewBobber();
                }
                break;


        }
    }

    void Cast()
    {
        if (isCast == false)
        {
            bobber.GetComponent<Rigidbody>().velocity = bobberSpawn.transform.right * powerSlider.value / 5;
            bobber.GetComponent<Rigidbody>().useGravity = true;
            bobber.GetComponent<WaterFloat_script>().enabled = true;
            gm.addBobber(bobber.gameObject);
            isCast = true;
        }
        else
        {
            if (bobber.GetComponent<Bobber_script>().getFishNear() == true)
            {
                bobber.GetComponent<Bobber_script>().catchFish();
            }

            bobber.GetComponent<WaterFloat_script>().enabled = false;
            gm.removeBobber(bobber.gameObject);
            Destroy(bobber);
            isCast = false;
        }
    }

    void SpawnNewBobber()
    {
        bobber = Instantiate(bobberPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        bobber.transform.position = bobberSpawn.transform.position;
        bobber.transform.parent = bobberSpawn.transform;
    }

    void ChargeSlider()
    {
        if (!sliderReverse)
        {
            powerSlider.value += 0.5f;
            if (powerSlider.value == 100)
            { 
                sliderReverse = true;
            }
        }
        else
        {
            powerSlider.value -= 0.5f;
            if (powerSlider.value == 0)
            {
                sliderReverse = false;
            }
        }
        
    }

    void ResetSlider()
    {
        powerSlider.value = 0;
    }
}
