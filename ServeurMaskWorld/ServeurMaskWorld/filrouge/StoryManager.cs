using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA
{
    class StoryManager
    {
		/*
		* constructor
		*/
		public StoryManager()
		{
		}

		/*
		* Display the text on the console and erase the line which was before
		*/
		public void displayText(string s)
		{
			Console.Write(s);
			Console.Write("\n");
			Console.Write("\rPress enter to continue...");
			Console.Read();
			//this thing "\033[A\33[2K" is to delete the line press enter to continue
			Console.Write("\x001B[A\x001B[2K");
		}

		/*
		* Display the choice and manage the choice that the players input
		*/
		public bool choiceText(string sChoice1, string sChoice2)
		{
			Console.Write(sChoice1);
			Console.Write("\n");
			Console.Write(sChoice2);
			Console.Write("\n");


			while (true)
			{
				char input;
				input = Console.ReadKey(true).KeyChar;
				if (input == '1')
				{
					Console.Write("Choix 1");
					Console.Write("\n");
					return true;
				}
				else if (input == '2')
				{
					Console.Write("Choix 2");
					Console.Write("\n");
					return false;
				}
				else
				{
					Console.Write("Mauvaise touche presse, ressayer :");
					Console.Write("\n");
				}
			}
		}


	}
}
