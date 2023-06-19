using Hmt.Common.Core.Interfaces;

namespace Hmt.Common.Core.Things;

public class Deck<T> : Component where T : ICard
{
    public List<T> Cards { get; set; } = new();

    public void Add(T card)
    {
        Cards.Add(card);
    }

    public T? Draw(string name)
    {
        var card = Cards.FirstOrDefault(c => c.Name == name);
        if (card != null)
            Cards.Remove(card);
        return card;
    }

    public T Draw(int index = -1)
    {
        if (index == -1)
            index = Cards.Count - 1;

        var card = Cards[index];
        Cards.RemoveAt(index);
        return card;
    }

    public void Shuffle(int count = 2)
    {
        var random = new Random(Guid.NewGuid().GetHashCode());
        for (var i = 0; i < count; i++)
        {
            var cards = new List<T>();
            while (Cards.Count > 0)
            {
                var index = random.Next(Cards.Count);
                cards.Add(Draw(index));
            }
            Cards = cards;
        }
    }
}
