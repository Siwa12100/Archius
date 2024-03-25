package siwa.controleurs;

import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;
import siwa.modele.livres.ILivresDataManager;
import siwa.modele.livres.Livre;
import siwa.modele.livres.StubLivres;

@Path("/livres")
public class LivresControleur {

    private ILivresDataManager manager;

    public LivresControleur() {
        this.manager = new StubLivres();
    }

    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public Response getTousLesLivres() {
        try {
            return Response.ok(manager.getTousLesLivres()).build();
        } catch (Exception e) {
            return Response.status(Response.Status.INTERNAL_SERVER_ERROR)
                    .entity("Erreur lors de la récupération des livres").build();
        }
    }

    @GET
    @Path("/{id}")
    @Produces(MediaType.APPLICATION_JSON)
    public Response getLivre(@PathParam("id") int id) {
        try {
            Livre livre = manager.getLivre(id);
            if (livre != null) {
                return Response.ok(livre).build();
            } else {
                throw new WebApplicationException("Livre non trouvé", Response.Status.NOT_FOUND);
            }
        } catch (WebApplicationException wae) {
            return Response.status(wae.getResponse().getStatus())
                    .entity(wae.getMessage()).build();
        } catch (Exception e) {
            return Response.status(Response.Status.INTERNAL_SERVER_ERROR)
                    .entity("Erreur serveur").build();
        }
    }

    @POST
    @Consumes(MediaType.APPLICATION_JSON)
    @Produces(MediaType.APPLICATION_JSON)
    public Response creerLivre(Livre livre) {
        try {
            if (livre == null || livre.getTitre() == null || livre.getDescription() == null) {
                throw new WebApplicationException("Informations du livre incomplètes", Response.Status.BAD_REQUEST);
            }
            manager.creerLivre(livre);
            return Response.status(Response.Status.CREATED).entity(livre).build();
        } catch (WebApplicationException wae) {
            return Response.status(wae.getResponse().getStatus())
                    .entity(wae.getMessage()).build();
        } catch (Exception e) {
            return Response.status(Response.Status.INTERNAL_SERVER_ERROR)
                    .entity("Erreur lors de la création du livre").build();
        }
    }

    @PUT
    @Path("/{id}")
    @Consumes(MediaType.APPLICATION_JSON)
    @Produces(MediaType.APPLICATION_JSON)
    public Response updateLivre(@PathParam("id") int id, Livre livre) {
        try {
            Livre livreExistant = manager.getLivre(id);
            if (livreExistant == null) {
                throw new WebApplicationException("Livre non trouvé pour mise à jour", Response.Status.NOT_FOUND);
            }
            if (livre == null || livre.getTitre() == null || livre.getDescription() == null) {
                throw new WebApplicationException("Informations du livre incomplètes pour la mise à jour", Response.Status.BAD_REQUEST);
            }
            livre.setId(id); // Assurez-vous que l'ID du livre est correctement mis à jour
            manager.updateLivre(livre);
            return Response.ok(livre).build();
        } catch (WebApplicationException wae) {
            return Response.status(wae.getResponse().getStatus())
                    .entity(wae.getMessage()).build();
        } catch (Exception e) {
            return Response.status(Response.Status.INTERNAL_SERVER_ERROR)
                    .entity("Erreur serveur lors de la mise à jour du livre").build();
        }
    }

    @DELETE
    @Path("/{id}")
    public Response supprimerLivre(@PathParam("id") int id) {
        try {
            Livre livre = manager.getLivre(id);
            if (livre == null) {
                throw new WebApplicationException("Livre non trouvé pour suppression", Response.Status.NOT_FOUND);
            }
            manager.supprimerLivre(id);
            return Response.status(Response.Status.NO_CONTENT).build();
        } catch (WebApplicationException wae) {
            return Response.status(wae.getResponse().getStatus())
                    .entity(wae.getMessage()).build();
        } catch (Exception e) {
            return Response.status(Response.Status.INTERNAL_SERVER_ERROR)
                    .entity("Erreur serveur lors de la suppression du livre").build();
        }
    }
}
