﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cam;

    public List<GameObject> rooms = new List<GameObject>();

    Rigidbody2D rigidbody;

    public static GameManager Instance { get; private set; } //Because the variable is static and public it is accesable anywhere by using its name

    public int Score; //Whatever is public will be accesable through GameManagerScript.i.variable

    public int gridsize = 21;

    public Grid<GameObject> grid = new Grid<GameObject>();

    // ----- Makes sure there is only ever 1 instance of the GameManager class and deletes the class to avoid duplicates. -----
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        rigidbody = player.GetComponent<Rigidbody2D>();
        fill(10, 10);
        
    }

    public void Move(string direction)
    {
        switch (direction)
        {
            case "u":
                MoveUp();
                break;

            case "d":
                MoveDown();
                break;

            case "r":
                MoveRight();
                break;

            case "l":
                MoveLeft();
                break;

            default:

                break;
        }
    }

    public void MoveDown()
    {

        //add new rooms
        fill(10, 9);

        //shifts the rooms inside of the grid
        for (int i = 0; i < gridsize; i++)//i =ypos
        {
            for (int b = (gridsize-1); b >0; b--)// b = xpos
            {
                grid.Set(i, b, grid.Get(i, b-1));

                if (grid.Get(i, b) != null)
                {
                    grid.Get(i, b).transform.position = new Vector3(grid.Get(i, b).transform.position.x, grid.Get(i, b).transform.position.y +10);
                }
            }
        }


        //deletes the rooms
        for (int i = 0; i < gridsize; i++)
        {
            Destroy(grid.Get(i, 0));
            grid.Set(i, 0, null);
            Destroy(grid.Get(i, (gridsize - 1)));
            grid.Set(i, (gridsize - 1), null);

        }

        //shifts the player and the camera accordingly
        player.transform.position = new Vector3(player.transform.position.x , player.transform.position.y+3);
        Debug.Log("Moved downwards");
    }

    public void MoveUp()
    {

        //add new rooms
        fill(10, 11);
        Debug.Log("created new room");

        //shifts the rooms inside of the grid
        for (int i = 0; i < gridsize; i++)//i =ypos
        {
            for (int b = 0; b <(gridsize-1); b++)// b = xpos
            {
                grid.Set(i,b, grid.Get(i, b+1)); 

                if (grid.Get(i,b) != null)
                {
                    grid.Get(i, b).transform.position = new Vector3(grid.Get(i, b).transform.position.x, grid.Get(i, b).transform.position.y-10);
                }
            }
        }

        

        Debug.Log(grid.printall());

        //deletes the rooms
        for (int i = 0; i < gridsize; i++)
        {
            Destroy(grid.Get(i, 0));
            grid.Set(i,0, null);
            Destroy(grid.Get(i,(gridsize - 1)));
            grid.Set(i, (gridsize - 1),  null);

        }

        Debug.Log("deletet edges");

        //shifts the player and the camera accordingly
        player.transform.position = new Vector3(player.transform.position.x , player.transform.position.y-3);

        Debug.Log("Moved Upwards");
    }


    public void MoveLeft()
    {

        //add new rooms
        fill(9, 10);

        //shifts the rooms inside of the grid
        for (int i = 0; i < gridsize; i++)//i =ypos
        {
            for (int b = (gridsize-1); b > 0; b--)// b = xpos
            {
                grid.Set(b, i, grid.Get(b - 1, i));

                if (grid.Get(b, i) != null)
                {
                    grid.Get(b, i).transform.position = new Vector3(grid.Get(b, i).transform.position.x + 20, grid.Get(b, i).transform.position.y);
                }
            }
        }


        //deletes the rooms
        for (int i = 0; i < gridsize; i++)
        {
            Destroy(grid.Get(0, i));
            grid.Set(0, i, null);
            Destroy(grid.Get((gridsize - 1), i));
            grid.Set((gridsize - 1), i, null);

        }

        //shifts the player and the camera accordingly
        player.transform.position = new Vector3(player.transform.position.x + 5, player.transform.position.y);
        Debug.Log("Moved to the left");
    }


    public void MoveRight()
    {
        

        //add new rooms
        fill(11, 10);

        //shifts the rooms inside of the grid
        for (int i = 0; i < gridsize; i++)//i =ypos
        {
            for (int b = 0; b < (gridsize - 1); b++)// b = xpos
            { 
                grid.Set(b, i, grid.Get(b + 1, i));
           
                if (grid.Get(b, i) != null)
                {
                    grid.Get(b, i).transform.position = new Vector3(grid.Get(b, i).transform.position.x - 20, grid.Get(b, i).transform.position.y);
                }
            }
        }


        //deletes the rooms
         for (int i = 0; i < gridsize; i++)
         {
             Destroy(grid.Get(0, i));
             grid.Set(0, i, null);
            Destroy(grid.Get((gridsize-1), i));
            grid.Set((gridsize - 1), i, null);

        }

        //shifts the player and the camera accordingly
        player.transform.position = new Vector3(player.transform.position.x -5, player.transform.position.y);
        

        Debug.Log("Moved to the Right");
    }

    

    public void fill(int xpos, int ypos)
    {/*
        for (int i = 0; i < gs; i++)
        {
            for (int b = 0; b < gs; b++)
            {
                Transform choosenRoom = (rooms[Random.Range(0, rooms.Count())]).transform;
                scenegrid.Set(i, b, choosenRoom.gameObject);
            }
        }
        */
        Transform chosen = (rooms[Random.Range(0, rooms.Count())]).transform;
        if (grid.Get(xpos, ypos) == null)
        {
            grid.Set(xpos, ypos, chosen.gameObject);
            Transform room = Instantiate(grid.Get(xpos, ypos).transform, new Vector3((xpos * 20) - 200, (ypos * 10) - 100), Quaternion.identity);
            grid.Set(xpos, ypos, room.gameObject);
        }
        else
        {
            Debug.Log("room already exists");
        }
    }
}
