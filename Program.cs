using QuartAQuai.Alertes;
using QuartAQuai.Equipage;

Mecanicien mecanicien = new("Jean", "quartier maître", "Salle des machines");
AvarieMecanique avarieMecanique = new("critique", "panne de régulation", "Génératrice 1");
mecanicien.ReagirAlerte(avarieMecanique);
