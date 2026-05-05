using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFurnitures : MonoBehaviour
{
    public GameObject[] furnitures;
    private int counter = 0;

    void Start()
    {
        // Ensure only the first furniture is active at the beginning
        for (int i = 0; i < furnitures.Length; i++)
        {
            furnitures[i].SetActive(i == counter);
        }
    }

    public void nextFurniture()
    {
        furnitures[counter].SetActive(false); // deactivate current
        counter = (counter + 1) % furnitures.Length;
        furnitures[counter].SetActive(true);  // activate next
    }

    public void previousFurniture()
    {
        furnitures[counter].SetActive(false); // deactivate current
        counter = (counter - 1 + furnitures.Length) % furnitures.Length;
        furnitures[counter].SetActive(true);  // activate previous
    }
}
