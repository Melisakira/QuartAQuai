# QuartAQuai

Application console interactive simulant à quai, un quart de nuit à bord de la frégate **F911 Wesdiep** (classe Wielingen) de la Force navale Belge.

## Contexte du projet

Ce projet est réalisé dans le cadre de l'examen de Programmation Orientée Objet (seconde session).
Le scénario s'appuie sur une expérience vécue d'électricienne de bord.


## Scénario


## Fonctionnalités (4, indépendantes)

| # | Fonctionnalité | Classe porteuse |
|---|---|---|
| 1 |  
| 3 | 
| 4 | 

Chaque fonctionnalité porte sur un sous-système distinct du navire ; aucune n'est la suite
obligée d'une autre.

## Architecture - deux hiérarchies d'héritage

### `MembreEquipage` (hiérarchie principale, polymorphisme)

Classe abstraite `MembreEquipage` (nom, grade, poste affecté) avec une méthode abstraite
`ReagirAlerte()`. Quatre rôles la spécialisent réellement, chacun avec sa propre réaction :
`Officier`, `Electricien`, `Mecanicien`, `Matelot`.

### `Incident` (hiérarchie secondaire)

Classe abstraite `Incident` (gravité, description) avec une méthode abstraite `Decrire()`.
Trois types concrets : `PanneElectrique`, `AvarieMecanique`, `AlerteMeteo`.

## Design pattern : Observer

Problème : quand un incident est déclaré au CentreAlerte, plusieurs membres d'équipage doivent réagir automatiquement, sans que le centre d'alerte connaisse à l'avance qui ils sont individuellement (couplage faible).

Solution retenue : pattern Observer

ISujet : rôle du CentreAlerte (Abonner, Desabonner, Notifier)
IObservateur : implémenté par chaque sous-classe de MembreEquipage (MettreAJour)
CentreAlerte ne connaît que l'interface IObservateur, jamais les classes concrètes directement — ajouter un nouveau rôle observateur ne modifie jamais CentreAlerte.
