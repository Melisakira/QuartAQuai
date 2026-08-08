using System;
using QuartEnMer.Alertes;

namespace QuartEnMer.Equipage;

public class Mecanicien : MembreEquipage, IObservateur
{
    public Mecanicien(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte)
    {
    }

}
