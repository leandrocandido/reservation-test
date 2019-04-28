
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Ryanair.Reservation.Infrastructure.Repositories
{
    /// <summary>
    /// Repository of fligths.
    /// </summary>
    /// <remarks>
    /// Having separated repositories can help to build a cache layer or add some custom behavior.
    /// </remarks>
    public class FlightRepository : RepositoryBase<Domain.Entities.Flight>
    {
        public FlightRepository()
        {
            this.Seed();
        }

        private void Seed()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Ryanair.Reservation.Infrastructure.InitialState.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                var initialList = JsonConvert.DeserializeObject<List<Domain.Entities.Flight>>(json, new JsonSerializerSettings
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                this.collection.AddRange(initialList);
            }
        }
    }
}
