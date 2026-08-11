namespace QuartAQuai.Alertes;

public class AlerteMeteo : Incident
{
    public string EquipementConcerne { get; }

    public AlerteMeteo(string gravite, string description, string equipementConcerne)
        : base(gravite, description)
    {
        EquipementConcerne = equipementConcerne;
    }

    public override string Decrire()
    {
        return $"[Alerte Météo] {Description} : équipement concerné : {EquipementConcerne} (gravité : {Gravite})";
    }
}
