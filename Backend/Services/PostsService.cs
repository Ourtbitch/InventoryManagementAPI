using Backend.DTOs;
using System.Text.Json;

namespace Backend.Services
{
    public class PostsService : IPostsService
    {
        private HttpClient _httpClient;

        public PostsService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PostDto>> Get()
        {
            var result = await _httpClient.GetAsync(_httpClient.BaseAddress);
            var body = await result.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions 
            {
                PropertyNameCaseInsensitive = true
            };

            var post = JsonSerializer.Deserialize<IEnumerable<PostDto>>(body, options);

            return post;
        }
    }
}
