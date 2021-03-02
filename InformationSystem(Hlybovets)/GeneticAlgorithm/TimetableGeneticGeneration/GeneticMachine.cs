using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimetableGeneticGeneration
{
    class GeneticMachine
    {
        private List<Chromosome> _generation;
        private int _generationNum = 0;

        private int _startPopulation;

        private const int percentageOfMutations = 45;

        private float currentFitness = 0;
        private float previousFitness;

        private String _dataFilename;


        public GeneticMachine(String dataFilename, int startPopulation = 4)
        {
            _startPopulation = startPopulation;
            _dataFilename = dataFilename;
            LoadStaticLimitations(dataFilename);
            _generation = new List<Chromosome>();
            for (int i = 0; i< _startPopulation; i++)
            {
                Chromosome start = new Chromosome(dataFilename);
                _generation.Add(start);
            }
            ComputeParametrs();
           

        }

        private void LoadStaticLimitations(String dataFilename)  //static limitations load here
        {
            Utilities.LoadLectureAudiences(dataFilename);
            Utilities.LoadRequiredLessonsSet(dataFilename);
        }

        private void ComputeParametrs()
        {
            float inversedCoeffsSum = 0;
            for (int i = 0; i < _generation.Count; i++)
            {
                float devi = _generation[i].ComputeDeviation();
                inversedCoeffsSum += devi == 0 ? 0 : 1 / devi;
            }

            previousFitness = currentFitness;
            int j = 1;
            currentFitness = 0;
            for (int i = 0; i < _generation.Count; i++, j++)
            {
                _generation[i].Likelihood = _generation[i].Deviation == 0 ? 100 : ((1 / _generation[i].Deviation) / inversedCoeffsSum) * 100;
                currentFitness += _generation[i].Deviation;
            }
            currentFitness /= j;
        }

        public bool NextGeneration()
        {
            if (!CheckForRightAnswers())
            {
                List<IEnumerable<int>> probabilityLine = BuildProbabilityLine();
                _generation = FillNewGeneration(probabilityLine);
                ComputeParametrs();
                ++_generationNum;
                return false;
            }
            return true;
        }

        public void FindAnswer()
        {
            Console.WriteLine(this);
            while (!NextGeneration())
            {
                Console.WriteLine(this);
                if(currentFitness >= previousFitness)
                {
                    Mutation();
                }
            }
        }

        private bool CheckForRightAnswers()
        {
            for (int i = 0; i < _generation.Count; i++)
            {
                if(_generation[i].Likelihood == 100)
                {
                    Console.WriteLine("Found answer:");
                    Console.WriteLine(_generation[i]);
                    return true;
                }
            }
            return false;

        }

        private void Mutation()
        {
            int numOfMutations = (percentageOfMutations * _generation.Count) / 100;
            for (int i = 0; i< numOfMutations; i++)
            {
                int randMutation = Utilities.ChooseRandomly(0, _generation.Count - 1);
                _generation[randMutation] = new Chromosome(_dataFilename);
            }
            ComputeParametrs();
        }

        private List<Chromosome> FillNewGeneration(List<IEnumerable<int>> probabilityLine)
        {
            List<Chromosome> children = new List<Chromosome>();
            int daysNum = _generation[0].AmountOfWorkingDays();
            int specNum = _generation[0].AmountOfSpecialties();
            List<String> specialties = _generation[0].Specialties();
            for (int i = 0, j = 0; i< daysNum * specNum; i++, j++)
            {
                if(j > daysNum - 1)
                {
                    j = 0;
                    specialties.RemoveAt(0);
                }
                IEnumerable<int> parent1Range;
                Chromosome parent1 = new Chromosome(ChooseFirstParent(probabilityLine, out parent1Range));
                Chromosome parent2 = new Chromosome(ChooseSecondParent(probabilityLine, parent1Range));

                Chromosome[] parentsChildren = parent1.doubleDaysCrossover(parent2, j, specialties[0]);
                children.Add(parentsChildren[0]);
            }
            return children;

        }

        private Chromosome ChooseFirstParent(List<IEnumerable<int>> probabilityLine, out IEnumerable<int> firstParentRange)
        {
            int randParent1 = Utilities.ChooseRandomly(0, 100);

            for (int j = 0; j < probabilityLine.Count; j++)
            {
                if (probabilityLine[j].Contains(randParent1))
                {
                    firstParentRange = probabilityLine[j];
                    return _generation[j];
                }
            }
            firstParentRange = Enumerable.Range(0, 0);
            return null;
        }

        private Chromosome ChooseSecondParent(List<IEnumerable<int>> probabilityLine, IEnumerable<int> firstParentRange)
        {
            int randParent2 = Utilities.ChooseRandomly(0, 100);
            while (true)  // choosing number that don't give same parent (avoid picking parent1)
            {
                if (firstParentRange.Contains(randParent2))
                {
                    randParent2 = Utilities.ChooseRandomly(0, 100);
                }
                else
                {
                    break;
                }
            }

            for (int j = 0; j < probabilityLine.Count; j++)
            {
                if (probabilityLine[j].Contains(randParent2))
                {
                    return _generation[j];
                }
            }
            return null;
        }


        private List<IEnumerable<int>> BuildProbabilityLine()
        {
            List<IEnumerable<int>> result = new List<IEnumerable<int>>();

            float prev = 0;
            for(int i = 0; i< _generation.Count; i++)
            {
                float next = prev + _generation[i].Likelihood;
                result.Add(Enumerable.Range((int)prev, (int)Math.Ceiling(next - prev)));
                prev = next;
            }
            IEnumerable<int> last = result[14];
            bool cgh = last.Contains(101);
            return result;
        }

        public override string ToString()
        {
            String result = String.Concat("Generation number = ", _generationNum, "\n");
            int i = 1;
            foreach (var chromo in _generation)
            {
                result += String.Concat("Chromosome ", i, " , Likelihood: ", chromo.Likelihood, " % , Fitness: ", chromo.Deviation, chromo.Deviation == 0 ? " ANSWER FOUND!!!" : "", "\n");
                ++i;
            }
            result += String.Concat("Average fitness: ", currentFitness, "\n");
            return result;
        }
    }
}
