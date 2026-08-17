namespace QuartAQuai.Alertes;

public abstract class Incident
{
    public static readonly IReadOnlyList<string> GravitesValides = ["mineur", "majeur", "critique"];

    public string Gravite { get; }
    public string Description { get; }

    protected Incident(string gravite, string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(gravite);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        if (!GravitesValides.Contains(gravite))
        {
            throw new ArgumentOutOfRangeException(nameof(gravite), gravite, $"Gravité inconnue. Valeurs attendues : {string.Join(", ", GravitesValides)}.");
        }

        Gravite = gravite;
        Description = description;
    }
    public abstract string Decrire();
}


