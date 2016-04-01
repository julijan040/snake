using UnityEngine;
using System.Collections;

public class mapMaker : MonoBehaviour {

    public GameObject tile;

    public int width; // width of map
    public int height; // height of map
    public int startPositionX; // start X position of tiles
    public int startPositionY; // start Y position of tiles    
    int originalStartPositionX;

    public Transform canvasTransform;

    void Start ()
    {
        makeMap();	    
	}
	
	void Update ()
    {
	
	}

    public void makeMap()
    {
        originalStartPositionX = startPositionX;

        for (int i = 0; i < height; i++)
        {
            for (int y = 0; y < width; y++)
            {
                GameObject copyTile = (GameObject)Instantiate(tile, new Vector2(startPositionX, startPositionY), Quaternion.identity);
                copyTile.transform.SetParent(canvasTransform, false);
                startPositionX+=50;
            }
            startPositionX = originalStartPositionX;
            startPositionY-=50;
        }
    }
}
