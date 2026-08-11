using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public class Electricien : MembreEquipage
{
    public Electricien(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte)
    {
    }

    public override void ReagirAlerte(Incident incident)
    {
        if (incident is PanneElectrique)
        {
            Console.WriteLine($"{Nom} ({Grade}) : Je me rends en {PosteAffecte} pour traiter :{incident.Decrire()}");
            Console.WriteLine($"{Nom} ({Grade}) : J'isole le circuit défaillant et je procède à la réparation ");
            Console.WriteLine($"{Nom} ({Grade}) : Officier de quart, ici l'électricien. Panne résolue concernant:{incident.Decrire()}");

        }
        else
        {
            Console.WriteLine($"{Nom} ({Grade}) : Incident hors de mon domaine, je reste à mon poste : {incident.Decrire()}");
        }
    }
}


