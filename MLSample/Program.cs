using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Selections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Domain.Randomizations;

namespace MLSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var selection = new TournamentSelection();
            var crossover = new OrderBasedCrossover();
            var mutation = new ReverseSequenceMutation();
            var fitness = new SortFitness();
            var chromosomes = new SortChromosomes();
            var population = new Population(40, 40, chromosomes);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new GenerationNumberTermination(100);


            Console.WriteLine("GA running...");

            ga.Start();
            ga.GenerationRan += Ga_GenerationRan;
            Console.WriteLine("GA done in {0} generations.", ga.GenerationsNumber);

            var bestChromosome = ga.BestChromosome as SortChromosomes;
            //Console.WriteLine("Best solution found is X:{0}, Y:{1} with {2} fitness.", bestChromosome.X, bestChromosome.Y, bestChromosome.Fitness);

            Console.WriteLine("Best solution found");

            DisplayChromosome(bestChromosome);
            Console.ReadKey();
        }

        private static void DisplayChromosome(IChromosome chromosome)
        {
            var deck = (chromosome as SortChromosomes).CardDeck;
            var diff = deck.Zip(deck.Skip(1), (x, y) => Math.Abs(Convert.ToInt32(x.Value) - Convert.ToInt32(y.Value)));

            var fitness = 1.0 - (diff.Sum() / (13.0 * 1000.0));

            if (fitness < 0)
            {
                fitness = 0.0;
            }


            Console.WriteLine("fitness: " + fitness);

            foreach (var val in (chromosome as SortChromosomes).CardDeck)
            {
                Console.Write(val.Value);
                Console.Write(" ");
            }
        }

        private static void Ga_GenerationRan(object sender, EventArgs e)
        {
            Console.WriteLine("GenerationRan...");
        }
    }

}
