namespace QuartAQuai.Alertes;

public class CentreAlerte : ISujet
{
    public void Abonner(IObservateur observateur) { throw new NotImplementedException(); }
    public void Desabonner(IObservateur observateur) { throw new NotImplementedException(); }
    public void Notifier(Incident incident) { throw new NotImplementedException(); }

}

