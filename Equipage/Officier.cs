using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public class Officier : MembreEquipage
{
    public Officier(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte)
    {
    }

    public override void ReagirAlerte(Incident incident)
    {
        // Minimal
        Console.WriteLine($"Officier {Nom} ({Grade}) répond à une alerte : {incident}");
    }

}
