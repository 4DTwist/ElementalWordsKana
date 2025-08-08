using ElementalWordsKana;
//Project for the Kata found over here on Codewars.
//TO USE IN CODEWARS COPY THE WHOLE DOC IN "Codewars.txt". It is a copy of this with additional usings and references to the elemental dictionary used over there.
// - https://www.codewars.com/kata/56fa9cd6da8ca623f9001233/train/csharp



//Quick code to run the method and get an output to the console.
string word = "Snack";

var x = ElementalWords.ElementalForms(word);
Console.WriteLine("Elemental combinations for the word: " + word);
foreach (var outerElement in x)
{
    Console.Write("| ");
    foreach (var innerElement in outerElement)
    {
        Console.Write(innerElement + " | ");
    }
    Console.WriteLine();
}

namespace ElementalWordsKana
{
    public class ElementalWords
    {
        //Manually recreated some of the CodeWars Kata dictionary so I could test in my own IDE.
        public static readonly Dictionary<string, string> cutdownElements = new Dictionary<string, string>
            {
                {"B", "Boron" },
                {"Be","Berytllium"},
                {"S", "Sulfur" },
                {"N","Nitrogen"},
                {"Ac","Actinium"},
                {"K","Potassium"},
                {"Na","Sodium"},
                {"C","Carbon"},
                {"Sn","Tin"},
                {"Y","Yttrium" },
                {"Es", "Einsteinium"},
                {"H","Hydrogen"}
            };

        //Adds the dictionary keys to the end of the value and lower-cases the keys. Reduces string manipulation later.
        //The commented out line is for Codewars; go to Codewars.txt in this project and copy everything from there, it's easier.
        public static Dictionary<string, string> elements = cutdownElements.ToDictionary(a => a.Key.ToLower(), a => a.Value + " (" + a.Key.ToString() + ")");
        //public static Dictionary<string, string> elements = ELEMENTS.ToDictionary(a => a.Key.ToLower(), a => a.Value + " (" + a.Key.ToString() + ")");


        //Class CodeWars tries to run.
        public static string[][] ElementalForms(string word)
        {
            return ElementalFormsRecursiveStart(word);
        }

        /// <summary>
        /// Sets up and starts the recursive method.
        /// </summary>
        /// <param name="word"></param>
        /// <returns>String[][] of possible elemental combinations</returns>
        public static string[][] ElementalFormsRecursiveStart(string word)
        {

            //Basic input checking
            if (string.IsNullOrWhiteSpace(word))
            {
                word = "";
            }
            
            word = word.ToLower();

            if (word.Length == 0)
            {
                return [];
            }

            //Running this on lists since we're adding elements as we go.
            List<List<string>> outputList = new List<List<string>>();
            List<string> runningList = new List<string>();

            List<List<string>> output = ElementalFormsRecursive(word, outputList, runningList);

            //Shift the output back to the expected type, an array of string arrays.
            return output.Select(a => a.ToArray()).ToArray();
        }


        /// <summary>
        /// Goes through the word provided, finds matches in the elements dictionary and stores them in the outputList, and returns when done.
        /// </summary>
        /// <returns>Returns a list of lists.</returns>
        public static List<List<string>> ElementalFormsRecursive(string word, List<List<string>> outputList, List<string> runningList)
        {
            //If we get here with a word length of 0, we have completed a word.
            //Add it to the outputList and return. 
            if (word.Length == 0)
            {
                outputList.Add(runningList);
                return outputList;
            }

            //Check dictionary for the first 1,2 and 3 characters.
            for (int i = 1; i < 4; i++)
            {
                if (word.Length >= i)
                {
                    string key = word.Substring(0, i);
                    #nullable enable
                    string? value;
                    #nullable disable

                    if (elements.TryGetValue(key, out value))
                    {
                        //Using append here to pass through the value of the running list + new data.
                        //As a result, runningList is clean for the next loop.
                        List<string> nextRunningList = runningList.Append(value).ToList();
                        outputList = ElementalFormsRecursive(word.Substring(i), outputList, nextRunningList);
                    }
                }
                else
                {
                    //Exit loop immediately if word is lever less than i. No need to keep running it.
                    break;
                }
            }

            return outputList;
        }
    }
}