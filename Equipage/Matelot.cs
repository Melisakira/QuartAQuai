using System;
using QuartEnMer.Alertes;

namespace QuartEnMer.Equipage;

public class Matelot : MembreEquipage, IObservateur
{
    public Matelot(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte)
    {
    }

}
