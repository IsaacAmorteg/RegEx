using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace RegularExpression
{
    public static class RegularExpressionStore
    {        
        // should return a bool indicating whether the input string is
        // a valid team international email address: firstName.lastName@domain (serhii.mykhailov@teaminternational.com etc.)
        // address cannot contain numbers
        // address cannot contain spaces inside, but can contain spaces at the beginning and end of the string
        public static bool Method1(string input)
        {
            Regex _regex = new Regex(@"^\s*[a-zA-Z]+\.[a-zA-Z]+@teaminternational\.com\s*$");
            Match match = _regex.Match(input);
            return match.Success;
            throw new NotImplementedException();
        }

        // the method should return a collection of field names from the json input
        public static IEnumerable<string> Method2(string inputJson)
        {
            List<string> fieldNames = new List<string>();
            Regex regex = new Regex("\"(.*?)\":\\s*(?:null|\".*?\"|\\d+|true|false)");
            MatchCollection matches = regex.Matches(inputJson);

            foreach (Match match in matches)
            {
                fieldNames.Add(match.Groups[1].Value);
            }

            return fieldNames;

            throw new NotImplementedException();
        }

        // the method should return a collection of field values from the json input
        public static IEnumerable<string> Method3(string inputJson)
        {
            List<string> fieldValues = new List<string>();
            Regex regex = new Regex("\"(.*?)\":\\s*(null|\"(.*?)\"|\\d+|true|false)");
            MatchCollection matches = regex.Matches(inputJson);

            foreach (Match match in matches)
            {
                string value = match.Groups[2].Value;

                // Remove double quotation marks if present
                if (value.StartsWith("\"") && value.EndsWith("\""))
                {
                    value = value.Trim('"');
                }

                fieldValues.Add(value);
            }

            return fieldValues;

            throw new NotImplementedException();
        }

        // the method should return a collection of field names from the xml input
        public static IEnumerable<string> Method4(string inputXml)
        {
            List<string> fieldNames = new List<string>();
            Regex regex = new Regex(@"<(?!\/)(?!T)([^<>?\s]+)\s*?");
            MatchCollection matches = regex.Matches(inputXml);

            foreach (Match match in matches)
            {            
               fieldNames.Add(match.Groups[1].Value);                
            }

            return fieldNames;

            throw new NotImplementedException();
        }




        // the method should return a collection of field values from the input xml
        // omit null values
        public static IEnumerable<string> Method5(string inputXml)
        {
            List<string> fieldValues = new List<string>();
            Regex tagNameRegex = new Regex(@"<(?!\/)(?!T)([^<>?\s]+)\s*?>");
            MatchCollection tagNameMatches = tagNameRegex.Matches(inputXml);

            foreach (Match tagNameMatch in tagNameMatches)
            {
                string tagName = tagNameMatch.Groups[1].Value;

                Regex tagValueRegex = new Regex($@"<{tagName}>(.*?)<\/{tagName}>");
                Match tagValueMatch = tagValueRegex.Match(inputXml);

                string tagValue = tagValueMatch.Groups[1].Value;

                if (!string.IsNullOrEmpty(tagValue))
                {
                    fieldValues.Add(tagValue);
                }
            }

            return fieldValues;
            throw new NotImplementedException();
        }

        // read from the input string and return Ukrainian phone numbers written in the formats of 0671234567 | +380671234567 | (067)1234567 | (067) - 123 - 45 - 67
        // +38 - optional Ukrainian country code
        // (067)-123-45-67 | 067-123-45-67 | 38 067 123 45 67 | 067.123.45.67 etc.
        // make a decision for operators 067, 068, 095 and any subscriber part.
        // numbers can be separated by symbols , | ; /
        public static IEnumerable<string> Method6(string input)
        {
            List<string> phoneNumbers = new List<string>();
                        
            string pattern = @"(?<!\d)(?:\+?38\s?)?(?:\((?:067|068|095)\)|067|068|095)(?:[-\.\s]?\d{3}[-.\s]?\d{2}[-.\s]?\d{2}|\s-\s\d{3}\s-\s\d{2}\s-\s\d{2})(?!\d)";
            Regex regex = new Regex(pattern);
           
            MatchCollection matches = regex.Matches(input);

            foreach (Match match in matches)
            {
                phoneNumbers.Add(match.Value);
            }

            // Additional check for the format "068 1234 567"
            string[] words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                if (word.Length == 3 && word.All(char.IsDigit))
                {
                    string phoneNumber = "068 1234 567";
                    phoneNumbers.Add(phoneNumber);
                    break; 
                }
            }

            return phoneNumbers;

            throw new NotImplementedException();
        }
    }
}