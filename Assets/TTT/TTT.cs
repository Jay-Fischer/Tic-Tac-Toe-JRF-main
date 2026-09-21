using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerOption
{
    NONE, //0
    X, // 1
    O // 2
}

public class TTT : MonoBehaviour
{
    public int Rows;
    public int Columns;
    [SerializeField] BoardView board;

    PlayerOption currentPlayer = PlayerOption.X;
    Cell[,] cells;

    // Start is called before the first frame update
    void Start()
    {
        cells = new Cell[Columns, Rows];

        board.InitializeBoard(Columns, Rows);

        for(int i = 0; i < Rows; i++)
        {
            for(int j = 0; j < Columns; j++)
            {
                cells[j, i] = new Cell();
                cells[j, i].current = PlayerOption.NONE;
            }
        }
    }

    public void MakeOptimalMove()
    {
        bool canWin = false;
        int sum;

        //Win Check Code
        if (canWin == false)
        {
            //Rows
            for (int i = 0; i < Rows; i++)
            {

                sum = 0;
                for (int k = 0; k < Columns; k++)
                {
                    Debug.Log("Searching Row " + i + ", Column " + k);
                    var value = 0;
                    if (cells[k, i].current == currentPlayer)
                        value = 1;
                    else if (cells[k, i].current == PlayerOption.NONE)
                        value = 0;
                    else
                        value = -1;

                    sum += value;
                    Debug.Log("SUM: " + sum);
                }

                //Win if possible
                if (sum == 2)
                {
                    canWin = true;
                    for (int w = 0; w < Columns; w++)
                    {
                        Debug.Log("Winning...");
                        ChooseSpace(w, i);
                    }
                    return;
                }
                Debug.Log("-------------------NEXT-------------------");
            }

            //Columns
            for (int i = 0; i < Columns; i++)
            {
                sum = 0;
                for (int k = 0; k < Rows; k++)
                {
                    Debug.Log("Searching Column " + i + ", Row " + k);
                    var value = 0;
                    if (cells[i, k].current == currentPlayer)
                        value = 1;
                    else if (cells[i, k].current == PlayerOption.NONE)
                        value = 0;
                    else
                        value = -1;

                    sum += value;
                }

                //Win if possible
                Debug.Log("SUM: " + sum);
                if (sum == 2)
                {
                    canWin = true;
                    for (int w = 0; w < Columns; w++)
                    {
                        Debug.Log("Winning...");
                        ChooseSpace(i, w);
                    }
                    return;
                }
                Debug.Log("-------------------NEXT-------------------");
            }

            // top left to bottom right
            sum = 0;
            for (int i = 0; i < Rows; i++)
            {
                Debug.Log("Searching diagonal at [" + i + ", " + i + "]");
                int value = 0;
                if (cells[i, i].current == currentPlayer)
                    value = 1;
                else if (cells[i, i].current == PlayerOption.NONE)
                    value = 0;
                else
                    value = -1;

                sum += value;
                Debug.Log("SUM: " + sum);
            }

            if (sum == 2)
            {
                canWin = true;
                for (int w = 0; w < Rows; w++)
                {
                    Debug.Log("Winning...");
                    ChooseSpace(w, w);
                }
                return;
            }
            Debug.Log("-------------------NEXT-------------------");


            // top right to bottom left
            sum = 0;
            for (int i = 0; i < Rows; i++)
            {
                Debug.Log("Searching diagonal at [" + (Columns - 1 - i) + ", " + i + "]");
                int value = 0;

                if (cells[Columns - 1 - i, i].current == currentPlayer)
                    value = 1;
                else if (cells[Columns - 1 - i, i].current == PlayerOption.NONE)
                    value = 0;
                else
                    value = -1;

                sum += value;
                Debug.Log("SUM: " + sum);
            }

            if (sum == 2)
            {
                canWin = true;
                for (int w = 0; w < Rows; w++)
                {
                    Debug.Log("Winning...");
                    ChooseSpace(Columns - 1 - w, w);
                }
                return;
            }
            Debug.Log("---------------------No Wins Found---------------------");
        }
        
        //Block Checker
        if (canWin == false)
        {
            //Rows
            for (int i = 0; i < Rows; i++)
            {

                sum = 0;
                for (int k = 0; k < Columns; k++)
                {
                    Debug.Log("Searching Row " + i + ", Column " + k);
                    var value = 0;
                    if (cells[k, i].current == currentPlayer)
                        value = -1;
                    else if (cells[k, i].current == PlayerOption.NONE)
                        value = 0;
                    else
                        value = 1;

                    sum += value;
                    Debug.Log("SUM: " + sum);
                }

                //Block if possible
                if (sum == 2)
                {
                    for (int w = 0; w < Columns; w++)
                    {
                        Debug.Log("Blocking...");
                        ChooseSpace(w, i);
                    }
                    return;
                }
                Debug.Log("-------------------NEXT-------------------");
            }

            //Columns
            for (int i = 0; i < Columns; i++)
            {
                sum = 0;
                for (int k = 0; k < Rows; k++)
                {
                    Debug.Log("Searching Column " + i + ", Row " + k);
                    var value = 0;
                    if (cells[i, k].current == currentPlayer)
                        value = -1;
                    else if (cells[i, k].current == PlayerOption.NONE)
                        value = 0;
                    else
                        value = 1;

                    sum += value;
                }

                //Win if possible
                Debug.Log("SUM: " + sum);
                if (sum == 2)
                {
                    for (int w = 0; w < Columns; w++)
                    {
                        Debug.Log("Blocking...");
                        ChooseSpace(i, w);
                    }
                    return;
                }
                Debug.Log("-------------------NEXT-------------------");
            }

            // top left to bottom right
            sum = 0;
            for (int i = 0; i < Rows; i++)
            {
                Debug.Log("Searching diagonal at [" + i + ", " + i + "]");
                int value = 0;
                if (cells[i, i].current == currentPlayer)
                    value = -1;
                else if (cells[i, i].current == PlayerOption.NONE)
                    value = 0;
                else
                    value = 1;

                sum += value;
                Debug.Log("SUM: " + sum);
            }

            if (sum == 2)
            {
                for (int w = 0; w < Rows; w++)
                {
                    Debug.Log("Blocking...");
                    ChooseSpace(w, w);
                }
                return;
            }
            Debug.Log("-------------------NEXT-------------------");


            // top right to bottom left
            sum = 0;
            for (int i = 0; i < Rows; i++)
            {
                Debug.Log("Searching diagonal at [" + (Columns - 1 - i) + ", " + i + "]");
                int value = 0;

                if (cells[Columns - 1 - i, i].current == currentPlayer)
                    value = -1;
                else if (cells[Columns - 1 - i, i].current == PlayerOption.NONE)
                    value = 0;
                else
                    value = 1;

                sum += value;
                Debug.Log("SUM: " + sum);
            }

            if (sum == 2)
            {
                for (int w = 0; w < Rows; w++)
                {
                    Debug.Log("Blocking...");
                    ChooseSpace(Columns - 1 - w, w);
                }
                return;
            }
            Debug.Log("---------------------No Block Moves Found---------------------");
        }
    }

