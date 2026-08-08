using System;
using QuartEnMer.Alertes;

namespace QuartEnMer.Equipage;

public class Electricien : MembrEquipage, IObservateur
{
	public Electricien(string nom, string grade, string posteAffecte)
		: base(nom, grade, posteAffecte);

}
