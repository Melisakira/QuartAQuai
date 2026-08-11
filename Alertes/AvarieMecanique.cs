namespace QuartAQuai.Alertes;

public class AvarieMecanique : Incident
{
    public string EquipementConcerne { get; }

    public AvarieMecanique(string gravite, string description, string equipementConcerne)
        : base(gravite, description)
    {
        EquipementConcerne = equipementConcerne;
    }

    public override string Decrire()
    {
        return $"[Avarie mécanique] {Description} : équipement concerné : {EquipementConcerne} (gravité : {Gravite})";
    }
}
