using System;

class MatrixOperations
{
    public static double[,] InputMatrix(int rows, int cols)
    {
        double[,] matrix = new double[rows, cols];
        Console.WriteLine("Въведете елементите на матрицата:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"Елемент на позиция ({i + 1},{j + 1}): ");
                matrix[i, j] = Convert.ToDouble(Console.ReadLine());
            }
        }
        return matrix;
    }

    public static double[,] MultiplyMatrixByScalar(double[,] matrix, double scalar)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        double[,] result = new double[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = matrix[i, j] * scalar;
            }
        }
        return result;
    }

    public static double[,] MultiplyMatrices(double[,] matrixA, double[,] matrixB)
    {
        int rowsA = matrixA.GetLength(0);
        int colsA = matrixA.GetLength(1);
        int rowsB = matrixB.GetLength(0);
        int colsB = matrixB.GetLength(1);

        if (colsA != rowsB)
        {
            throw new InvalidOperationException("Матриците не могат да се умножават. Броят на колоните на първата матрица трябва да е равен на броя редове на втората.");
        }

        double[,] result = new double[rowsA, colsB];
        for (int i = 0; i < rowsA; i++)
        {
            for (int j = 0; j < colsB; j++)
            {
                for (int k = 0; k < colsA; k++)
                {
                    result[i, j] += matrixA[i, k] * matrixB[k, j];
                }
            }
        }
        return result;
    }

    public static double Determinant(double[,] matrix)
    {
        int n = matrix.GetLength(0);
        if (n == 2)
        {
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }

        double det = 0;
        for (int p = 0; p < n; p++)
        {
            double[,] subMatrix = GetSubMatrix(matrix, 0, p);
            det += (p % 2 == 0 ? 1 : -1) * matrix[0, p] * Determinant(subMatrix);
        }
        return det;
    }

    private static double[,] GetSubMatrix(double[,] matrix, int row, int col)
    {
        int n = matrix.GetLength(0);
        double[,] subMatrix = new double[n - 1, n - 1];
        int subi = 0;
        for (int i = 0; i < n; i++)
        {
            if (i == row) continue;
            int subj = 0;
            for (int j = 0; j < n; j++)
            {
                if (j == col) continue;
                subMatrix[subi, subj] = matrix[i, j];
                subj++;
            }
            subi++;
        }
        return subMatrix;
    }
    public static void PrintMatrix(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    static void Main()
    {
        bool continueProgram = true;

        while (continueProgram)
        {
            Console.WriteLine("\nИзберете операция:");
            Console.WriteLine("1. Умножение на матрица с число");
            Console.WriteLine("2. Умножение на две матрици");
            Console.WriteLine("3. Изчисляване на детерминанта");
            Console.WriteLine("4. Изход");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Въведете размерите на матрицата (редове и колони):");
                    int rows = Convert.ToInt32(Console.ReadLine());
                    int cols = Convert.ToInt32(Console.ReadLine());

                    double[,] matrix = InputMatrix(rows, cols);
                    Console.WriteLine("Въведената матрица:");
                    PrintMatrix(matrix);

                    Console.WriteLine("Въведете число за умножение:");
                    double scalar = Convert.ToDouble(Console.ReadLine());

                    double[,] result = MultiplyMatrixByScalar(matrix, scalar);
                    Console.WriteLine("Резултат от умножение с число:");
                    PrintMatrix(result);
                    break;

                case 2:
                    Console.WriteLine("Въведете размерите на първата матрица (редове и колони):");
                    int rowsA = Convert.ToInt32(Console.ReadLine());
                    int colsA = Convert.ToInt32(Console.ReadLine());

                    double[,] matrixA = InputMatrix(rowsA, colsA);

                    Console.WriteLine("Въведете размерите на втората матрица (редове и колони):");
                    int rowsB = Convert.ToInt32(Console.ReadLine());
                    int colsB = Convert.ToInt32(Console.ReadLine());

                    double[,] matrixB = InputMatrix(rowsB, colsB);

                    try
                    {
                        double[,] matrixResult = MultiplyMatrices(matrixA, matrixB);
                        Console.WriteLine("Резултат от умножение на матриците:");
                        PrintMatrix(matrixResult);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Грешка: " + ex.Message);
                    }
                    break;

                case 3:
                    Console.WriteLine("Въведете размерите на матрицата (редове и колони):");
                    rows = Convert.ToInt32(Console.ReadLine());
                    cols = Convert.ToInt32(Console.ReadLine());

                    matrix = InputMatrix(rows, cols);
                    Console.WriteLine("Въведената матрица:");
                    PrintMatrix(matrix);

                    if (rows == cols)
                    {
                        double det = Determinant(matrix);
                        Console.WriteLine($"Детерминантата на матрицата е: {det}");
                    }
                    else
                    {
                        Console.WriteLine("Матрицата не е квадратна, не може да се изчисли детерминант.");
                    }
                    break;

                case 4:
                    continueProgram = false;
                    break;

                default:
                    Console.WriteLine("Невалиден избор.");
                    break;
            }
        }
    }
}
