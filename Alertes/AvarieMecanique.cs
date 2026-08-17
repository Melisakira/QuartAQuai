namespace QuartAQuai.Alertes;

public class AvarieMecanique : Incident
{
    public string SystemeConcerne { get; }

    public AvarieMecanique(string gravite, string description, string systemeConcerne)
        : base(gravite, description)
    {
        SystemeConcerne = systemeConcerne;
    }

    public override string Decrire()
    {
        return Formater(SystemeConcerne);
    }
}
