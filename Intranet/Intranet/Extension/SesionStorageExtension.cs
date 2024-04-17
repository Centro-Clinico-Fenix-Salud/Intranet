using Blazored.SessionStorage;
using System.Text.Json;
using System.Text;
using Intranet.Shared;

namespace Intranet.Extension
{
    public static class SesionStorageExtension
    {
        private static bool valor ;
        public static async Task GuardarStorage<T>(
            this ISessionStorageService sessionStorageService,
            string key, T item
            ) where T : class
        {

            var itemJson = JsonSerializer.Serialize(item);
            await sessionStorageService.SetItemAsStringAsync(key, itemJson);

        }

        public static async Task<T?> ObtenerStorage<T>(
        this ISessionStorageService sessionStorageService,
        string key
        ) where T : class
        {
            var itemJson = await sessionStorageService.GetItemAsStringAsync(key);

            if (itemJson != null)
            {
                var item = JsonSerializer.Deserialize<T>(itemJson);
                return item;
            }
            else
                return null;
        }

        public static async Task<bool> ObtenerLogin(this ISessionStorageService sessionStorageService) {

            return valor;
        }

        public static async Task GuardarLogin(this ISessionStorageService sessionStorageService, bool data)
        {

            valor = data;
        }
    }
}
