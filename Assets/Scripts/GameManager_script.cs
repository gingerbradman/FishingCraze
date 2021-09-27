using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_script : MonoBehaviour
{
    GameObject[] nodes;
    private List<Transform> nodesTransform = new List<Transform>();
    public List<Transform> getNodes() { return nodesTransform; }

    // Start is called before the first frame update
    void Start()
    {
        nodes = GameObject.FindGameObjectsWithTag("Node");
        for (int i = 0; i<nodes.Length; i++)
        {
            nodesTransform.Add(nodes[i].GetComponent<Transform>());
        }
    }

    public void addBobber(GameObject bobber)
    {
        nodesTransform.Add(bobber.GetComponent<Transform>());
    }

    public void removeBobber(GameObject bobber)
    {
        nodesTransform.Remove(bobber.GetComponent<Transform>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
