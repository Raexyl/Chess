using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    WhiteTurn,
    BlackTurn
}

public class Board : StaticInstance<Board>
{
    public ChessPiece selectedPiece;

    public GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.WhiteTurn;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForSelection();
        CheckForValidMove();
    }

    //Checks to see if a player has selected a piece.
    private void CheckForSelection()
    {
        if(!Input.GetMouseButtonDown(0)) { return; }; //Early out if mouse button not pressed.

        //Now find the piece that was pressed.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if(hit == false) { return; }; //Or null?

        //Check the hit object is a ChessPiece
        ChessPiece piece = hit.transform.GetComponent<ChessPiece>();
        if(piece == null) { return; };

        //Check it's of the correct colour
        switch(gameState)
        {
            case GameState.WhiteTurn:
                if (hit.transform.tag == "Black")
                {
                    return;
                }
                break;

            case GameState.BlackTurn:
                if (hit.transform.tag == "White")
                {
                    return;
                }
                break;

            default:
                return;
        }

        selectedPiece = piece;
        Debug.Log(selectedPiece);
    }

    private void CheckForValidMove()
    {
        if (!Input.GetMouseButtonDown(0)) { return; }; //Early out if mouse button not pressed.
    }
}
