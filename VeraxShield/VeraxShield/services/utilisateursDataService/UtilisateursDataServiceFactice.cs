using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using VeraxShield.modele.utilisateurs;

namespace VeraxShield.services.UtilisateursDataService
{
	public class UtilisateursDataServiceFactice : IUtilisateursDataService
	{
		[Inject]
		private HttpClient _clientHttp { get; set; }

		[Inject]
		public ILocalStorageService _localStorage { get; set; }

		[Inject]
		public NavigationManager _navigationManager { get; set; }

		private String EmplacementLocalStorage { get; set; }
		private String EmplacementJson {get; set;}

		
		public UtilisateursDataServiceFactice(HttpClient clientHttp, ILocalStorageService localStorage, NavigationManager navigationManager)
		{
			this._clientHttp = clientHttp;
			this._localStorage = localStorage;
			this._navigationManager = navigationManager;

			this.EmplacementLocalStorage = "utilisateursData";
			this.EmplacementJson = $"{_navigationManager.BaseUri}data/fakeUtilisateurs.json";
		}

		public async Task<List<Utilisateur>> getAllUtilisateurs()

		{
			List<Utilisateur> lUtilisateurs = new List<Utilisateur>();

			lUtilisateurs = await this.getUtilisateursFromLocalStorage();
			if (lUtilisateurs.Count == 0)
			{
				lUtilisateurs = await this.getUtilisateursFromJson(this.EmplacementJson);
				await this.saveUtilisateursLocalStorage(lUtilisateurs);

				Console.WriteLine("--> Le contenu du local storage a été écrasé !");
			}

			return lUtilisateurs;
		}

		public async Task resetDataUtilisateurs()
		{
            List<Utilisateur> lUtilisateurs = new List<Utilisateur>();
            lUtilisateurs = await this.getUtilisateursFromJson(this.EmplacementJson);
            await this.saveUtilisateursLocalStorage(lUtilisateurs);

			Console.WriteLine("Local storage reset !");
        }

		public async Task SaveAllUtilisateurs(List<Utilisateur> list)
		{
			await this.saveUtilisateursLocalStorage(list);
		}

		public async Task AjouterUtilisateur(Utilisateur u)
		{
			List<Utilisateur> data = await this.getAllUtilisateurs();
			data.Add(u);
			await this.SaveAllUtilisateurs(data);
		}

		public async Task SupprimerUtilisateur(Utilisateur u)
		{
            List<Utilisateur> data = await this.getAllUtilisateurs();

            foreach (Utilisateur temp in data)
            {
                Console.WriteLine(" - d : " + temp.Pseudo);
            }

            int index = -1;

			foreach(Utilisateur temp in data)
			{
				if (temp.Pseudo == u.Pseudo)
				{
					index = data.IndexOf(temp);
				}
			}

            Console.WriteLine("Index : " + index);

            if (index != -1)
			{
				data.RemoveAt(index);
			}          

            await this.SaveAllUtilisateurs(data);

			Console.WriteLine("L'utilisateur " + u.Pseudo + "supprimé !");

			data = await this.getAllUtilisateurs();

            foreach (Utilisateur temp in data)
			{
				Console.WriteLine(temp.Pseudo);
			}
        }

		public async Task MettreAJourUtilisateur(Utilisateur u)
        {
			await this.SupprimerUtilisateur(u);
			await this.AjouterUtilisateur(u);
        }

		private async Task<List<Utilisateur>> getUtilisateursFromJson(String cheminVersJson)
		{
			List<Utilisateur> utilisateursDeserialise = new List<Utilisateur> ();

			var data = await _clientHttp.GetFromJsonAsync<Utilisateur[]>(cheminVersJson);
			utilisateursDeserialise = data.ToList();

			return utilisateursDeserialise;
		}

		private async Task<List<Utilisateur>> getUtilisateursFromLocalStorage()
		{
			List<Utilisateur> utilisateursFromLocalStorage = null;

			var data = await _localStorage.GetItemAsync<Utilisateur[]>(EmplacementLocalStorage);
			//utilisateursFromLocalStorage = data.ToList();

			if (data == null)
			{
				utilisateursFromLocalStorage = new List<Utilisateur>();
            } 
			else
			{
				utilisateursFromLocalStorage = data.ToList();
			}

			return utilisateursFromLocalStorage;
		}

        public async Task<Utilisateur> getUtilisateurFromPseudo(String pseudo)
		{
			Console.WriteLine("Passage dans le getFromPseudo...");
			List<Utilisateur> utilisateurs = await this.getAllUtilisateurs();
			Utilisateur temp = null;

			foreach (Utilisateur u in utilisateurs)
			{
				if (u.Pseudo == pseudo)
				{
					temp = u;
				}
			}

			if (temp == null)
			{
				Console.WriteLine("Aucun u. par pseudo trouve...");
			} else
			{
				Console.WriteLine("Utilisateur trouvé : " + temp.Pseudo);
			}

			return temp;
		}


        private async Task saveUtilisateursLocalStorage(List<Utilisateur> lUtilisateurs)
		{
			await _localStorage.SetItemAsync(this.EmplacementLocalStorage, lUtilisateurs);
		}

    }
}
