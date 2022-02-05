using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoader : MonoBehaviour
{
    Grid<GameObject> scenegrid;

    public List<GameObject> rooms = new List<GameObject>();

    int gs ;


    // Start is called before the first frame update
    void Start()
    {
        scenegrid = GameManager.Instance.grid;
        gs = GameManager.Instance.gridsize;
        
    }

    // Update is called once per frame
    void Update()
    {
    }

   

    private void moveUp()
    {
        
    }

    private void moveDown()
    {

    }

    
}
