namespace QuartAQuai.Alertes;

public class PanneElectrique : Incident
{
	public string EquipementConcerne { get; }

	public PanneElectrique(string gravite, string description, string equipementConcerne)
		: base(gravite, description)
	{
		EquipementConcerne = equipementConcerne;
	}

	public override string Decrire ()
	{
		 return $"[Panne électrique] {Description} : équipement concerné : {EquipementConcerne} (gravité : {Gravite})";
	}
}
	
