namespace Core.IViews
{
    public interface ITravelDetailView
    {
        void ShowShortToastMessage(string message);
        void ShowDeleteAllDataWarningPopupMessage();
        void GetExternalPermissions();
        string GetFilePath(string fileName);
        void ReloadActivity();
    }
}
