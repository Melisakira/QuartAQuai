namespace QuartAQuai.Alertes;

public class CentreAlerte : ISujet
{
    private readonly List<IObservateur> _observateurs = new List<IObservateur>();
    public void Abonner(IObservateur observateur)
    {
        ArgumentNullException.ThrowIfNull(observateur);

        if (_observateurs.Contains(observateur))
        {
            throw new InvalidOperationException("Cet observateur est déjà abonné au centre d'alerte.");
        }

        _observateurs.Add(observateur);
    }
    public void Desabonner(IObservateur observateur)
    {
        ArgumentNullException.ThrowIfNull(observateur);

        if (!_observateurs.Remove(observateur))
        {
            throw new InvalidOperationException("Cet observateur n'est pas abonné au centre d'alerte.");
        }
    }
    public void Notifier(Incident incident)
    {
        ArgumentNullException.ThrowIfNull(incident);

        List<Exception> echecs = [];
        foreach (IObservateur observateur in _observateurs.ToList())
        {
            try
            {
                observateur.MettreAJour(incident);
            }
            catch (Exception exception)
            {
                echecs.Add(new InvalidOperationException($"L'observateur {observateur.GetType().Name} n'a pas pu traiter l'incident : {incident.Decrire()}", exception));
            }
        }

        if (echecs.Count > 0)
        {
            throw new AggregateException("Certains membres d'équipage n'ont pas pu réagir à l'incident.", echecs);
        }
    }
}
