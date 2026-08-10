using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public class Matelot : MembreEquipage
{
    public Matelot(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte)
    {
    }

    public override void ReagirAlerte(Incident incident)
    {
        if (incident is AlerteMeteo)
        {
            Console.WriteLine($"{Nom} ({Grade}) : Je vérifie le pont et les amarres : {incident.Decrire()}");
        }
        else
        {
            Console.WriteLine($"{Nom} ({Grade}) : incident hors de mon domaine, je reste à disposition : {incident.Decrire()}");
        }
    }
}


