namespace Core.IViews
{
    public interface ITravelDetailView
    {
        void ShowErrorMessage(string message);
        void GetExternalPermissions();
        string GetFilePath(string fileName);
    }
}
