
# Ma solution de test Cleemy

## Mes projets
J'ai créé 3 projets .Net:

 - **CleemyWebApi**: Api contenant les controllers + DTO pour classes + mappage entity/DTO
 - **CleemyDAL**: projet de persistance /  accès aux données
 - **CleemyUnitTests**: projet de tests unitaires xUnit

## Mes choix

Ayant une forte expertise base de données, j'ai choisit un design "Database First" suivi d'un Scaffold-DbContext dont la commande se trouve dans Scaffold_Command.txt.
Les scripts de création de la base et initialisation des données se trouve dans le dossier BDDScripts, numérotés par ordre d'exécution.
J'ai retenu l'option d'avoir des tables de référence nature et devise afin de permettre d'ajouter des valeurs sans recompiler l'application.

Pour le Json en entrée j'ai choisit 
 - Pour l'utilisateur: mettre un id car il était explicitement indiqué dans l'énoncé qu'il y avait une table utilisateur
 - Pour la devise, j'ai choisit de considérer en entrée le code iso, afin d'éviter les doublons avec des orthographes différentes pour les textes des devises. 
 - Pour les nature, j'ai supposé que l'API acceptait les valeurs textuelles.
 
Pour le mappage des DTO vs Entity, j'ai décidé d'utiliser un mapper automatique utilisant la réflexion qui m'a servit dans d'autres projets du même genre. 
Cela permet si on ajoute une propriété de type system à l'entité, d'avoir uniquement à l'ajouter au DTO (avec le même nom), sans avoir à modifier les méthodes de mapping.
Pour les propriétés de type objet, un mappage explicite est nécessaire. 

J'ai une légère incohérence dans le nommage des propriété entre DTO et Entité.
Pour les entités, la génération étant faite pas .Net Core, j'ai gardé le CamelCase.
Pour les autres élément du code j'ai opté pour le camelCase, plus usité de nos jours et surtout cohérent avec les pratiques des FrontOffices JavaScript ou TypeScript.