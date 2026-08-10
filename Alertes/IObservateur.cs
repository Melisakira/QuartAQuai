using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public interface IObservateur
{
    void MettreAJour(Incident incident);
}
