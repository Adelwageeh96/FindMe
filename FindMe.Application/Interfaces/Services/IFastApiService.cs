public interface IFastApiService
{
    Task<string> ValidatePhotoAsync(byte[] photo);
    Task<List<float>> GenerateEmbeddingAsync(byte[] photo);
    Task<List<SimilarityResult>> CalculateSimilaritiesAsync(List<float> embedding, Dictionary<string, List<float>> embeddingsDict);
}

public class SimilarityResult
{
    public string PhotoId { get; set; }
    public float Similarity { get; set; }
}
