using System.Net.Http.Json;

// Interroge l'API geo.api.gouv.fr pour trouver les communes d'un code postal.
public static class ApiGeo
{
    // un seul HttpClient pour tout le programme, avec l'adresse de base de l'API
    private static readonly HttpClient Client = new HttpClient { BaseAddress = new Uri("https://geo.api.gouv.fr/") };

    // Renvoie les communes du code postal, ou une liste vide si aucune ne correspond.
    public static async Task<List<Commune>> CommunesAsync(string codePostal)
    {
        // GetFromJsonAsync envoie la requete et deserialise directement le JSON en List<Commune>
        List<Commune>? communes = await Client.GetFromJsonAsync<List<Commune>>($"communes?codePostal={codePostal}");
        // l'API renvoie [] pour un code inconnu (99999), mais on se protege aussi contre null
        return communes ?? new List<Commune>();
    }
}
