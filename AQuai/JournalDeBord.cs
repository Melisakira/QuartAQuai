namespace QuartAQuai.AQuai;

public class JournalDeBord
{
    private readonly List<EntreeJournal> _entrees = new List<EntreeJournal>();

    public void ConsulterEntrees()
    {
        if (_entrees.Count == 0)
        {
            Console.WriteLine("Le journal de bord est vide pour l'instant"); return;
        }
        Console.WriteLine("=== Journal de bord ===");
        foreach (EntreeJournal entreeJournal in _entrees)
            Console.WriteLine
                ($"{entreeJournal.Date:dd/MM/yyyy HH:mm:ss} - {entreeJournal.Name} {entreeJournal.Poste} : {entreeJournal.Evenement}");
    }
    public void AjouterEntree(DateTime date,
                              string name,
                              string poste,
                              string evenement)
    {
        EntreeJournal nouvelleEntree = new EntreeJournal(date, name, poste, evenement);
        _entrees.Add(nouvelleEntree);
    }
}


