using System;
using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public class OfficierDeGarde : MembreEquipage
{
    public OfficierDeGarde(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte)
    {
    }

    public override void ReagirAlerte(Incident incident)
    {
        Console.WriteLine($"{Nom} ({Grade}) — {PosteAffecte} : Je centralise l'information — {incident.Decrire()}");

        if (incident.Gravite == "critique")
        {
            Console.WriteLine($"{Nom} ({Grade}) : Gravité critique, je sonne l'alarme générale, mobilise du renfort.");
            EscaladerVersCommandant(incident);
        }
    }
    public void EscaladerVersCommandant(Incident incident)
    {    
            Console.WriteLine($"{Nom} ({Grade}) : Je réveille le commandant et l'en informe : {incident.Decrire()}");
    }
}





