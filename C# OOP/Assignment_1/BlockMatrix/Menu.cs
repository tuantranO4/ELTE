    using System;
    using System.Collections.Generic;

    namespace BlockMatrix
    {
        public class Menu
        {
            private List<BlockMatrix> matrices = new List<BlockMatrix>();

            public void Run()
            {
                int choice;

                do
                {
                    Console.WriteLine("\nMenu:");
                    Console.WriteLine("-----------------------------------------------");
                    Console.WriteLine("0. Exit");
                    Console.WriteLine("1. Add new matrix");
                    Console.WriteLine("2. Get entry at index (i, j)");
                    Console.WriteLine("3. Summation of matrices (requires existing matrices)");
                    Console.WriteLine("4. Multiplication of matrices (requires existing matrices)");
                    Console.WriteLine("5. Print matrix");
                    Console.WriteLine("-----------------------------------------------");

                    choice = GetChoice();

                    switch (choice)
                    {
                        case 1:
                            CreateMatrix();
                            break;
                        case 2:
                            GetEntry();
                            break;
                        case 3:
                            SumMatrices();
                            break;
                        case 4:
                            MultiplyMatrices();
                            break;
                        case 5:
                            PrintMatrix();
                            break;
                    }
                } while (choice != 0);
            }

            private int GetChoice()
            {
                int v;
                do
                {
                    v = -1;
                    Console.WriteLine("CHOOSE OPTION: -");
                    string choose = Console.ReadLine();

                    if (!int.TryParse(choose, out v) || v < 0 || v > 5)
                    {
                        Console.WriteLine("Invalid option. Please choose between 0-5.");
                        continue;
                    }
                } while (v < 0 || v > 5);
                return v;
            }

            private void CreateMatrix()
            {
                int b1, b2, n;
                Console.WriteLine("Enter size of Block 1: ");
                b1 = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter size of Block 2: ");
                b2 = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter total matrix size: ");
                n = int.Parse(Console.ReadLine());

                BlockMatrix matrix = new BlockMatrix(b1, b2, n);

                Console.WriteLine("Enter values for block1 matrix:");
                for (int i = 0; i < b1; i++)
                {
                    for (int j = 0; j < b1; j++)
                    {
                        int value = int.Parse(Console.ReadLine());
                        matrix.SetEntry(i, j, value);
                    }
                }

                Console.WriteLine("Enter values for block2 matrix:");
                for (int i = b1; i < n; i++)
                {
                    for (int j = b1; j < n; j++)
                    {
                        int value = int.Parse(Console.ReadLine());
                        matrix.SetEntry(i, j, value);
                    }
                }

                matrices.Add(matrix); 
                Console.WriteLine("Matrix added successfully.");
            }

            private void GetEntry()
            {
                int index = GetMatrixIndex();

                if (index == -1)
                {
                    Console.WriteLine("No matrix in the database!");
                    return;
                }

                BlockMatrix matrix = matrices[index];

                int i, j;
                Console.WriteLine("Enter row index (1-" + matrix.n + "): ");
                i = int.Parse(Console.ReadLine()) - 1;
                Console.WriteLine("Enter column index (1-" + matrix.n + "): ");
                j = int.Parse(Console.ReadLine()) - 1;

                Console.WriteLine("Value at (" + (i + 1) + ", " + (j + 1) + "): " + matrix.GetEntry(i, j));
            }

            private int GetMatrixIndex()
        {
            if (matrices.Count == 0)
        {
            Console.WriteLine("No matrices in the database.");
            return -1;
        }

        int matrixIndex = -1;
        bool isValidIndex = false;

        do
        {
            Console.WriteLine("Enter the index of the matrix you want (e.g., '1' or '2'): ");
            string input = Console.ReadLine().Trim();

            if (int.TryParse(input, out matrixIndex))
            {
                if (matrixIndex >= 1 && matrixIndex <= matrices.Count)
                {
                    isValidIndex = true;
                }
                else
                {
                    Console.WriteLine("Invalid matrix index. Please enter a valid index.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid index.");
            }
        } while (!isValidIndex);

        return matrixIndex - 1; 
        }


            private void SumMatrices()
            {
                if (matrices.Count < 2)
                {
                    Console.WriteLine("Insufficient matrices to perform operation.");
                    return;
                }

                Console.WriteLine("-PERFORM SUMMATION-");
                int index1 = GetMatrixIndex();
                int index2 = GetMatrixIndex();

                if (index1 == -1 || index2 == -1)
                {
                    Console.WriteLine("Invalid matrix indices.");
                    return;
                }

                BlockMatrix matrix1 = matrices[index1];
                BlockMatrix matrix2 = matrices[index2];

                try
                {
                    BlockMatrix sum = BlockMatrix.Adding(matrix1, matrix2);
                    Console.WriteLine("Sum:\n" + sum.ToString());
                }
                catch (BlockMatrix.UnmatchedSizeException)
                {
                    Console.WriteLine("Matrices must have compatible sizes for addition!");
                }
            }

            private void MultiplyMatrices()
            {
                if (matrices.Count < 2)
                {
                    Console.WriteLine("Insufficient matrices to perform operation.");
                    return;
                }
                
            Console.WriteLine("-PERFORM MULTIPLICATION-");
                int index1 = GetMatrixIndex();
                int index2 = GetMatrixIndex();

                if (index1 == -1 || index2 == -1)
                {
                    Console.WriteLine("Invalid matrix indices.");
                    return;
                }

                BlockMatrix matrix1 = matrices[index1];
                BlockMatrix matrix2 = matrices[index2];

                try
                {
                    BlockMatrix product = BlockMatrix.Multiply(matrix1, matrix2);
                    Console.WriteLine("Product:\n" + product.ToString());
                }
                catch (BlockMatrix.UnmatchedSizeException)
                {
                    Console.WriteLine("Matrices must have compatible sizes for multiplication!");
                }
            }

            private void PrintMatrix()
            {
                int index = GetMatrixIndex();

                if (index == -1)
                {
                    Console.WriteLine("No matrix in the database!");
                    return;
                }

                Console.WriteLine(matrices[index].ToString());
            }
    
        }
    }