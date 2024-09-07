using System.Text.Json.Serialization;

namespace App.Web.Models
{
    public class RoleAsClaim
    {
        [JsonPropertyName("roles")] public List<string> Roles { get; set; }
    }
}