using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber_script : MonoBehaviour
{
    private bool fishNear = false;

    public bool getFishNear() { return fishNear; }
    GameObject fish;

    private void OnTriggerEnter(Collider other)
    {    
        if (other.tag == "Fish")
        {
            GetComponent<WaterFloat_script>().AttachToSurface = false;
            fishNear = true;
            fish = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Fish")
        {

            GetComponent<WaterFloat_script>().AttachToSurface = true;
            fishNear = false;
            fish = null;
        }
    }

    public void catchFish()
    {
        Destroy(fish);
    }
}
