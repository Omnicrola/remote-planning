namespace RemotePlanning.Storycards
{
    public interface IStorcardViewModel
    {
        string Role { get; set; }
        string Title { get; set; }
        string Content { get; set; }
        int Estimate { get; set; }
    }
}