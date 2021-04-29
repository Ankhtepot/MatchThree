using System.Collections;
using Enums;
using UnityEngine;
using Utils;

//Fireball Games * * * PetrZavodny.com

public class GamePiece : MonoBehaviour
{
#pragma warning disable 649
    public int xIndex;
    public int yIndex;

    public EInterpType interpolation = EInterpType.SmootherStep;
    private bool m_isMoving = false;
#pragma warning restore 649
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move((int) (transform.position.x + 2), (int) transform.position.y, 0.5f);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move((int) (transform.position.x - 2), (int) transform.position.y, 0.5f);
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move((int) transform.position.x, (int) (transform.position.y + 1), 0.5f);
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move((int) transform.position.x, (int) (transform.position.y - 1), 0.5f);
        }
    }

    public void SetCoord(int x, int y)
    {
        xIndex = x;
        yIndex = y;
    }

    public void Move(int destX, int destY, float timeToMove)
    {
        if (m_isMoving) return;
        
        StartCoroutine(MoveRoutine(new Vector3(destX, destY, 0), timeToMove));
    }

    private IEnumerator MoveRoutine(Vector3 destination, float timeToMove)
    {
        Vector3 startPosition = transform.position;

        bool reachedDestination = false;

        float elapsedTime = 0f;

        m_isMoving = true;

        while (!reachedDestination)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp(elapsedTime / timeToMove, 0, 1);

            t = MoveInterpolation.Get(interpolation, t);

            transform.position = Vector3.Lerp(startPosition, destination, t);
            
            if (Vector3.Distance(transform.position, destination) < 0.01f)
            {
                reachedDestination = true;
                transform.position = destination;
                SetCoord((int) destination.x, (int) destination.y);
            }
            
            yield return new WaitForEndOfFrame();
        }

        m_isMoving = false;
    }
}
