using Microsoft.Azure.Cosmos;
using mvcCochesCosmos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcCochesCosmos.Services {
    public class CochesCosmosService {
        private CosmosClient cosmosClient;
        private Container cosmosContainer;
        public CochesCosmosService(CosmosClient client, Container container) {
            this.cosmosClient = client;
            this.cosmosContainer = container;
        }
        public async Task CreateDatabaseAsync() {
            ContainerProperties properties = new ContainerProperties("containercoches", "/id");
            await this.cosmosClient.CreateDatabaseAsync("cosmosCoches");
            await this.cosmosClient.GetDatabase("cosmoscoches").CreateContainerAsync(properties);
        }
        public async Task AddVehiculosAsync(Vehiculo car) {

            await this.cosmosContainer.CreateItemAsync<Vehiculo>(car, new PartitionKey(car.Id));
        }
        public async Task<List<Vehiculo>> GetVehiculosAsync() {
            var query = this.cosmosContainer.GetItemQueryIterator<Vehiculo>();
            List<Vehiculo> coches = new List<Vehiculo>();
            while (query.HasMoreResults) {
                var result = await query.ReadNextAsync();
                coches.AddRange(result);
            }
            return coches;
        }
        public async Task UpdateVehiculoAsinc(Vehiculo car) {
            await this.cosmosContainer.UpsertItemAsync<Vehiculo>(car, new PartitionKey(car.Id));
        }
        public async Task<Vehiculo> FindVehiculoAsync(string id) {
            ItemResponse<Vehiculo> response = await this.cosmosContainer.ReadItemAsync<Vehiculo>(id, new PartitionKey(id));
            return response.Resource;
        }
        public async Task DeleteVehiculoAsync(string id) {
            await this.cosmosContainer.DeleteItemAsync<Vehiculo>(id, new PartitionKey(id));
        }
        public async Task<List<Vehiculo>> BuscarVehiculosMarca(string marca) {
            string consulta = "select * from c where c.Marca='" + marca + "'";
            QueryDefinition definition = new QueryDefinition(consulta);
            var query = this.cosmosContainer.GetItemQueryIterator <Vehiculo>(definition); 
            List<Vehiculo> coches = new List<Vehiculo>();
            while (query.HasMoreResults) {
                var result = await query.ReadNextAsync();
                coches.AddRange(result);
            }
            return coches;
        }
        public async Task<List<Vehiculo>> BuscarVehiculosModelo(string modelo) {
            string consulta = "select * from c where c.Modelo='" + modelo+ "'";
            QueryDefinition definition = new QueryDefinition(consulta);
            var query = this.cosmosContainer.GetItemQueryIterator<Vehiculo>(definition);
            List<Vehiculo> coches = new List<Vehiculo>();
            while (query.HasMoreResults) {
                var result = await query.ReadNextAsync();
                coches.AddRange(result);
            }
            return coches;
        }
    }
}
