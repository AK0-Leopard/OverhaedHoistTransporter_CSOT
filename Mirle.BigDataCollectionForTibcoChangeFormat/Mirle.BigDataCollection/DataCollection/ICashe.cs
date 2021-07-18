namespace Mirle.BigDataCollection.DataCollection
{
    public abstract class ICashe
    {
        protected string ConvertStringEmpty(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "0" : value;
        }
    }
}