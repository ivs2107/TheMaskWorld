using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA
{
    public class Constants
    {
        public enum Case
        {
            cEmpty = 0,
            cAllyHero = 1,
            cTarget = 2,
            cTargetAttack = 3,
            cEnnemyHero = 4
        }

        public const string LOG_FILE_PATH = "C:\\Users\\lucas.perrin1\\Downloads\\log.txt";

        //https://stackoverflow.com/questions/3219393/stdlib-and-colored-output-in-c

        public const int KEY_UP = 72;
        public const int KEY_DOWN = 80;
        public const int KEY_LEFT = 75;
        public const int KEY_RIGHT = 77;
        public const string ANSI_COLOR_RED = "\x1b[31m";
        public const string ANSI_COLOR_GREEN = "\x1b[32m";
        public const string ANSI_COLOR_YELLOW = "\x1b[33m";
        public const string ANSI_COLOR_BLUE = "\x1b[34m";
        public const string ANSI_COLOR_MAGENTA = "\x1b[35m";
        public const string ANSI_COLOR_CYAN = "\x1b[36m";
        public const string ANSI_COLOR_RESET = "\x1b[0m";


    }
}
