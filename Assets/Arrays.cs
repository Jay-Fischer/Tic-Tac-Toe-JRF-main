using UnityEngine;
using System.Collections.Generic;

public class Arrays : MonoBehaviour
{
    int[,] grid =
    {
        {7 , 9  , 2},
        {4 , 26 , 12},
        {8 , 105, 20},
        {30, 206, 64},
    };



    int[] myArray = { 7, 9, 2, 4, 26 };
    List<int> myList = new List<int>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*for (int i = 0; i < myArray.Length; i++)
        {
            Debug.Log(myArray[i]);
        }*/

        /*foreach (int i in myArray)
        {
            Debug.Log(i);
        }*/

        Debug.Log(grid[0, 0]);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
