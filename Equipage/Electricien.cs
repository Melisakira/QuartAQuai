using System;
using QuartAQuai.Alertes;
using QuartAQuai.Equipage;

namespace QuartAQuai.Equipage;

public class Electricien : MembreEquipage, IObservateur
{
	public Electricien(string nom, string grade, string posteAffecte)
		: base(nom, grade, posteAffecte)
	{
	}		

	public override void ReagirAlerte(Incident incident)
	{
		if (incident is PanneElectrique)
		{
			Console.WriteLine($"{Nom} ({Grade}) : Je me reds à {PosteAffecte} pour traiter {incident.Decrire()}");
		}
		else
		{
            Console.WriteLine($"{Nom} ({Grade}) :Incident hors de mon domaine, je reste disponible{incident.Decrire()}");
        }
	}
	public void MettreAJour(Incident incident)
	{
		ReagirAlerte(incident);
	}
}

