using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public class Officier : MembreEquipage
{
    public Officier(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte)
    {
    }

    public void MettreAJour(Incident incident)
    {
        // idem
        ReagirAlerte(incident);
    }

}
