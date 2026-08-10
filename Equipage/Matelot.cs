using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public class Matelot : MembreEquipage
{
    public Matelot(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte)
    {
    }

    public void MettreAJour(Incident incident)
    {
        ReagirAlerte(incident);
    }
}


