namespace QuartAQuai.Alertes;

public class IncidentSurete : Incident
{
    public string NatureMenace { get; }

    public IncidentSurete(string gravite, string description, string natureMenace)
        : base(gravite, description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(natureMenace);

        NatureMenace = natureMenace;
    }

    public override string Decrire()
    {
        return $"{Description} -{NatureMenace} [{Gravite}]";
    }
}