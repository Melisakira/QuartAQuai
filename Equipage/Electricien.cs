using System;
using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public class Electricien : MembreEquipage
{
	public Electricien(string nom, string grade, string posteAffecte)
		: base(nom, grade, posteAffecte)
	{
	}		

	public override void ReagirAlerte(Incident incident)
	{
		if (incident is PanneElectrique)
		{
			Console.WriteLine($"{Nom} ({Grade}) : Je me rends à {PosteAffecte} pour traiter :{incident.Decrire()}");
            Console.WriteLine($"{Nom} ({Grade}) : J'isole le circuit concerné et répare la panne  :{incident.Decrire()}");
            Console.WriteLine($"{Nom} ({Grade}) : Je rends compte à l'officier de garde de la résolution effectuée :{incident.Decrire()}");
        }
		else
		{
            Console.WriteLine($"{Nom} ({Grade}) :Incident hors de mon domaine, je reste à mon poste : {incident.Decrire()}");
        }
	}
}	


