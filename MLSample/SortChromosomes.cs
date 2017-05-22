using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Randomizations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLSample
{
    public class SortChromosomes : ChromosomeBase
    {
        private const int MAX_CARD_IN_DECK = 13;
        public SortChromosomes() : base(MAX_CARD_IN_DECK)
        {
            var sequence = RandomizationProvider.Current.GetUniqueInts(MAX_CARD_IN_DECK, 1, MAX_CARD_IN_DECK + 1);

            //Console.WriteLine("Sequence: ");

            //sequence.ToList().ForEach(s => Console.Write(s + " "));

            //Console.WriteLine("Sum: " + sequence.Sum());

            for (int i = 0; i < MAX_CARD_IN_DECK; i++)
            {
                ReplaceGene(i, new Gene(sequence[i]));
            }

        }

        public Gene[] CardDeck
        {
            get
            {
                return GetGenes();
            }
        }

        public override IChromosome CreateNew()
        {
            return new SortChromosomes();
        }

        public override Gene GenerateGene(int geneIndex)
        {
            int value;

            value = RandomizationProvider.Current.GetInt(1, MAX_CARD_IN_DECK + 1);

            return new Gene(value);
        }
    }

    public class SortFitness : IFitness
    {
        private int[] CARD_SEQUENCE = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
        public double Evaluate(IChromosome chromosome)
        {
            var genes = chromosome.GetGenes();

            var sum = 0.0;

            //for (int i = 0; i < genes.Length-1; i++)
            //{
            //    sum += Convert.ToInt32(genes[i + 1].Value) - Convert.ToInt32(genes[i]);
            //}

            var difference = genes.Zip(genes.Skip(1), (x, y) => Math.Abs(Convert.ToInt32(y.Value) - Convert.ToInt32(x.Value)));

            genes.ToList().ForEach(g => Console.Write(Convert.ToInt32(g.Value) + " "));

            var fitness = 1.0 - (difference.Sum() / (13.0 * 1000.0));

            if (fitness < 0)
            {
                fitness = 0.0;
            }


            Console.WriteLine("fitness: " + fitness);

            return fitness;
        }
    }


}
