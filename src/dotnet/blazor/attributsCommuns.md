# Attributs communs dans les modèles de formulaires

`[ValidateNever]`: ValidateNeverAttribute Indique qu’une propriété ou un paramètre doit être exclu de la validation.

`[CreditCard]`: Vérifie que la propriété a un format de carte de crédit. Requiert des méthodes supplémentaires de validation jQuery.

`[Compare]`: Valide que deux propriétés d’un modèle correspondent.

`[EmailAddress]`: Vérifie que la propriété a un format d’e-mail.

`[Phone]`: Vérifie que la propriété a un format de numéro de téléphone.

`[Range]`: Vérifie que la valeur de la propriété est comprise dans une plage spécifiée.

`[RegularExpression]`: Valide le fait que la valeur de propriété corresponde à une expression régulière spécifiée.

`[Required]`: Vérifie que le champ n’a pas la valeur null. Pour plus d’informations sur le comportement de cet attribut, consultez [Required] attribut .

`[StringLength]`: Valide le fait qu’une valeur de propriété de type chaîne ne dépasse pas une limite de longueur spécifiée.

`[Url]`: Vérifie que la propriété a un format d’URL.

`[Remote]`: Valide l’entrée sur le client en appelant une méthode d’action sur le serveur. Pour plus d’informations sur le comportement de cet attribut, consultez [Remote] attribut.

Plus d'informations dans la doc .NET [ici](https://learn.microsoft.com/fr-fr/dotnet/api/system.componentmodel.dataannotations?view=net-8.0)