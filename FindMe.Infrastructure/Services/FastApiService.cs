using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

public class FastApiService : IFastApiService
{
    private readonly HttpClient _httpClient;

    public FastApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> ValidatePhotoAsync(byte[] photo)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new ByteArrayContent(photo), "file", "photo.jpg");

        var response = await _httpClient.PostAsync("https://findme-vqk2.onrender.com/validate-photo/", content);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ValidationResult>();
        return result.Data.Result;
    }

    public async Task<List<float>> GenerateEmbeddingAsync(byte[] photo)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new ByteArrayContent(photo), "file", "photo.jpg");

        var response = await _httpClient.PostAsync("https://findme-vqk2.onrender.com/generate-embedding/", content);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<EmbeddingResult>();
        return result.Data.Embedding;
    }

    public async Task<List<SimilarityResult>> CalculateSimilaritiesAsync(List<float> embedding, Dictionary<string, List<float>> embeddingsDict)
    {
        var request = new EmbeddingRequest
        {
            embedding = embedding,
            embeddings_dict = embeddingsDict
        };

        // Log the request payload for debugging
        var requestJson = JsonSerializer.Serialize(request);
        Console.WriteLine("Request JSON: " + requestJson);

        var response = await _httpClient.PostAsJsonAsync("https://findme-vqk2.onrender.com/calculate-similarities/", request);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Request failed with status code {response.StatusCode} and message: {errorContent}");
        }

        var result = await response.Content.ReadFromJsonAsync<SimilarityResponse>();

        var similarityResults = result.Data.top_similarities.Select(similarity => new SimilarityResult
        {
            PhotoId = similarity[0].GetString(),
            Similarity = similarity[1].GetSingle()
        }).ToList();

        return similarityResults;
    }


    public class ValidationResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public ValidationData Data { get; set; }
    }

    public class ValidationData
    {
        public string Result { get; set; }
    }

    public class EmbeddingResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public EmbeddingData Data { get; set; }
    }

    public class EmbeddingData
    {
        public List<float> Embedding { get; set; }
    }

    public class EmbeddingRequest
    {
        public List<float> embedding { get; set; }
        public Dictionary<string, List<float>> embeddings_dict { get; set; }
    }

    public class SimilarityResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public SimilarityData Data { get; set; }
    }

    public class SimilarityData
    {
        public List<List<JsonElement>> top_similarities { get; set; }
    }



}
