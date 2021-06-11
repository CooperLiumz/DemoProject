using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniquePathsWithObstacles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[,] array0 = new int[2 , 2] { { 1 , 0 } , { 0 , 0 } };
        Debug.LogError (Solution (array0));
        Debug.LogError ("=======================");

        int[,] array1 = new int[2,2] { { 0 , 0 },{ 0, 0} };
        Debug.LogError (Solution (array1));
        Debug.LogError ("=======================");

        int[,] array2 = new int[3 , 3] { { 0, 0, 0 } , { 0, 1, 0 }, { 0, 0, 0} };
        Debug.LogError (Solution (array2));
        Debug.LogError ("=======================");

        int[,] array3 = new int[3 , 3] { { 0 , 0 , 0 } , { 0 , 0 , 0 } , { 0 , 0 , 0 } };
        Debug.LogError (Solution (array3));
        Debug.LogError ("=======================");

        int[][] iii = new int[2][];
        iii[0] = new int[3];
        iii[1] = new int[3];

        Debug.LogError (iii.Length);
        Debug.LogError (iii.GetLength(0));
        Debug.LogError (iii[0].Length);
    }


    int  Solution (int[,] obstacleGrid)
    {
        if (obstacleGrid.Length == 0)
        {
            return 0;
        }

        if (obstacleGrid[0,0] == 1)
        {
            return 0;
        }

        int row = obstacleGrid.GetLength (0);
        int col = obstacleGrid.GetLength (1);

        int[,] dp = new int[row,col];


        for (int i = 0; i < row; i++)
        {
            if (obstacleGrid[i,0] == 1)
            {
                dp[i,0] = 0;
                break;
            }
            dp[i , 0] = 1;
        }

        for (int i = 0; i < col; i++)
        {
            if (obstacleGrid[i,0] == 1)
            {
                dp[0 , i] = 0;
                break;
            }
            dp[0 , i] = 1;
        }

        for (int i = 1; i < row; i++)
        {
            for (int j = 1; j < col; j++)
            {
                if (obstacleGrid[i,j] == 1)
                {
                    continue;
                }
                else
                {
                    dp[i,j] = dp[i - 1, j] + dp[i, j - 1];
                }
            }
        }

        return dp[row-1, col-1];

    }

}
