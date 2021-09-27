using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPole_script : MonoBehaviour
{
    public enum fishinCondition { Idle, Aim, Charge, Swing, Cast };

    public fishinCondition state;

    public float rotateSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        state = fishinCondition.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case fishinCondition.Idle:
                Debug.Log("Idle State");
                break;
            case fishinCondition.Aim:
                Debug.Log("Aim State");
                float pingPong = Mathf.PingPong(Time.time * rotateSpeed, 90);
                pingPong = -pingPong;
                transform.localEulerAngles = new Vector3(0, pingPong, 30);
                break;
            case fishinCondition.Charge:
                Debug.Log("Charge State");
                Debug.Log(returnEuler());
                break;
            case fishinCondition.Swing:
                Debug.Log("Swing State");
                break;
            case fishinCondition.Cast:
                Debug.Log("Cast State");
                break;
        }
    }

    Vector3 returnEuler()
    {
        return transform.localEulerAngles;
    }
}
