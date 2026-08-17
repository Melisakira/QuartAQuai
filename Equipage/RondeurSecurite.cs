using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public class RondeurSecurite : MembreEquipage
{
    public RondeurSecurite(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte)
    {
    }
    public override void ReagirAlerte(Incident incident)
    {
        Annoncer($"Je poursuis ma ronde, attentif à toute anomalie - {incident.Description}");
    }
}

