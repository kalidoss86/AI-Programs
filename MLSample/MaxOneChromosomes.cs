using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Randomizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLSample
{
    public class MaxOneChoromosomes : ChromosomeBase
    {

        private int m_numberofsequence;
        public MaxOneChoromosomes(int numberofsequence) : base(numberofsequence)
        {

            m_numberofsequence = numberofsequence;

            var sequence = RandomizationProvider.Current.GetInts(10, 0, 2);

            for (int i = 0; i < numberofsequence; i++)
            {
                ReplaceGene(i, GenerateGene(0));
            }

            //ReplaceGene(0, GenerateGene(0));
            //ReplaceGene(1, GenerateGene(1));
        }

        public Gene[] sequence
        {
            get
            {
                return GetGenes();
            }
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

            value = RandomizationProvider.Current.GetInt(0, 2);

            return new Gene(value);
        }

        public override IChromosome CreateNew()
        {
            return new MaxOneChoromosomes(m_numberofsequence);
        }
    }

    public class MyFitness : IFitness
    {
        static int generation = 0;
        public double Evaluate(IChromosome chromosome)
        {
            generation++;
            if (generation % 10 == 0)
            {
                Console.WriteLine("Generation: " + generation);
            }
            var genes = chromosome.GetGenes();

            foreach (var g in genes)
            {
                Console.Write(g.Value);
                Console.Write(" ");
            }
            var total = genes.Sum(g => (int)g.Value);
            Console.Write(" = ");
            Console.Write(total);

            Console.WriteLine();

            return (double)total;
            
        }
    }

}
