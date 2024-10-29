namespace MnS_L2_Dog_Flea
{
	using System;

	class DogFleaModel
	{
		private static readonly Random random = new();

		// Simulates one iteration of the model
		static (int, (int,int)) Transition((int, int) state)
		{
			int fleasOnDogA = state.Item1;
			int fleasOnDogB = state.Item2;

			int randomFlea = random.Next(fleasOnDogA + fleasOnDogB);

			if (randomFlea < fleasOnDogA)
			{
				fleasOnDogA--;
				fleasOnDogB++;
			}
			else
			{
				fleasOnDogA++;
				fleasOnDogB--;
			}

			return (randomFlea, (fleasOnDogA, fleasOnDogB));
		}

        // Runs a single simulation for specific amount of iterations
        static long RunSimulation(int n, long iterations, bool mode)
        {
            (int, int) state = (n, 0); // Initialize initial state of X(0)
			long returnIteration = 0;
			if (mode) { Console.WriteLine("i\tx(i-1)\t\tRandom Number\t\tx(i)"); }

            for (int i = 1; i <= iterations; i++)
            {
				if (mode) { Console.Write($"{i}\t({state.Item1}, {state.Item2})\t\t"); }
				int randomNumber;
				(randomNumber, state) = Transition(state);
				if (mode) { Console.Write($"{randomNumber}\t\t({state.Item1}, {state.Item2})\n"); }
				//else { Console.Write(i); Console.SetCursorPosition(Console.CursorLeft - i.ToString().Length, Console.CursorTop); }
				if (state == (n,0)) { returnIteration = i; break; }
            }
			return returnIteration;
        }

        // Runs 20 simulations and averages the return times
        static (double, double) CalculateAverageReturnToInitialState(int n, int simulations, long iterations, bool mode)
		{
			long totalIterations = 0;
			long iterationsToReturn = 0;

			for (int i = 0; i < simulations; i++)
			{
				Console.WriteLine($"\nSimulation {i + 1}: ");
				iterationsToReturn = RunSimulation(n, iterations, mode);
				totalIterations += iterationsToReturn;
				Console.Write(iterationsToReturn);
			}

			return (iterationsToReturn, (double)totalIterations / simulations);
		}

		static void Main()
		{
			int n = 20;
			int simulations = 20;
			int iterations = 20;

			Console.WriteLine("Running 20 simulations of the Dog-Flea Model:");
			RunSimulation(n, iterations, true);
            (double, double) averageReturn = CalculateAverageReturnToInitialState(n, simulations, long.MaxValue, false);
            Console.WriteLine($"\nAverage value of iterations to return to initial state: {averageReturn.Item2}");
		}
	}


}
