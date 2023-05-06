/* You're about to go on a trip around the world! On this trip you're bringing your trusted backpack, that anything fits into.
* The bad news is that the airline has informed you that your luggage cannot exceed a certain amount of weight.
* To make sure you're bringing your most valuable items on this journey you've decided to give all your items a score that represents how valuable this item is to you.
* It's your job to pack your bag so that you get the most value out of the items that you decide to bring.
* Your input will consist of two arrays, one for the scores and one for the weights.
* Your input will always be valid lists of equal length, so you don't have to worry about verifying your input.
* You'll also be given a maximum weight. This is the weight that your backpack cannot exceed. */

using System.Text;

namespace Challenges;

internal struct Score: IComparable<Score>{
    public int Position { get; set; }

    public int Value { get; set; }

    public int CompareTo(Score other)
    {
        return this.Value.CompareTo(other.Value);
    }

    public override bool Equals(object? obj)
    {
        Score score = (Score)obj!;
        return score.Position == this.Position;
    }

    public override int GetHashCode()
    {
        return this.Position;
    }
}

public class PackingBackpack : IAlgorithm
{
    public void Execute()
    {
        int capacity = 551;
        int[] scores= new int[] {42,22,46,28,52,81,67,76,42,1,18,79,46,58,98,16,66,13,70,67,77,64,49,20,42,29,39,1,83,1,80,49,40,81,31,52,69,65,69,79,35,14,56,62,54,39,55,86,59,76,52,86,78,64,2,24,61,81,94,51,6,35,85,15,39,78,56,95,9,35,24,17,5,52,82,60,44,17,58,85,61,54,49,81,42,9,17,34,47,3,3,21,85,26,6,86,7,53,66,44,66,80,24,69,60,14,97,60,26,12,74,75,64,3,86,88,73,32,88,89,56,75,87,55,65,11,86,1,83,55,25,85,32,29,75,36,14,98,24,35,65,4,53,65,99,66,62,64,20,64,26,8,44,26,27,90,16,75,5,79,72,97,3,29,59,92,62,47,23,71,44,18,74,49,85,79,89,17,89,24,49,20,91,29,54,32,33,90,26,36,30,49,19,22,18,80,65,53,5,84,63,49,71,20,55,96,59,15,75,43,30,27,11,81,16,18,93,55,77,89,94,18,96,77,33,29,29,19,64,67,67,84,22,26,32,30,83,64,88,65,13,22,59,25,51,28,16,1,30};
        int[] weights= new int[] {13,25,1,27,26,10,13,15,26,8,3,22,25,11,16,11,7,21,25,8,13,22,12,4,9,26,26,29,23,6,20,27,20,17,17,9,4,2,27,28,25,25,4,23,22,4,27,28,16,22,5,17,22,1,19,18,21,8,4,7,2,20,2,27,5,13,19,26,10,22,24,25,19,5,22,22,21,28,26,9,14,28,22,5,11,12,1,27,21,21,1,15,15,18,26,10,10,18,27,29,20,3,29,4,21,5,24,28,17,7,18,10,26,17,26,18,23,6,22,2,20,28,1,17,11,9,10,28,27,18,6,21,13,20,1,11,25,2,5,12,22,25,16,14,23,13,25,16,2,5,20,24,29,24,1,25,5,11,6,19,11,4,12,1,10,24,14,12,27,7,15,1,17,15,3,16,4,16,23,28,12,13,8,22,21,8,2,23,2,20,19,7,13,13,27,16,26,19,7,14,25,22,24,23,15,5,7,17,28,7,26,23,13,20,6,9,11,9,25,15,16,8,23,19,7,26,13,26,25,18,2,1,6,19,21,28,27,29,12,19,27,25,10,4,21,25,16,12,8};

        /* //Expected = 50
        int[] scores= new int[] {7,11,12,10,2,10};
        int[] weights= new int[] {4,2,4,2,2,3};
        int capacity = 15; */

        /* //Expected = 60
        int[] scores= new int[] {20, 5, 10, 40, 15, 25};
        int[] weights = new int[] {1, 2, 3, 8, 7, 4};
        int capacity = 10; */

        /* //Expected = 29
        int[] scores= new int[] {15, 10, 9, 5};
        int[] weights = new int[] {1, 5, 3, 4}; 
        int capacity = 8; */

        int total = 0;
        IDictionary<int,ICollection<Score>> values = FindPositionsWeight(weights,scores);
        
        IEnumerable<int[]> combinations = capacity < weights.Sum()? GetCombinations(capacity,weights):new List<int[]>{weights};

        foreach(int[] combination in combinations)
        {
            bool[] positions = new bool[scores.Length];
            int result = 0;
            for(int i=0;i < combination.Length;i++)
            {
                int number = combination[i];

                if(!values[number].Any(s => !positions[s.Position])) continue;

                Score max = values[number].Where(s => !positions[s.Position]).Max()!;
                result +=  max.Value;
                positions[max.Position] = true;
            }

            total = result > total? result:total;
        }
    }

    private IEnumerable<int[]> GetCombinations(int capacity, int[] weights)
    {
        ICollection<string> numbers = new List<string>();
        bool higherSum = capacity < weights.Sum();
        
        for(int i=0;i < weights.Length;i++)
        {
            int[] combination = GetNextCombination(capacity,weights,i);

            if(combination.Sum()!=capacity) continue;

            yield return combination;
        }
    }

    private int[] GetNextCombination(int capacity, int[] weights,int position)
    {
        int visited = 0;
        int total = 0;
        ICollection<int> numbers = new List<int>();
        int[] orderedWeights = weights.OrderBy(n => n).ToArray();

        while(visited < weights.Length)
        {
            int number = orderedWeights[position];

            if(total + number <= capacity)
            {
                total += number;
                numbers.Add(number);
            }
            
            position = (position < weights.Length -1) ? position + 1 : 0; 
            visited++;
        }
        
        return numbers.ToArray();
    }

    private IDictionary<int,ICollection<Score>> FindPositionsWeight(int[] weights,int[] scores){
        IDictionary<int,ICollection<Score>> groupedWeights = new Dictionary<int,ICollection<Score>>();
        IEnumerable<int> numbers = weights.Distinct();

        Parallel.ForEach(numbers,n =>{
            ICollection<Score> values = new List<Score>();
            for(int i=0;i<weights.Length;i++)
            {
                if(weights[i]!=n)continue;

                values.Add(new Score{
                    Position = i,
                    Value = scores[i]
                });
            }

            lock(groupedWeights)
            {
                groupedWeights.Add(n,values);
            }
        });

        return groupedWeights;
    }
}