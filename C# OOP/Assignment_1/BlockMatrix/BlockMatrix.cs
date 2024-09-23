using System;
using System.Collections.Generic;

namespace BlockMatrix
{
    public class BlockMatrix
    {
        private List<int> block1;
        private List<int> block2;
        public int b1, b2, n;

        public class InvalidSizeException : Exception { };
        public class UnmatchedSizeException : Exception { };
        public class OutOfBlockException : Exception { };


        private bool isValidSize(int b1, int b2, int n)
        {
            return b1 >= 1 && b1 <= n - 1 && b2 >= 1 && b2 <= n - 1 && b1 + b2 == n;
        }


        public BlockMatrix(int b1, int b2, int n)
        {
            if (!isValidSize(b1, b2, n))
                throw new InvalidSizeException();
            this.b1 = b1;
            this.b2 = b2;
            this.n = n;
            block1 = new List<int>(b1 * b1);
            block2 = new List<int>(b2 * b2);
        }

        public int GetEntry(int i, int j)
        {
            if (i < b1 && j < b1)
                return block1[i * b1 + j]; //accessing: i*number of column + j
            else if (i >= b1 && j >= b1)
                return block2[(i - b1) * b2 + (j - b1)]; //i-b1 for the offsets. (since after b1 is gonna be b2 anyway)
            else
                return 0;
        }

        public void SetEntry(int i, int j, int value)
        {
            if (i >= 0 && i < b1 && j >= 0 && j < b1)
            {
                block1.Add(value);
            }
            else if (i >= b1 && i < n && j >= b1 && j < n)
            {
                block2.Add(value);
            }
            else if (value != 0)
            {
                throw new OutOfBlockException();
            }
        }

        public static BlockMatrix Adding(BlockMatrix a, BlockMatrix b)
    {
        if (a.n == b.n && a.b1 == b.b1 && a.b2 == b.b2)
        {
            BlockMatrix sum = new BlockMatrix(a.b1, a.b2, a.n);
            for (int i = 0; i < a.n; i++)
            {
                for (int j = 0; j < a.n; j++)
                {
                    int elementSum = a.GetEntry(i, j) + b.GetEntry(i, j); 
                    sum.SetEntry(i, j, elementSum); 
                }
            }
            return sum; 
        }
        else
        {
            throw new UnmatchedSizeException(); 
        }
    }


        public static BlockMatrix Multiply(BlockMatrix a, BlockMatrix b)
        {
            if (a.n == b.n && a.b1 == b.b1 && a.b2 == b.b2)
            {
                BlockMatrix product = new BlockMatrix(a.b1, a.b2, a.n);
                for (int i = 0; i < a.n; i++)
                {
                    for (int j = 0; j < a.n; j++)
                    {
                        int ans = 0;
                        for (int k = 0; k < a.n; k++)
                        {
                            ans += a.GetEntry(i, k) * b.GetEntry(k, j);
                        }
                        product.SetEntry(i, j, ans);
                    }
                }

                return product;
            }
            else
            {
                throw new UnmatchedSizeException();
            }
        }

        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    output += GetEntry(i, j) + " ";
                }
                output += "\n";
            }
            return output;
        }
    }
}
