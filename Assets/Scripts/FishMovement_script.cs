using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement_script : MonoBehaviour
{
    private GameManager_script gm;

    private List<Transform> targets = new List<Transform>();
    public void setTargetArray(List<Transform> nodes) { targets = nodes; }
    bool isMoving = false;
    float speed= 3.0f;

    [SerializeField] private Transform newTarget;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_script>();
    }

    private void Update()
    {
        updateTargets();

        if (newTarget == null)
        {
            newTarget = targets[Random.Range(0, targets.Count)];
            isMoving = true;
        }

        if (isMoving == false)
        {
            newTarget = targets[Random.Range(0, targets.Count)];
            isMoving = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, newTarget.position, speed * Time.deltaTime);
        transform.LookAt(newTarget);

        if (transform.position == newTarget.position)
        {
            isMoving = false;
        }
        
    }

    void updateTargets()
    {
        setTargetArray(gm.getNodes());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}
