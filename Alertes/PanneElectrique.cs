namespace QuartAQuai.Alertes;

public class PanneElectrique : Incident
{
    public string SystemeConcerne { get; }

    public PanneElectrique(string gravite, string description, string systemeConcerne)
        : base(gravite, description)
    {
        SystemeConcerne = systemeConcerne;
    }

    public override string Decrire()
    {
        return $"{Description} -{SystemeConcerne} [{Gravite}]";
    }
}

