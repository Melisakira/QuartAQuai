using System;
using QuartEnMer.Alertes;

namespace QuartEnMer.Equipage;

public class Electricien : MembreEquipage, IObservateur
{
	public Electricien(string nom, string grade, string posteAffecte)
		: base(nom, grade, posteAffecte)
	{
	}		

}
