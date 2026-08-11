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
        if (incident.Gravite == "mineur")
        {
            Console.WriteLine($"{Nom} ({Grade}) :Officier de garde au PC Sécurité. Alerte reçue : {incident.Decrire()}. A traiter lors de la prochaine ronde.");
        }
        else if (incident.Gravite == "majeur" || incident.Gravite == "critique")
        {
            Console.WriteLine($"{Nom} ({Grade}) : Officier de garde au PC Sécurité. Alerte reçue : {incident.Decrire()}. Equipe de garde, procédez immédiatement à la levée des doutes.");
            Console.WriteLine($"{Nom} ({Grade}) : Je bascule les réseaux de secours et je passe le poste de garde en posture de vigilance.");
            if (incident.Gravite == "critique")
            {
                EscaladerVersCommandant(incident);
            }
        }
    }
    public void EscaladerVersCommandant(Incident incident)
    {
        Console.WriteLine($"{Nom} ({Grade}) : Je réveille le commandant et l'en informe : {incident.Decrire()}");
    }
}
