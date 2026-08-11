using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public class Mecanicien : MembreEquipage
{
    public Mecanicien(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte)
    {
    }

    public override void ReagirAlerte(Incident incident)
    {
        if (incident is AvarieMecanique)
        {
            Console.WriteLine($"{Nom} ({Grade}) : Je me rends en {PosteAffecte} pour traiter l'incident: {incident.Decrire()}");
            Console.WriteLine($"{Nom} ({Grade}) : J'isole le circuit défaillant et je procède à la réparation");
            Console.WriteLine($"{Nom} ({Grade}) : Officier de quart, ici la machine. Panne résolue concernant :{incident.Decrire()}");
        }

        else
        {
            Console.WriteLine($"{Nom} ({Grade}) : Incident hors de mon domaine, je reste à mon poste : {incident.Decrire()}");
        }
    }
}

