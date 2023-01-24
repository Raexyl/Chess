using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    protected Vector2[] possibleMoves;

    protected Transform t;
    protected Collider2D coll2D;

    protected Vector2Int position = new Vector2Int(0, 0);

    // Start is called before the first frame update
    public void Awake()
    {
        if ((t = GetComponent<Transform>()) == null) { Debug.Log(gameObject); Debug.Log("Error - Missing vital component."); Destroy(gameObject); };
        if ((coll2D = GetComponent<Collider2D>()) == null) { Debug.Log(gameObject); Debug.Log("Error - Missing vital component."); Destroy(gameObject); };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Returns true if can theoretically move to the target position. (i.e. is what is allowed by the piece)
    public bool CanMoveTo(Vector2Int targetPosition)
    {
        return true;//-------------------------------------------------------------------------------------------
    }

    public void MoveToPosition(Vector2Int targetPosition)
    {
        //Forbid movement if already moving!-------------------------------------------------------------------
        StartCoroutine(LerpMove(targetPosition));
    }

    private IEnumerator LerpMove(Vector2Int targetPosition)
    {
        int i;
        for(i = 0; i < 50; i++)
        {
            t.Translate(Vector3.Lerp(t.position, Board.ToWorldPosition(targetPosition), 0.05f));
            yield return new WaitForSeconds(0.05f);
        }

        t.position = Board.ToWorldPosition(targetPosition);
        position = targetPosition;
    }


}
