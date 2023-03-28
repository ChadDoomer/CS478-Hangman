using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("States")]
    public Tile.State emptyState;
    public Tile.State occupiedState;
    public Tile.State correctState;
    public Tile.State wrongSpotState;
    public Tile.State incorrectState;

    [Header("UI")]
    public TextMeshProUGUI invalidWordText;
    public Button newWordButton;
    public Button retryButton;

    // assigns the row array
    private void Awake()
    {
        rows = GetComponentsInChildren<Row>();
    }

    private void Start()
    {
        LoadData();
        NewGame();
    }

    public void NewGame()
    {
        ClearBoard();
        SetRandomWord();
        enabled = true;
    }

    public void Retry()
    {
        ClearBoard();
        enabled = true;
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
            rows[rowIndex].tiles[columnIndex].SetState(emptyState);
            invalidWordText.gameObject.SetActive(false);
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
                    rows[rowIndex].tiles[columnIndex].SetState(occupiedState);
                    columnIndex++;
                    break;
                }
            }
        }
    }
    // logic for submitting a row
    private void SubmitRow(Row row)
    {
        if (!IsValid(row.word))
        {
            invalidWordText.gameObject.SetActive(true);
            return;
        }

        string remaining = word;

        // for loop that determines the correct and incorrect letters
        for (int i = 0; i < row.tiles.Length; i++)
        {
            Tile tile = row.tiles[i];

            if (tile.letter == word[i])
            {
                tile.SetState(correctState);
                remaining = remaining.Remove(i, 1);
                remaining = remaining.Insert(i, " ");
            }
            else if (!word.Contains(tile.letter))
            {
                tile.SetState(incorrectState);
            }
        }

        // for loop that determines wrong spot letters
        for (int i = 0; i < row.tiles.Length; i++)
        {
            Tile tile = row.tiles[i];

            if (tile.state != correctState && tile.state != wrongSpotState)
            {
                if (remaining.Contains(tile.letter))
                {
                    tile.SetState(wrongSpotState);

                    int index = remaining.IndexOf(tile.letter);
                    remaining = remaining.Remove(index, 1);
                    remaining = remaining.Insert(index, " ");
                }
                else
                    tile.SetState(incorrectState);
            }
        }

        if (HasWon(row))
        {
            enabled = false;
        }

        rowIndex++;
        columnIndex = 0;

        if (rowIndex >= rows.Length)
        {
            //too many guesses
            enabled = false;
        }
    }

    private bool IsValid(string word)
    {
        for (int i = 0; i < validInputs.Length; i++)
        {
            if (validInputs[i] == word)
                return true;
        }
        return false;
    }

    private void ClearBoard()
    {
        for (int row = 0; row < rows.Length; row++)
        {
            for (int column = 0; column < rows[row].tiles.Length; column++)
            {
                rows[row].tiles[column].SetLetter('\0');
                rows[row].tiles[column].SetState(emptyState);
            }
        }
        rowIndex = 0;
        columnIndex = 0;
    }

    private bool HasWon(Row row)
    {
        for (int i = 0; i < row.tiles.Length; i++)
        {
            if (row.tiles[i].state != correctState)
            {
                return false;
            }
        }
        return true;
    }

    private void OnEnable()
    {
        retryButton.gameObject.SetActive(false);
        newWordButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        retryButton.gameObject.SetActive(true);
        newWordButton.gameObject.SetActive(true);
    }
}
