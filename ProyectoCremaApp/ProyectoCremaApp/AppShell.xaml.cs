using ProyectoCremaApp.Views;
using Xamarin.Forms;

namespace ProyectoCremaApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NoteEntryPage), typeof(NoteEntryPage));
        }
    }
}