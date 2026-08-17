using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public abstract class MembreEquipage : IObservateur
{
    public string Nom { get; }
    public string Grade { get; }
    public string PosteAffecte { get; }
    public string Identite => $"{Nom} ({Grade})";

    protected MembreEquipage(string nom, string grade, string posteAffecte)
    {
        Nom = nom; Grade = grade;
        PosteAffecte = posteAffecte;
    }
    public abstract void ReagirAlerte(Incident incident);

    public void MettreAJour(Incident incident)
    {
        ReagirAlerte(incident);
    }

    protected void Annoncer(string message)
    {
        Console.WriteLine($"{Identite} : {message}");
    }

    protected void AnnoncerHorsDomaine(Incident incident)
    {
        Annoncer($"Incident hors de mon domaine, je reste à mon poste : {incident.Decrire()}");
    }
}

    
   