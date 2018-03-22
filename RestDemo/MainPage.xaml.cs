using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RestDemoN
{
    public class Post : INotifyPropertyChanged
    {
        public int userId { get; set; }
        public int id { get; set; }
        private string _title;
        [JsonProperty("title")]
        public string Title {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public string body { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged( string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class MainPage : ContentPage
    {
        private const string url = "https://jsonplaceholder.typicode.com/posts";
        private HttpClient _Client = new HttpClient();
        private ObservableCollection<Post> _post;

        public MainPage()
        { 
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var content = await _Client.GetStringAsync(url);
            List<Post> post = JsonConvert.DeserializeObject<List<Post>>(content);
            _post = new ObservableCollection<Post>(post);
            Post_List.ItemsSource = _post;
            
            base.OnAppearing();
        }

        private async void Button_Add(object sender, EventArgs e)
        {
            Post post = new Post { Title = $"Title : TimeStamp{DateTime.UtcNow.TimeOfDay}" };
            string content = JsonConvert.SerializeObject(post);
            _post.Insert(0, post);
            await _Client.PostAsync(url, new StringContent(content));
        }

        private async void Button_Update(object sender, EventArgs e)
        {
            Post postt = _post[0];
            postt.Title += "[updated]";
            string content = JsonConvert.SerializeObject(postt);
            await _Client.PutAsync(url + "/" + postt.id,new StringContent(content));
        }
        private async void Button_Delete(object sender, EventArgs e)
        {
            Post post = _post[0];
            string content = JsonConvert.SerializeObject(post);
            await _Client.DeleteAsync(url + "/" + post.id);
            _post.Remove(post);
        }
    }
}
