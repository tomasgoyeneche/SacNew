namespace SacNew.Services
{
    public interface INavigationService
    {
        void ShowDialog<TForm>() where TForm : Form;
        void Show<TForm>() where TForm : Form;
        void HideForm<TForm>() where TForm : Form;
        void CloseForm<TForm>() where TForm : Form;
        bool IsFormOpen<TForm>() where TForm : Form;
    }
}