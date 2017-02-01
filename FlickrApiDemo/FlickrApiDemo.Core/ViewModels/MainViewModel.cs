using System.Collections.ObjectModel;
using System.Windows.Input;
using FlickrApiDemo.Core.Models;
using FlickrApiDemo.Core.WebServices;
using Xamarin.Forms;

namespace FlickrApiDemo.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private FlickrApi _flickrApi;
        private string _searchTerm = "";
        private int _currentPage = 1;
        private bool _isBusy;
        private const int PageSize = 20;

        public ICommand SearchCommand { get; private set; }
        public ICommand LoadMoreCommand { get; private set; }
        public ObservableCollection<Photo> Photos { get; set; }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetValue(ref _isBusy, value);
            }
        }

        public MainViewModel()
        {
            _flickrApi = new FlickrApi();

            SearchCommand = new Command<string>(Search);
            LoadMoreCommand = new Command(LoadMore);
            Photos = new ObservableCollection<Photo>();
        }

        private async void Search(string searchTerm)
        {
            IsBusy = true;
            _searchTerm = searchTerm;

            _currentPage = 1;
            Photos.Clear();

            var results = await _flickrApi.SearchPhotos(_searchTerm, _currentPage, PageSize);

            foreach (var photo in results)
            {
                Photos.Add(photo);
            }
            IsBusy = false;
        }

        private async void LoadMore()
        {
            IsBusy = true;
            _currentPage++;

            var results = await _flickrApi.SearchPhotos(_searchTerm, _currentPage, PageSize);

            foreach (var photo in results)
            {
                Photos.Add(photo);
            }
            IsBusy = false;
        }
    }
}
