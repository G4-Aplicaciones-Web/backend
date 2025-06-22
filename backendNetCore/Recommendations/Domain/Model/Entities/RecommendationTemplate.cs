using backendNetCore.Recommendations.Domain.Model.Aggregates;

namespace backendNetCore.Recommendations.Domain.Model.Entities;

public class RecommendationTemplate
{
    public int Id { get; private set; }

    public string Title { get; private set; }

    public string Content { get; private set; }

    // Navegación inversa (opcional si necesitas acceder desde Template → Recommendations)
    public ICollection<Recommendation> Recommendations { get; private set; }

    // Constructor público para EF Core
    public RecommendationTemplate()
    {
        Title = string.Empty;
        Content = string.Empty;
        Recommendations = new List<Recommendation>();
    }

    // Constructor de dominio
    public RecommendationTemplate(string title, string content)
    {
        Title = title;
        Content = content;
        Recommendations = new List<Recommendation>();
    }

    public void Update(string newTitle, string newContent)
    {
        Title = newTitle;
        Content = newContent;
    }
}