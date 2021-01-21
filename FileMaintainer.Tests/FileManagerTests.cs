using System;
using System.IO;
using Xunit;

namespace FileMaintainer.Tests
{
    public class FileManagerTests
    {
        #region snippet_GatherFiles_Passes_InputIsCorrect

        [Fact]
        public void GatherFiles_Passes_InputIsCorrect()
        {
            // Arrange
            const string fileName1 = "test1.txt";
            const string fileName2 = "test2.txt";
            const string fileName3 = "test3.txt";

            var data = new byte[10 * 1024 * 1024];
            var rng = new Random();
            rng.NextBytes(data);
            File.WriteAllBytes(fileName1, data);
            File.WriteAllBytes(fileName2, data);
            File.WriteAllBytes(fileName3, data);

            // Act
            FileManager.GatherFiles(fileName1,fileName2,fileName3);


            // Assert
            Assert.True(File.Exists("gathered.txt"));
        }

        #endregion

        #region snippet_GatherFiles_ThrowsArgumentNullException_InputIsNull

        [Fact]
        public void GatherFiles_ThrowsArgumentNullException_InputIsNull()
        {
            // Arrange&&Act
            static void Result()=>
            FileManager.GatherFiles(null);


            // Assert
            Assert.Throws<ArgumentNullException>(Result);
        }

        #endregion

        #region snippet_SplitFiles_Passes_InputIsCorrect

        [Fact]
        public void SplitFiles_Passes_InputIsCorrect()
        {
            // Arrange
            const string fileName1 = "test1.txt";

            var data = new byte[10 * 1024 * 1024];
            var rng = new Random();
            rng.NextBytes(data);
            File.WriteAllBytes(fileName1, data);

            // Act
            FileManager.SplitFiles(fileName1,2);


            // Assert
            Assert.True(File.Exists("split0.txt"));
            Assert.True(File.Exists("split1.txt"));
        }

        #endregion

        #region snippet_SplitFiles_ThrowsArgumentNullException_InputIsNull

        [Fact]
        public void SplitFiles_ThrowsArgumentNullException_InputIsNull()
        {
            // Arrange&&Act
            static void Result() =>
                FileManager.SplitFiles(null, 2);


            // Assert
            Assert.Throws<ArgumentNullException>(Result);
        }

        #endregion

        #region snippet_SplitFiles_ThrowsArgumentOutOfRangeException_InputPartsIs0OrLess

        [Fact]
        public void SplitFiles_ThrowsArgumentOutOfRangeException_InputIsNull()
        {
            // Arrange&&Act
            static void Result() =>
                FileManager.SplitFiles("null", -1);


            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(Result);
        }

        #endregion

        #region snippet_DeleteSubstring_Passes_InputIsCorrect

        [Fact]
        public void DeleteSubstring_Passes_InputIsCorrect()
        {
            // Arrange
            const string fileName1 = "test1.txt";

            const string text = "123456789";
            File.WriteAllText(fileName1, text);

            // Act
            FileManager.DeleteSubstring(fileName1, "789");


            // Assert
            Assert.Equal("123456",File.ReadAllText(fileName1));
        }

        #endregion

        #region snippet_DeleteSubstring_ThrowsArgumentNullException_InputIsNull

        [Fact]
        public void DeleteSubstring_ThrowsArgumentNullException_InputIsNull()
        {
            // Arrange&&Act
            static void Result() =>
                FileManager.DeleteSubstring(null,null);


            // Assert
            Assert.Throws<ArgumentNullException>(Result);
        }

        #endregion

        #region snippet_PrintTextFromFiles_ThrowsArgumentNullException_InputIsNull

        [Fact]
        public void PrintTextFromFiles_ThrowsArgumentNullException_InputIsNull()
        {
            // Arrange&&Act
            static void Result() =>
                FileManager.PrintTextFromFiles(null,SearchOption.AllDirectories, null);


            // Assert
            Assert.Throws<ArgumentNullException>(Result);
        }

        #endregion

    }
}