using System;
using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public class Mecanicien : MembreEquipage, IObservateur
{
    public Mecanicien(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte)
    {
    }

    public override void ReagirAlerte(Incident incident)
    {
        // Par défaut 
    }

    // !! redondance implémentation interface IObservateur ??? à revoir le chapitre 11 ?
    public void MettreAJour(Incident incident)
    {
        // !! redondance avec electricien et 
        ReagirAlerte(incident);
    }
}
