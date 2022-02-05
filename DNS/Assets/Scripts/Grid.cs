using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid<T> 
{

    T[,] grid = new T[21, 21]; //10,10 is the postion of the player

    public void Set(int x , int y, T element)
    {
        if (element != null || x >= 0 || y >= 0)
        {
            grid[x, y] = element;
        }
        else
        {
            Debug.Log("You fucked up");
        }
    }

    public string printall()
    {
        string temp = "";
        for (int i = 0; i< 21;i++)
        {
            
            for (int b = 0; b < 21;b++)
            {
             if(grid[b,i] != null)
                {
                    temp += "| |";    
                }
             else
                {
                    temp += "|f|";
                }
            }
            temp += "\n";
        }
        return temp;
    }

    public T Get(int x, int y)
    {
        if (x >= 0 || y >= 0)
        {
            return grid[x, y];
        }
        else
        {
            return default(T);
        }
    }
}
