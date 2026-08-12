namespace QuartAQuai.Alertes;

public abstract class Incident
{
    public string Gravite { get; }
    public string Description { get; }

    protected Incident(string gravite, string description)
    {
        Gravite = gravite;
        Description = description;
    }
    public abstract string Decrire();
}


