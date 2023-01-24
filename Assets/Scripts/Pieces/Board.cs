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
    }

    private void CheckForValidMove()
    {
        if (!Input.GetMouseButtonDown(0)) { return; }; //Early out if mouse button not pressed.
        if (selectedPiece == null) { return; }; //Early out if no piece selected.

        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Check if space is within board region
        if (clickPosition.x < -4.0f || clickPosition.x > 4.0f) { return; };
        if (clickPosition.y < -4.0f || clickPosition.y > 4.0f) { return; };

        Debug.Log("Within Board region.");

        Vector2Int boardPosition = ToBoardPosition(clickPosition);

        //Check move is valid for piece
        if(!selectedPiece.CanMoveTo(boardPosition)) { return; };

        Debug.Log("Piece can move like this.");

        //Check if space not occupied by piece of the same colour.
        Ray ray = Camera.main.ScreenPointToRay(ToWorldPosition(ToBoardPosition(clickPosition)));
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit == false) { selectedPiece.MoveToPosition(boardPosition); }; //Empty space found.

        Debug.Log("Space Occupied.");

        //Check the hit object is a ChessPiece
        ChessPiece piece = hit.transform.GetComponent<ChessPiece>();
        if (piece == null) { return; };

        //Check it's of the opposing colour to the selected piece.
        switch (gameState)
        {
            case GameState.WhiteTurn:
                if (hit.transform.tag == "White")
                {
                    return;
                }
                break;

            case GameState.BlackTurn:
                if (hit.transform.tag == "Black")
                {
                    return;
                }
                break;

            default:
                return;
        }

        selectedPiece.MoveToPosition(boardPosition);
    }

    public static Vector2Int ToBoardPosition(Vector3 v)
    {
        return new Vector2Int((int)(v.x+3.5f), (int)(v.y+3.5f));//---------------------------------------------------------------
    }

    public static Vector3 ToWorldPosition(Vector2Int v)
    {
        return new Vector3((float)v.x - 3.5f, (float)v.y - 3.5f, 0.0f);//---------------------------------------------------------------
    }
}
