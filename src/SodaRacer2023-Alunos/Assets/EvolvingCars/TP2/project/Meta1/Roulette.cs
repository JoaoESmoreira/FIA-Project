using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Infrastructure.Framework.Texts;
using GeneticSharp.Runner.UnityApp.Car;

public class Roulette : SelectionBase
{
    public Roulette() : base(2)
    {
    }

    protected override IList<IChromosome> PerformSelectChromosomes(int number, Generation generation)
    {

        IList<CarChromosome> population = generation.Chromosomes.Cast<CarChromosome>().ToList();
        IList<IChromosome> parents = new List<IChromosome>();

        double sumFitness = 0.0f;
        int i = 0;
        while(i < population.Count)
        {
            sumFitness += population[i].Fitness;
            i++;
        }
        
        i = 0;
        while(i < number)
        {
            double pointer = RandomizationProvider.Current.GetDouble();
            float partial = 0.0f;
            int index = 0;
            while(partial <= pointer){
                partial += (float)(population[index].Fitness / sumFitness);
                index++;
            }
            parents.Add(population[index - 1]);
            i++;
        }

        return parents;
    }
}
