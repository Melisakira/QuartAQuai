namespace QuartAQuai.AQuai;

public class EntreeJournal
{
    public DateTime Date { get; }
    public string Name { get; }
    public string Poste { get; }
    public string Evenement { get; }

    public EntreeJournal(DateTime date, string name, string poste, string evenement)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(poste);
        ArgumentException.ThrowIfNullOrWhiteSpace(evenement);

        Date = date; Name = name; Poste = poste; Evenement = evenement;
    }
}


