using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sandogh.App.InputValidator
{
    public static class ValidateInput
    {
        private static readonly Regex LoginRegex = new Regex(@"^(?=.{3,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9-آ-ی ء چ._]+(?<![_.])$");

        public static bool IsLoginInputValid(string username,string password) => LoginRegex.IsMatch(username)&&LoginRegex.IsMatch(password);
    }
}