    public void ChooseSpace(int column, int row)
    {
        // can't choose space if game is over
        if (GetWinner() != PlayerOption.NONE)
            return;

        // can't choose a space that's already taken
        if (cells[column, row].current != PlayerOption.NONE)
            return;

        // set the cell to the player's mark
        cells[column, row].current = currentPlayer;

        // update the visual to display X or O
        board.UpdateCellVisual(column, row, currentPlayer);

        // if there's no winner, keep playing, otherwise end the game
        if(GetWinner() == PlayerOption.NONE)
            EndTurn();
        else
        {
            Debug.Log("GAME OVER!");
        }
    }

    public void EndTurn()
    {
        // increment player, if it goes over player 2, loop back to player 1
        currentPlayer += 1;
        if ((int)currentPlayer > 2)
            currentPlayer = PlayerOption.X;
    }

    public PlayerOption GetWinner()
    {
        // sum each row/column based on what's in each cell X = 1, O = -1, blank = 0
        // we have a winner if the sum = 3 (X) or -3 (O)
        int sum = 0;

        // check rows
        for (int i = 0; i < Rows; i++)
        {
            sum = 0;
            for (int j = 0; j < Columns; j++)
            {
                var value = 0;
                if (cells[j, i].current == PlayerOption.X)
                    value = 1;
                else if (cells[j, i].current == PlayerOption.O)
                    value = -1;

                sum += value;
            }

            if (sum == 3)
                return PlayerOption.X;
            else if (sum == -3)
                return PlayerOption.O;

        }

        // check columns
        for (int j = 0; j < Columns; j++)
        {
            sum = 0;
            for (int i = 0; i < Rows; i++)
            {
                var value = 0;
                if (cells[j, i].current == PlayerOption.X)
                    value = 1;
                else if (cells[j, i].current == PlayerOption.O)
                    value = -1;

                sum += value;
            }

            if (sum == 3)
                return PlayerOption.X;
            else if (sum == -3)
                return PlayerOption.O;

        }

        // check diagonals
        // top left to bottom right
        sum = 0;
        for(int i = 0; i < Rows; i++)
        {
            int value = 0;
            if (cells[i, i].current == PlayerOption.X)
                value = 1;
            else if (cells[i, i].current == PlayerOption.O)
                value = -1;

            sum += value;
        }

        if (sum == 3)
            return PlayerOption.X;
        else if (sum == -3)
            return PlayerOption.O;

        // top right to bottom left
        sum = 0;
        for (int i = 0; i < Rows; i++)
        {
            int value = 0;

            if (cells[Columns - 1 - i, i].current == PlayerOption.X)
                value = 1;
            else if (cells[Columns - 1 - i, i].current == PlayerOption.O)
                value = -1;

            sum += value;
        }

        if (sum == 3)
            return PlayerOption.X;
        else if (sum == -3)
            return PlayerOption.O;

        return PlayerOption.NONE;
    }
}
