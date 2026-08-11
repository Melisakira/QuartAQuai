using System;
using QuartAQuai.Alertes;

namespace QuartAQuai.Equipage;

public class VeilleurCoupee : MembreEquipage
{
    public VeilleurCoupee(string nom, string grade, string posteAffecte)
        : base(nom, grade, posteAffecte)
    {
    }
    public override void ReagirAlerte(Incident incident)
    { 
    }
}
