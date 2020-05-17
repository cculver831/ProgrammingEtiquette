using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class CreateBoard : MonoBehaviour
{
    public GameObject treePrefab;
    long TreeBB = 0; // for Trees
    long PlayerBB = 0; // for house
    public GameObject[] tilePrefabs;
    public GameObject housePreFab;
    public Text Score;
    GameObject[] tiles; //hold onto tiles
    //actual bitboard
    long dirtBB = 0; //need a bitboard for each tile
    long desertBB = 0; //need a bitboard for each tile

    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[64];
        //generating the board
        for(int r = 0; r < 8; r++)
        {
            for(int c = 0; c < 8; c++)
            {
                int randomTile = UnityEngine.Random.Range(0, tilePrefabs.Length); //creates and integer for radnom tile to be placed
                Vector3 pos = new Vector3(c, 0, r); //position where tile will be placed based on loop prgression
                GameObject tile = Instantiate(tilePrefabs[randomTile], pos, Quaternion.identity); //instantiating random tile at pos

                tile.name = tile.tag + "_" + r + "_" + c;
                tiles[r * 8 + c] = tile; //adds tile to Array
                if(tile.tag == "Dirt")
                {
                    dirtBB = SetCellState(dirtBB & ~PlayerBB, r, c);
                    //printBB("Dirt", dirtBB);
                }
                else if (tile.tag == "Desert")
                {
                    desertBB = SetCellState(desertBB & ~PlayerBB, r, c);
                    //printBB("Dirt", dirtBB);
                }
            }
        }
        Debug.Log("Cell Count: " + CellCount(dirtBB));
        InvokeRepeating("PlantTree", 1, 1);
    }

    //Show bit board in a flattened array in console 
    void printBB(string name, long bitboard)
    {
        Debug.Log(name + ": " + Convert.ToString(bitboard, 2).PadLeft(64, '0'));
    }
    // Update is called once per frame
    void Update()
    {
        //Player interaction
        //click on tile and put a house down
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                int r = (int)hit.collider.gameObject.transform.position.z;
                int c = (int)hit.collider.gameObject.transform.position.x;
                if(getCellState((dirtBB & ~TreeBB) | desertBB, r, c))
                {
                    GameObject house = Instantiate(housePreFab);
                    house.transform.parent = hit.collider.gameObject.transform; //parent house to tile that was clicked
                    house.transform.localPosition = Vector3.zero; // sets local position to zero for simplicity
                    PlayerBB = SetCellState(PlayerBB, r, c);
                    CalcScore();
                }

            }
           
        }
        
    }
    //Set a bit board cell to 1
    long SetCellState(long biboard, int row, int col)
    {
        long newBit = 1L << (row * 8 + col);
        return (newBit |= biboard);
    }

    //returns true if cell is occupied with a bit
    bool getCellState(long bitboard, int row, int col)
    {
        long newBit = 1L << (row * 8 + col);
        return ((newBit & bitboard)!=0);
    }

    //Gets number of cells that are in effect from bitboard
    int CellCount(long bb)
    {
        int count = 0;
        long bitb = bb;
        while(bitb != 0)
        {
            bitb &= bitb - 1;
            count++;
        }

        return count;
    }

    void CalcScore()
    {
        Score.text = "Score: " + (CellCount(dirtBB & PlayerBB) * 10 + CellCount(desertBB & PlayerBB) * 2);
    }

    //plants treee on random dirt tile on invoke
    void PlantTree()
    {
        int rr = UnityEngine.Random.Range(0, 8);
        int rc = UnityEngine.Random.Range(0, 8);
        if(getCellState(dirtBB & ~PlayerBB, rr, rc))
        {
            GameObject tree = Instantiate(treePrefab);
            tree.transform.parent = tiles[rr * 8 + rc].transform;
            tree.transform.localPosition = Vector3.zero;
            TreeBB = SetCellState(TreeBB, rr, rc); //adds tree placement to TreeBB;
        }
    }
}
