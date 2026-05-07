using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFurnitures : MonoBehaviour
{
    public GameObject[] furnitures;
    private int counter = 0;

    void Start()
    {
        for (int i = 0; i < furnitures.Length; i++)
        {
            furnitures[i].SetActive(i == counter);
        }
    }

    //public void nextFurniture()
    //{
    //    furnitures[counter].SetActive(false);
    //    counter = (counter + 1) % furnitures.Length;
    //    furnitures[counter].SetActive(true);
    //}

    //public void previousFurniture()
    //{
    //    furnitures[counter].SetActive(false);
    //    counter = (counter - 1 + furnitures.Length) % furnitures.Length;
    //    furnitures[counter].SetActive(true);
    //}

    public void selectFurniture(int index)
    {
        if (index < 0 || index >= furnitures.Length) return;
        if (index == counter) return;

        furnitures[counter].SetActive(false);
        counter = index;
        furnitures[counter].SetActive(true);
    }
}