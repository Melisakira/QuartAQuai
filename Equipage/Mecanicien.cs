using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public class Mecanicien : Technicien
{
    public Mecanicien(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte, "la machine")
    {
    }

    protected override bool EstDeMonDomaine(Incident incident)
    {
        return incident is AvarieMecanique;
    }
}
