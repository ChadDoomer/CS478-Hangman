using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    // public array of tiles
    public Tile[] tiles { get; private set; }

    // sets the tile array
    private void Awake()
    {
        tiles = GetComponentsInChildren<Tile>();
    }
}
