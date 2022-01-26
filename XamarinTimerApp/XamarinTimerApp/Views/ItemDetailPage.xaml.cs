using System.ComponentModel;
using Xamarin.Forms;
using XamarinTimerApp.ViewModels;

namespace XamarinTimerApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}