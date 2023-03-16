using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    // constant array of letters
    private static readonly KeyCode[] ALPHABET = new KeyCode[]
    {
        KeyCode.A,KeyCode.B,KeyCode.C,KeyCode.D,KeyCode.E,KeyCode.F,
        KeyCode.G,KeyCode.H,KeyCode.I,KeyCode.J,KeyCode.K,KeyCode.L,
        KeyCode.M,KeyCode.N,KeyCode.O,KeyCode.P,KeyCode.Q,KeyCode.R,
        KeyCode.S,KeyCode.T,KeyCode.U,KeyCode.V,KeyCode.W,KeyCode.X,
        KeyCode.Y,KeyCode.Z
    };
    // array of rows
    private Row[] rows;

    // indexes for rows and columns
    private int rowIndex;
    private int columnIndex;

    // assigns the row array
    private void Awake()
    {
        rows = GetComponentsInChildren<Row>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            // decreases the column index to point at the value just entered with the clamp function to make sure it does not go negative
            columnIndex = Mathf.Clamp(columnIndex - 1, 0, 5);
            // pressing backspace nulls out the current tile value
            rows[rowIndex].tiles[columnIndex].SetLetter('\0');
        }
        else if (columnIndex >= rows[rowIndex].tiles.Length)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SubmitRow(rows[rowIndex]);
            }
        }
        else
        {
            // loops through every letter in ALPHABET
            for (int i = 0; i < ALPHABET.Length; i++)
            {
                // if the input matches a letter, the selected tile displays that letter and moves to the next tile
                if (Input.GetKeyDown(ALPHABET[i]))
                {
                    rows[rowIndex].tiles[columnIndex].SetLetter((char)ALPHABET[i]);
                    columnIndex++;
                    break;
                }
            }
        }
    }
    private void SubmitRow(Row row)
    {
        // submit row logic
    }
}
