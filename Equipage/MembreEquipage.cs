using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public abstract class MembreEquipage : IObservateur
{
    public string Nom { get; }
    public string Grade { get; }
    public string PosteAffecte { get; }

    protected MembreEquipage(string nom, string grade, string posteAffecte)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nom);
        ArgumentException.ThrowIfNullOrWhiteSpace(grade);
        ArgumentException.ThrowIfNullOrWhiteSpace(posteAffecte);

        Nom = nom; Grade = grade;
        PosteAffecte = posteAffecte;
    }
    public abstract void ReagirAlerte(Incident incident);

    public void MettreAJour(Incident incident)
    {
        ArgumentNullException.ThrowIfNull(incident);

        ReagirAlerte(incident);
    }
}

    
   