using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA
{
	static class InputUser
	{

		//move hero with input
		public static void  InputMovement(ConsoleKey input, Map m, Hero h)
		{

			switch (input)
			{
				case ConsoleKey.UpArrow:
					//key up
					m.movementOfHero(-1, 0, h);
					break;
				case ConsoleKey.DownArrow:
					// key down
					m.movementOfHero(1, 0, h);
					break;
				case ConsoleKey.LeftArrow:
					// key left
					m.movementOfHero(0, -1, h);
					break;
				case ConsoleKey.RightArrow:
					// key right
					m.movementOfHero(0, 1, h);
					break;
				default:
					Console.Write("\n");
					Console.Write("null");
					Console.Write("\n");
					break;
			}
		}

		//move target with input
		public static void InputMovementTarget(ConsoleKey input, Map m, Hero h)
		{

			switch (input)
			{
				case ConsoleKey.UpArrow:
					m.movementOfTargetAttackWithKey(-1, 0, h);
					break;
				case ConsoleKey.DownArrow:
					m.movementOfTargetAttackWithKey(1, 0, h);
					break;
				case ConsoleKey.LeftArrow:
					m.movementOfTargetAttackWithKey(0, -1, h);
					break;
				case ConsoleKey.RightArrow:
					m.movementOfTargetAttackWithKey(0, 1, h);
					break;
				default:
					Console.Write("\n");
					Console.Write("null");
					Console.Write("\n");
					break;
			}
		}

	}
}
