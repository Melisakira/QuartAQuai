namespace QuartAQuai.Alertes;

public class CentreAlerte : ISujet
{
    private readonly List<IObservateur> _observateurs = new List<IObservateur>();
    public void Abonner(IObservateur observateur)
    {
        _observateurs.Add(observateur);
    }
    public void Desabonner(IObservateur observateur)
    {
        _observateurs.Remove(observateur);
    }
    public void Notifier(Incident incident)
    {
        foreach (IObservateur observateur in _observateurs)
        {
            observateur.MettreAJour(incident);
        }
    }
}



