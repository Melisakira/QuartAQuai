using System;
using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public class Matelot : MembreEquipage, IObservateur
{
    public Matelot(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte)
    {
    }

}
