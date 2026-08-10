using QuartAQuai.Equipage;

namespace QuartAQuai.Alertes;

public interface ISujet
{
    void Abonner(IObservateur observateur);
    void Desabonner(IObservateur observateur);
    void Notifier (Incident incident);
}
