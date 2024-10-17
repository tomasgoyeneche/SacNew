namespace SacNew.Services
{
    public interface INavigationService
    {
        void ShowDialog<TForm>() where TForm : Form;

        void HideForm<TForm>() where TForm : Form;
    }
}