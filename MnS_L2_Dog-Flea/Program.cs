namespace MnS_L2_Dog_Flea
{
	using System;

	class DogFleaModel
	{
		private static readonly Random random = new();

		// Simulates one iteration of the model
		static (int, int) Transition((int, int) state)
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

			return (fleasOnDogA, fleasOnDogB);
		}

		// Runs a single simulation for specific amount of iterations
		static int RunSimulation(int n, int iterations)
		{
			(int, int) state = (n, 0); // Initial state X(0) = (20,0)
			int returnIteration = 0;
			Console.WriteLine("i\tRandom Number\tx(i)");

			for (int i = 1; i <= iterations; i++)
			{
				int randomNumber = random.Next(n); // Random number for transition
				state = Transition(state);
				Console.WriteLine($"{i}\t{randomNumber}\t\t({state.Item1}, {state.Item2})");
				if (state == (n, 0))
				{
					Console.WriteLine($"The model has returned to its initial state at iteration {i}.");
					returnIteration = i;
					break;
				}
			}

			if (state == (n, 0)) return returnIteration; else return 0;
		}

		// Runs 20 simulations and averages the return times
		static (double, double) CalculateAverageReturnToInitialState(int n, int simulations, int iterations)
		{
			int totalIterations = 0;
			int simulationsToReturn = 0;

			for (int i = 0; i < simulations; i++)
			{
				Console.WriteLine($"\nSimulation {i + 1}:");
				int iterationsToReturn = RunSimulation(n, iterations);
                simulationsToReturn += iterationsToReturn > 0 ? 1 : 0;
				totalIterations += iterationsToReturn;
			}

			return (simulationsToReturn, (double)totalIterations / simulations);
		}

		static void Main()
		{
			int n = 20;
			int simulations = 20;
			int iterations = 20;

			Console.WriteLine("Running 20 simulations of the Dog-Flea Model:");
			(double, double) averageReturn = CalculateAverageReturnToInitialState(n, simulations, iterations);
			Console.WriteLine($"\nValue of simulations when the model has returned to initial state: {averageReturn.Item1}");
			Console.WriteLine($"Average value of iterations to return to initial state: {averageReturn.Item2}");
		}
	}


}
