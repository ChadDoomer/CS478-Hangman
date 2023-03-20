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

    // arrays for storing word lists from text files
    private string[] solutions;
    private string[] validInputs;

    // indexes for rows and columns
    private int rowIndex;
    private int columnIndex;

    // word picked to guess
    private string word;

    // assigns the row array
    private void Awake()
    {
        rows = GetComponentsInChildren<Row>();
    }

    private void Start()
    {
        LoadData();
        SetRandomWord();
    }

    // function that loads the data from the text files onto the arrays
    private void LoadData()
    { 
        TextAsset textFileAll = Resources.Load("official_wordle_all") as TextAsset;
        validInputs = textFileAll.text.Split("\n");

        TextAsset textFileCommon = Resources.Load("official_wordle_common") as TextAsset;
        solutions = textFileCommon.text.Split("\n");
    }

    // sets a random word to guess from the array, lowercases it, and trims it
    private void SetRandomWord()
    {
        word = solutions[Random.Range(0, solutions.Length)];
        word = word.ToLower().Trim();

        //word = "blood"; // for testing purposes
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

    // logic for submitting a row
    private void SubmitRow(Row row)
    {
        for (int i = 0; i < row.tiles.Length; i++)
        {
            Tile tile = row.tiles[i];

            if (tile.letter == word[i])
            {
                //correct letter

            }
            else if (word.Contains(tile.letter))
            {
                //wrong spot

            }
            else
            {
                //incorrect letter

            }
        }

        rowIndex++;
        columnIndex = 0;

        if(rowIndex >= rows.Length)
        {
            //too many guesses
            enabled = false;
        }
    }
}
