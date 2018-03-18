using System;
using System.Text;
using System.Text.RegularExpressions;

namespace PGSRecruitmentTask
{
    class PersonalDataCollector
    {
        //Properties to store data, could be read anywhere, set only in this class
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string HouseNum { get; private set; }
        public string FlatNum { get; private set; }
        public string ZipCode { get; private set; }
        public string PhoneNumber { get; private set; }

        public PersonalDataCollector() {}

        /// <summary>
        /// Check if parameter is valid, if yes -> store first name
        /// </summary>
        /// <param name="input">user input</param>
        /// <returns></returns>
        public bool TakeName(string input)
        {
            if (input.Length == 0)
                return false;
            if (lettersValidate(input))
            {
                FirstName = input;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if parameter is valid, if yes -> store last name
        /// </summary>
        /// <param name="input">user input</param>
        /// <returns></returns>
        public bool TakeLastName(string input)
        {
            if (input.Length == 0)
                return false;
            if (lettersValidate(input))
            {
                LastName = input;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if all strings are valid, if yes -> store address
        /// </summary>
        /// <param name="input">table of address inputs</param>
        /// <returns></returns>
        public bool TakeAddress(string[] input)
        {
            //fieldsCorrect == true when ALL inputs in address tab are valid
            bool fieldsCorrect = lettersValidate(input[0])
                                    && lettersAndDigitsValidate(input[1])
                                    && (lettersAndDigitsValidate(input[2]) || (input[2].Length == 0))
                                    && lettersAndDigitsValidate(input[3])
                                    && (Regex.IsMatch(input[4], @"^[0-9-]+$"));
            if (fieldsCorrect)
            {
                Street = input[0];
                HouseNum = input[1];
                FlatNum = input[2].Length != 0 ? input[2] : "";
                City = input[3];
                ZipCode = input[4];
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if parameter is valid, if yes -> store phone number
        /// </summary>
        /// <param name="input">user input</param>
        /// <returns></returns>
        public bool TakePhoneNumber(string input)
        {
            if (input.Length == 0)
                return false;
            if (Regex.IsMatch(input, @"^[0-9+]+$"))
            {
                PhoneNumber = input;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Show all captured informations
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //When flat num is empty, do not append slash
            string slash = FlatNum.Length == 0 ? "" : "/";
            StringBuilder tmp = new StringBuilder();
            tmp.AppendLine("Imię: " + FirstName);
            tmp.AppendLine("Nazwisko: " + LastName);
            tmp.AppendLine("Adres: " + Street + " " + HouseNum + slash + FlatNum + " " + City + " " + ZipCode);
            tmp.AppendLine("Telefon: " + PhoneNumber);
            return tmp.ToString();
        }

        /// <summary>
        /// Check if letters and digits are in argument string
        /// </summary>
        /// <param name="input">string to check</param>
        /// <returns></returns>
        private bool lettersAndDigitsValidate(string input)
        {
            if (Regex.IsMatch(input, @"^[0-9a-ząćęłńóśżźA-ZĄĆĘŁŃÓŚŹŻ]+$"))
                return true;
            return false;
        }

        /// <summary>
        /// Check if only letters are in argument string
        /// </summary>
        /// <param name="input">string to check</param>
        /// <returns></returns>
        private bool lettersValidate(string input)
        {
            if (Regex.IsMatch(input, @"^[a-ząćęłńóśżźA-ZĄĆĘŁŃÓŚŹŻ]+$"))
                return true;
            return false;
        }
    }
}
