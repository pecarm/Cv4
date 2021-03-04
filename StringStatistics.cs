using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cv4
{
    class StringStatistics
    {
        #region Attributes
        private string input;
        private string[] words;
        #endregion

        #region Constructor
        public StringStatistics(string strinpt)
        {
            input = strinpt;
            words = SplitToWords(strinpt);
        }
        #endregion

        #region Methods
        public int CountWords()
        {
            return words.Length;
        }

        public int CountRows()
        {
            //this method counts an empty row as a row as well which means 
            //"lorem ipsum dolor\n" +
            //"\n" +
            //"sit amet"
            //counts as 3 separate rows
            return input.Split('\n').Length;
        }

        public int CountSentences()
        {
            string regex = @"(\.|\?|!)\s[A-Z]";
            MatchCollection matches = Regex.Matches(input, regex);
            
            return matches.Count + 1;
        }

        public string[] LongestWords()
        {
            int maxlength = 0;
            foreach (string word in words)
            {
                if (word.Length > maxlength) maxlength = word.Length;
            }

            //SAME AS ShortestWords()
            string[] longwords = words.Where(word => word.Length == maxlength).ToArray();

            return longwords;
        }

        public string[] ShortestWords()
        {
            int minlength = int.MaxValue;
            foreach (string word in words)
            {
                if (word.Length < minlength) minlength = word.Length;
            }

            //NO NEED to use .Select(word => word.Length == minlength : word ? "").Where(word => word != "").ToArray();
            string[] shortwords = words.Where(word => word.Length == minlength).ToArray();

            return shortwords;
        }
        
        public string[] MostCommonWords()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (string word in words)
            {
                if (dictionary.ContainsKey(word))
                {
                    dictionary[word]++;
                }
                else
                {
                    dictionary.Add(word, 1);
                }
            }

            int mostapppearances = 0;
            foreach (var item in dictionary)
            {
                if (item.Value > mostapppearances)
                {
                    mostapppearances = item.Value;
                }
            }

            //LINQ used AGAIN, filtered instances of KeyValuePair based on their Value, then selected their Keys

            return dictionary.Where(word => word.Value == mostapppearances).Select(word => word.Key).ToArray();

            /*string[] commonwords = new string[dictionary.Count];
            int iterator = 0;
            foreach (var item in dictionary)
            {
                if (item.Value == mostapppearances)
                {
                    commonwords[iterator] = item.Key;
                    iterator++;
                }
            }

            Array.Resize(ref commonwords, iterator);

            return commonwords;*/
        }

        public string[] AlphSort()
        {
            string[] temp = words;
            Array.Sort(temp);
            return temp;
        }

        public bool IsInfected()
        {
            foreach (string word in words)
            {
                switch (word.ToLower())
                {
                    case "covid":
                    case "covid-19":
                    case "sars-cov-2":
                        return true;
                    default:
                        break;
                }
            }
            return false;
        }

        private string[] SplitToWords(string text)
        {
            //THIS METHOD splits the input string to an array of strings
            //it also removes all instances of " - " as these are not a word BUT keeps all words with a hyphen (e.g. "half-life")

            char[] separators = new char[] { ' ', '.', ',', '?', '!', ';', '\n', '(', ')', '/' };
            string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries).Where(word => word != "-").ToArray();

            //You can FILTER with LINQ, very POWERFUL

            
            //  TOO DIFFICULT, can be replaced with .Where
            //  |||
            //  vvv
            
            //Checks if there are any dashes and returns their indices.
            //Enumerable.Select (lambda function, returns ALL INDECES of a dash AND a -1 wherever the dash IS NOT). Where(lambda function, filters out ALL -1s, only indices of dashes remain).ToArray()
            //this returns an array of indices of all dashes THAT ARE BETWEEN 2 SPACES therefore are not a word OR returns a int[0] if there are none
            /*int[] dashesPositions = words.Select((word, index) => word == "-" ? index : -1).Where(index => index != -1).ToArray();

            //this then replaces all the solitary dashes with empty strings, makes it a long string with only spaces between words and splits it again
            if (dashesPositions.Length > 0)
            {
                for (int i = 0; i < dashesPositions.Length; i++)
                {
                    words[dashesPositions[i]] = "";
                }

                string temp = "";
                foreach (string word in words)
                {
                    temp = temp + " " + word;
                }
                words = temp.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            }*/

            return words;
        }
        #endregion
    }
}