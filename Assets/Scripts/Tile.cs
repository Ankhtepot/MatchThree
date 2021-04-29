using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class Tile : MonoBehaviour
{
#pragma warning disable 649
    public int xIndex;
    public int yIndex;

    private Board m_board;
#pragma warning restore 649

    public void Init(int x, int y, Board board)
    {
        xIndex = x;
        yIndex = y;
        m_board = board;
    }
}
