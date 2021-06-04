namespace Core.IViews
{
    public interface ITravelDetailView
    {
        void ShowShortToastMessage(string message);
        void ShowImportDataFromExcelPopUp();
        void ShowDeleteAllDataWarningPopupMessage();
        void GetExternalPermissions();
        string GetFilePath(string fileName);
        void ReloadActivity();
        void NotifyListViewDataChanged();
    }
}
