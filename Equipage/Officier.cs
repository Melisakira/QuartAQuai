using System;
using QuartEnMer.Alertes;

namespace QuartEnMer.Equipage;

public class Officier : MembreEquipage, IObservateur
{
    public Officier(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte)
    {
    }

}
