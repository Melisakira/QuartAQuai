namespace QuartAQuai.Alertes;

public class AlerteMeteo : Incident
{
    public string SystemeConcerne { get; }

    public AlerteMeteo(string gravite, string description, string systemeConcerne)
        : base(gravite, description)
    {
        SystemeConcerne = systemeConcerne;
    }

    public override string Decrire()
    {
        return $"[Alerte Météo] {Description} : système concerné : {SystemeConcerne} (gravité : {Gravite})";
    }
}
