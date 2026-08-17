using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public class Electricien : Technicien
{
    public Electricien(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte, "l'électricien")
    {
    }

    protected override bool EstDeMonDomaine(Incident incident)
    {
        return incident is PanneElectrique;
    }
}
