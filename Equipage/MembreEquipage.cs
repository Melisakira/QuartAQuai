using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public abstract class MembreEquipage 
{
	public string Nom { get; }
	public string Grade { get; }
	public string PosteAffecte { get; }
	
	public MembreEquipage(string nom, string grade, string posteAffecte)
	{
		Nom = nom;
		Grade = grade;
		PosteAffecte = posteAffecte;
	}
	public abstract void ReagirAlerte(Incident incident);

}
