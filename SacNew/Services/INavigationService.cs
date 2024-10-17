using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Services
{
    public interface INavigationService
    {
        void ShowDialog<TForm>() where TForm : Form;
        void HideForm<TForm>() where TForm : Form;
    }
}
