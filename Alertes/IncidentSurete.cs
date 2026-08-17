namespace QuartAQuai.Alertes;

public class IncidentSurete : Incident
{
    public string NatureMenace { get; }

    public IncidentSurete(string gravite, string description, string natureMenace)
        : base(gravite, description)
    {
        NatureMenace = natureMenace;
    }

    public override string Decrire()
    {
        return Formater(NatureMenace);
    }
}