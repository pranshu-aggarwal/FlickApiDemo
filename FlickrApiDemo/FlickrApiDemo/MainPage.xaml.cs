using System.Linq;
using FlickrApiDemo.Core.ViewModels;
using Xamarin.Forms;

namespace FlickrApiDemo
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel _viewModel;
        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            BindingContext = _viewModel;
        }

        private void ListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (e.Item == _viewModel.Photos.LastOrDefault() && _viewModel.LoadMoreCommand.CanExecute(null))
                _viewModel.LoadMoreCommand.Execute(null);
        }
    }
}
