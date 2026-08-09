using System;
using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public class Mecanicien : MembreEquipage, IObservateur
{
    public Mecanicien(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte)
    {
    }

}
