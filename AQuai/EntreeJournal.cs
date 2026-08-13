namespace QuartAQuai.AQuai;

public class EntreeJournal
{
    public DateTime Date { get; }
    public string Name { get; }
    public string Poste { get; }
    public string Evenement { get; }

    public EntreeJournal(DateTime date, string name, string poste, string evenement)
    {
        Date = date; Name = name; Poste = poste; Evenement = evenement;
    }
}


