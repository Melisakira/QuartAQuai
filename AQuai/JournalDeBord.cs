using QuartAQuai.Equipage;

namespace QuartAQuai.AQuai;

public class JournalDeBord
{
    private readonly List<EntreeJournal> _entrees = new List<EntreeJournal>();

    public List<EntreeJournal> ObtenirEntrees()
    {
        return _entrees;
    }
    public void AjouterEntree(DateTime date,
                              string name,
                              string poste,
                              string evenement)
    {
        EntreeJournal nouvelleEntree = new EntreeJournal(date, name, poste, evenement);
        _entrees.Add(nouvelleEntree);
    }
    public void AjouterEntree(DateTime date, MembreEquipage membre, string evenement)
    {
        AjouterEntree(date, membre.Nom, membre.PosteAffecte, evenement);
    }
}


