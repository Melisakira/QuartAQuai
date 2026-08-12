namespace QuartAQuai.Alertes;

public class AlerteMeteo : Incident
{
    public string Phenomene { get; }

    public AlerteMeteo(string gravite, string description, string phenomene)
        : base(gravite, description)
    {
        Phenomene = phenomene;
    }

    public override string Decrire()
    {
        return $"{Description} -{Phenomene} {Gravite})";
    }
}
