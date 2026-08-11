using QuartAQuai.Alertes;
using QuartAQuai.Equipage;

Electricien electricien = new("Isabelle", "second maitre", "Local technique");
PanneElectrique panneElectrique = new("critique", "Court-circuit sur le tableau principal", "Tableau éléctrique");
electricien.ReagirAlerte(panneElectrique);
