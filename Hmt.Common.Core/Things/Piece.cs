namespace Hmt.Common.Core.Things;

public class Piece : Component
{
    public string? OwnerName { get; set; }
    public GraphSpace? AtSpace { get; set; }
    public List<Trait> Traits { get; set; } = new();

    public void SetTrait(Trait trait)
    {
        var existingTrait = Traits.Find(x => x.Type == trait.Type && x.Name == trait.Name);
        if (existingTrait != null)
            Traits.Remove(existingTrait);
        Traits.Add(trait);
    }
}
