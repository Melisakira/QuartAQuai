using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public class VeilleurCoupee : MembreEquipage
{
    public VeilleurCoupee(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte)
    {
    }

    public void SurveillerQuai(string etatAmarres, string observation)
    {
        Annoncer($"Surveillance du quai et des accès : amarres {etatAmarres}, {observation}, prêt à réagir en cas d'incident.");
    }
    public override void ReagirAlerte(Incident incident)
    {
        if (incident is IncidentSurete)
        {
            Annoncer($"Alerte sûreté ! Bouclage des accès et application des mesures de protection — {incident.Decrire()}");
        }
        else if (incident is AlerteMeteo)
        {
            Annoncer($"Alerte météo ! Je m'assure que tout l'équipage est en sécurité et que les procédures d'urgence sont suivies — {incident.Decrire()}");
        }
        else
        {
            Annoncer($"{PosteAffecte} - Incident hors de mon domaine, je maintiens le contrôle - {incident.Decrire()}");
        }
    }
}






