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
            var selection = new EliteSelection();
            var crossover = new OnePointCrossover(0);
            var mutation = new UniformMutation(true);
            var fitness = new MyFitness();
            var chromosomes = new MyProblemChromosomes();
            var population = new Population(50, 50, chromosomes);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new GenerationNumberTermination(100);


            Console.WriteLine("GA running...");

            ga.Start();

            Console.WriteLine("GA done in {0} generations.", ga.GenerationsNumber);

            var bestChromosome = ga.BestChromosome as MyProblemChromosomes;
            Console.WriteLine("Best solution found is X:{0}, Y:{1} with {2} fitness.", bestChromosome.X, bestChromosome.Y, bestChromosome.Fitness);
            Console.ReadKey();
        }
    }

    public class MyProblemChromosomes : ChromosomeBase
    {
        public MyProblemChromosomes() : base(2)
        {
            ReplaceGene(0, GenerateGene(0));
            ReplaceGene(1, GenerateGene(1));
        }

        public int X
        {
            get
            {
                return (int)GetGene(0).Value;
            }
        }

        public int Y
        {
            get
            {
                return (int)GetGene(1).Value;
            }
        }

        public override Gene GenerateGene(int geneIndex)
        {
            int value;

            if (geneIndex == 0)
            {
                value = RandomizationProvider.Current.GetInt(0, 8);
            }
            else
            {
                value = RandomizationProvider.Current.GetInt(0, 101);
            }

            return new Gene(value);
        }

        public override IChromosome CreateNew()
        {
            return new MyProblemChromosomes();
        }
    }

    public class MyFitness : IFitness
    {
        public double Evaluate(IChromosome chromosome)
        {
            double n = 9;
            var x = (int)chromosome.GetGene(0).Value;
            var y = (int)chromosome.GetGene(1).Value;
            double f1 = System.Math.Pow(15 * x * y * (1 - x) * (1 - y) * System.Math.Sin(n * System.Math.PI * x) * System.Math.Sin(n * System.Math.PI * y), 2);
            Console.WriteLine(string.Format("X = {0}, Y= {1}, Fitness: {2}", x, y, f1.ToString()));
            return f1;
        }
    }
}
