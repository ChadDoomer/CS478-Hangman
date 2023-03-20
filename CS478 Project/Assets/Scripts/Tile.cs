using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public class State
    {
        public Color fillColor;
        public Color outlineColor;
    }

    // creates reference to text component
    private TextMeshProUGUI text;

    // letter property for the tile
    public char letter { get; private set; }

    // called automatically by Unity, establishes reference to text component
    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // sets the letter that shows up in the tile
    public void SetLetter(char letter)
    {
        this.letter = letter;
        text.text = letter.ToString();
    }
}
