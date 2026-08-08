using QuartEnMer;

namespace QuartEnMer.Equipage;

public interface IObservateur
{
    void MettreAJour(Incident incident);
}
