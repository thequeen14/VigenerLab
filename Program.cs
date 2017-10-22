using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace VigenerCipher
{
    class Program
    {
        public static string[,] vigenerTable = new string[26,26];

        static void Main(string[] args)
        {
            FillVigenerTable();
            PrintVigenerTable();

            var input = ReadPlainText("text.txt");
            Console.WriteLine("Введите ключ: ");
            var key = Console.ReadLine();
            var encryptedInput = Encrypt(input, key);
            Console.WriteLine("Что получилось: {0}", encryptedInput);
            var decryptedInput = Decrypt(encryptedInput, key);
            Console.WriteLine("Возвращаем обратно: {0}", decryptedInput);

            Console.ReadKey();
        }

        public static void FillVigenerTable()
        {
            var line = "abcdefghijklmnopqrstuvwxyz";
            var currentIndex = 0;
            for (int i = 0; i < 26; i++)
            {
                var j = 0;
                for (int jInLine = currentIndex; jInLine < 26; jInLine++)
                {
                    vigenerTable[i, j] = line[jInLine].ToString();
                    j++;
                }
                for (int jInLine = 0; jInLine < currentIndex; jInLine++)
                {
                    vigenerTable[i, j] = line[jInLine].ToString();
                    j++;
                }
                currentIndex++;
            }
        }

        public static void PrintVigenerTable()
        {
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    Console.Write(vigenerTable[i, j] + " ");
                }
                Console.Write("\r\n");
            }
        }

        public static string ReadPlainText(string path)
        {
            var result = string.Empty;
            using (var sr = new StreamReader(path))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }

        public static int GetLetterNumber(char letter)
        {
            var result = 0;
            var line = "abcdefghijklmnopqrstuvwxyz";
            for (int i = 0; i < 26; i++)
            {
                if (letter == line[i])
                {
                    result = i;
                    break;
                }
            }
            return result;
        }

        public static string Encrypt(string plainText, string key)
        {
            var currentIndexInKey = 0;
            var result = string.Empty;
            foreach (char c in plainText)
            {
                var i = GetLetterNumber(key[currentIndexInKey]);
                var j = GetLetterNumber(c);
                result += vigenerTable[i, j];
                currentIndexInKey++;
                if (currentIndexInKey == key.Length)
                {
                    currentIndexInKey = 0;
                }
            }
            return result;
        }

        public static string Decrypt(string ciphertext, string key)
        {
            var currentIndexInKey = 0;
            var result = string.Empty;
            foreach (char c in ciphertext)
            {
                var i = GetLetterNumber(key[currentIndexInKey]);
                char nextDecryptedSymbol = ' ';
                for (int j = 0; j < 26; j++)
                {
                    if (c.ToString() == vigenerTable[i, j])
                    {
                        nextDecryptedSymbol = vigenerTable[0, j].ToCharArray()[0];
                    }
                }
                result += nextDecryptedSymbol.ToString();
                currentIndexInKey++;
                if (currentIndexInKey == key.Length)
                {
                    currentIndexInKey = 0;
                }
            }
            return result;
        }
    }
}
