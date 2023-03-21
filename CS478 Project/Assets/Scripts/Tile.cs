using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [System.Serializable]
    public class State
    {
        public Color fillColor;
        public Color outlineColor;
    }

    // creates reference to text component
    private TextMeshProUGUI text;

    // outline and image color variables
    private Image fill;
    private Outline outline;

    // state property for the tile
    public State state { get; private set; }

    // letter property for the tile
    public char letter { get; private set; }

    // called automatically by Unity, establishes reference to text component
    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        fill = GetComponent<Image>();
        outline = GetComponent<Outline>();
    }

    // sets the letter that shows up in the tile
    public void SetLetter(char letter)
    {
        this.letter = letter;
        text.text = letter.ToString();
    }
    
    public void SetState(State state)
    {
        this.state = state;
        fill.color = state.fillColor;
        outline.effectColor = state.outlineColor;
    }
}
