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
            Console.WriteLine($"{Nom} ({Grade}) : Je me rends à {PosteAffecte} pour traiter : {incident.Decrire()}");
        }
        else
        {
            Console.WriteLine($"{Nom} ({Grade}) : incident hors de mon domaine, je reste disponible : {incident.Decrire()}");
        }
    }
}

