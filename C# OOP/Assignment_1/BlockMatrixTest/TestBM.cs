using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlockMatrix;

namespace BlockMatrixTests
{
    [TestClass]
    public class BlockMatrixTests
    {
        [TestMethod]
        public void TestOperations()
        {
            TestCreateMatrix();
            TestGetEntry();
            TestSummation();
            TestMultiply();
        }

     [TestMethod]
        public static void TestCreateMatrix()
    {

        int expectedB1 = 2;
        int expectedB2 = 3;
        int expectedN = 5;

        BlockMatrix.BlockMatrix matrix = new BlockMatrix.BlockMatrix(expectedB1, expectedB2, expectedN);
        Assert.AreEqual(expectedB1, matrix.b1);
        Assert.AreEqual(expectedB2, matrix.b2);
        Assert.AreEqual(expectedN, matrix.n);
    }

 [TestMethod]
        public static void TestGetEntry()
        {
            BlockMatrix.BlockMatrix matrix = new BlockMatrix.BlockMatrix(2, 1, 3);
            MatrixFiller(matrix, 3);

            Assert.AreEqual(3, matrix.GetEntry(0, 0));
            Assert.AreEqual(4, matrix.GetEntry(0, 1));
            Assert.AreEqual(0, matrix.GetEntry(0, 2));
            Assert.AreEqual(5, matrix.GetEntry(1, 0));
            Assert.AreEqual(6, matrix.GetEntry(1, 1));
            Assert.AreEqual(0, matrix.GetEntry(1, 2));
            Assert.AreEqual(0, matrix.GetEntry(2, 0));
            Assert.AreEqual(0, matrix.GetEntry(2, 1));
            Assert.AreEqual(3, matrix.GetEntry(2, 2));
        }

     [TestMethod]
        public static void TestSummation()
        {
            BlockMatrix.BlockMatrix matrixA = new BlockMatrix.BlockMatrix(2, 1, 3);
            BlockMatrix.BlockMatrix matrixB = new BlockMatrix.BlockMatrix(2, 1, 3);
            MatrixFiller(matrixA, 1);
            MatrixFiller(matrixB, 2);

            BlockMatrix.BlockMatrix sum = BlockMatrix.BlockMatrix.Adding(matrixA, matrixB);

            Assert.AreEqual(3, sum.GetEntry(0, 0));
            Assert.AreEqual(5, sum.GetEntry(0, 1));
            Assert.AreEqual(0, sum.GetEntry(0, 2));
            Assert.AreEqual(7, sum.GetEntry(1, 0));
            Assert.AreEqual(9, sum.GetEntry(1, 1));
            Assert.AreEqual(0, sum.GetEntry(1, 2));
            Assert.AreEqual(0, sum.GetEntry(2, 0));
            Assert.AreEqual(0, sum.GetEntry(2, 1));
            Assert.AreEqual(3, sum.GetEntry(2, 2));
        }

     [TestMethod]
    public static void TestMultiply()
    {

    int b1 = 2;
    int b2 = 2;
    int n = 4; 

    BlockMatrix.BlockMatrix matrixA = new BlockMatrix.BlockMatrix(b1, b2, n);
    BlockMatrix.BlockMatrix matrixB = new BlockMatrix.BlockMatrix(b1, b2, n);

    MatrixFiller(matrixA, 1);
    MatrixFiller(matrixB, 2);
  
    int[,] expectedResult = new int[,] {
        { 10, 13 },   
        { 22, 29 },   
        { 0, 0 },  
        { 0, 0 },  
    };

    BlockMatrix.BlockMatrix product = BlockMatrix.BlockMatrix.Multiply(matrixA, matrixB);

    Assert.AreEqual(expectedResult[0, 0], product.GetEntry(0, 0));
    Assert.AreEqual(expectedResult[0, 1], product.GetEntry(0, 1));
    Assert.AreEqual(expectedResult[1, 0], product.GetEntry(1, 0));
    Assert.AreEqual(expectedResult[1, 1], product.GetEntry(1, 1));
    Assert.AreEqual(expectedResult[2, 0], product.GetEntry(2, 0));
    Assert.AreEqual(expectedResult[2, 1], product.GetEntry(2, 1));
    Assert.AreEqual(expectedResult[3, 0], product.GetEntry(3, 0));
    Assert.AreEqual(expectedResult[3, 1], product.GetEntry(3, 1));
    }


        private static void MatrixFiller(BlockMatrix.BlockMatrix matrix, int offset)
    {
        for (int i = 0; i < matrix.n; i++)
        {
            for (int j = 0; j < matrix.n; j++)
            {   
                if (i >= 0 && i < matrix.b1 && j >= 0 && j < matrix.b1)
                {
                    matrix.SetEntry(i, j, i * matrix.b1 + j + offset);
                }
                else if (i >= matrix.b1 && i < matrix.n && j >= matrix.b1 && j < matrix.n)
                {
                    matrix.SetEntry(i, j, (i - matrix.b1) * matrix.b2 + (j - matrix.b1) + offset);
                }
            }
        }
    }

         [TestMethod]
        public void TestExceptions()
        {
            TestInvalidSizeException();
            TestUnmatchedSizeException();
            TestOutOfBlockException();
        }
        
         [TestMethod]
        public static void TestInvalidSizeException()
        {
            try
            {
                BlockMatrix.BlockMatrix matrix = new BlockMatrix.BlockMatrix(2, 3, 6);
                Assert.Fail("InvalidSizeException was not thrown.");
            }
            catch (BlockMatrix.BlockMatrix.InvalidSizeException)
            {
            }
            catch (Exception ex)
            {
                Assert.Fail($"Wrong exception: {ex.Message}");
            }
        }
        
         [TestMethod]
        public static void TestUnmatchedSizeException()
        {
            try
            {
                BlockMatrix.BlockMatrix matrixA = new BlockMatrix.BlockMatrix(2, 1, 3);
                BlockMatrix.BlockMatrix matrixB = new BlockMatrix.BlockMatrix(2, 2, 4);
                BlockMatrix.BlockMatrix sum = BlockMatrix.BlockMatrix.Adding(matrixA, matrixB);
                Assert.Fail("UnmatchedSizeException was not thrown.");
            }
            catch (BlockMatrix.BlockMatrix.UnmatchedSizeException)
            {

            }
            catch (Exception ex)
            {
                Assert.Fail($"Wrong exception: {ex.Message}");
            }
        }
        
         [TestMethod]
        public static void TestOutOfBlockException()
        {
            try
            {
                BlockMatrix.BlockMatrix matrix = new BlockMatrix.BlockMatrix(2, 1, 3);
                matrix.SetEntry(0, 7, 10);
                matrix.SetEntry(2, 90, 20);
                Assert.Fail("OutOfBlockException was not thrown.");
            }
            catch (BlockMatrix.BlockMatrix.OutOfBlockException)
            {

            }
            catch (Exception ex)
            {
                Assert.Fail($"Wrong exception: {ex.Message}");
            }
        }
    }
}
    
