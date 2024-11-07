using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PopulateTilemap : MonoBehaviour
{
    [SerializeField] private TileBase backgroundTile;
    [SerializeField] private Tilemap raceTrackTilemap;
    [SerializeField] private Tilemap backgroundTilemap;
    [SerializeField] private int marginSize; 
    
    // Start is called before the first frame update
    void Start()
    {
        raceTrackTilemap.CompressBounds();
        var cellBounds = raceTrackTilemap.cellBounds;
        
        for (var x = cellBounds.xMin - marginSize; x < cellBounds.xMax + marginSize; x++)
        {
            for (var y = cellBounds.yMin - marginSize; y < cellBounds.yMax + marginSize;y++)
            {
                backgroundTilemap.SetTile( new Vector3Int(x,y), backgroundTile);
            }
        }
    }

}
