using AutoMapper;
using HotelApp.Api.DTO;
using HotelApp.Api.Entities;
using HotelApp.Api.Exceptions;
using HotelApp.Api.Filter;
using System.Net.Http;

namespace HotelApp.Api.Services
{
    public class ExternalApiClient : IExternalApiClient
    {
        private readonly ILogger _logger;
        private readonly HttpClient _client;
        public ExternalApiClient(ILogger<ExternalApiClient> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _client = clientFactory.CreateClient("ExternalApi");
        }
        public async Task<string> AddReservation(ExternalApiDto externalDto)
        {
            _logger.LogInformation("Adding reservation to external api.");
            var response = await _client.PostAsJsonAsync("/Puzjak/reservation-rest/reservation", externalDto);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Unable to add reservation to external api.");
                throw new HttpRequestException(response.ReasonPhrase);
            }
            return response.ReasonPhrase.ToString();
        }

        public async Task<string> DeleteReservation(int id)
        {
            _logger.LogInformation($"Deleting reservation from external api with id {id}");
            var url = string.Format("/Puzjak/reservation-rest/reservation/{0}", id);
            var response = await _client.DeleteAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Unable to delete reservation from external api.");
                throw new HttpRequestException(response.ReasonPhrase);
            }
            if ((int)response.StatusCode == StatusCodes.Status404NotFound)
            {
                _logger.LogError($"Reservation with id {id} was not found on external api.");
                throw new RecordNotFoundException($"Record with id {id} does not exist.");
            }
            return response.ReasonPhrase.ToString();
        }

        public async Task<ExternalApiDto> GetReservationById(int id)
        {
            _logger.LogInformation("Listing reservation from external api with id {id}", id);
            var url = string.Format("/Puzjak/reservation-rest/reservation/{0}", id);
            var response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Unable to list reservation from external api with id {id}.", id);
                throw new HttpRequestException(response.ReasonPhrase);
            }
            if ((int)response.StatusCode == StatusCodes.Status404NotFound)
            {
                _logger.LogError("Reservation with id {id} was not found on external api.", id);
                throw new RecordNotFoundException($"Record with id {id} does not exist.");
            }
            return await response.Content.ReadAsAsync<ExternalApiDto>();
        }

        public async Task<List<ExternalApiDto>> GetReservations()
        {
            _logger.LogInformation("Listing all reservation from external.");
            var response = await _client.GetAsync("/Puzjak/reservation-rest/reservation");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Unable to list reservations from external api.");
                throw new HttpRequestException(response.ReasonPhrase);
            }
            List<ExternalApiDto> products = await response.Content.ReadAsAsync<List<ExternalApiDto>>();
            
            return products;
        }

        public async Task<string> UpdateReservation(ExternalApiDto externalDto, int id)
        {
            _logger.LogInformation("Updating reservation from external api with id {id}", id);
            var url = string.Format("/Puzjak/reservation-rest/reservation/{0}", id);
            var response = await _client.PutAsJsonAsync(url, externalDto);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Unable to update reservation from external api with id {id}.", id);
                throw new HttpRequestException(response.ReasonPhrase);
            }
            return response.ReasonPhrase.ToString();
        }
    }
}
