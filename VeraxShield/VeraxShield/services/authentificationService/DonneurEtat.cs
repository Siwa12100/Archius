using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using VeraxShield.composants.formulaires.modeles;

public class DonneurEtat : AuthenticationStateProvider
{
    private IAuthentificationService _serviceAuthentification;
    private UtilisateurCourant _utilisateurCourant;

    public DonneurEtat(IAuthentificationService service)
    {
        this._serviceAuthentification = service;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identite = new ClaimsIdentity();
        
        try
        {
            var userInfo = this.getUtilisateurCourant();

            if (userInfo.EstAuthentifie)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, this._utilisateurCourant.Nom) }.Concat(this._utilisateurCourant.Claims.Select(c => new Claim(c.Key, c.Value)));
                identite = new ClaimsIdentity(claims, "Server authentication");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Request failed:" + ex);
        }

        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identite)));
    }

    private UtilisateurCourant getUtilisateurCourant()
    {
        if (this._utilisateurCourant != null && this._utilisateurCourant.EstAuthentifie)
        {
            return this._utilisateurCourant;
        }

        return new UtilisateurCourant();
    }

    public async Task Connexion(RequeteConnexion requete)
    {
        this._serviceAuthentification.Connexion(requete);

        // No error - Login the user
        var user = this._serviceAuthentification.GetUtilisateur(requete.Nom);
        this._utilisateurCourant = user;

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task Deconnexion()
    {
        this._utilisateurCourant = null;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task Inscription(RequeteInscription requete)
    {
        this._serviceAuthentification.Inscription(requete);

        // No error - Login the user
        var user = this._serviceAuthentification.GetUtilisateur(requete.Nom);
        this._utilisateurCourant = user;

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}