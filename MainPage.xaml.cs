using Microsoft.Maui;
using System.Reflection;
namespace rmp8l
{
    public partial class MainPage : ContentPage
    {
        private List<Student> students = new List<Student>();
        private StreamImageSource userPhotoSource; // Для хранения изображения

        public MainPage()
        {
            InitializeComponent();
        }


        private async void ph_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync();
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                userPhotoSource = (StreamImageSource)ImageSource.FromStream(() =>
                {
                    // Возвращаем новый поток каждый раз
                    return result.OpenReadAsync().Result;
                });
                UserPhoto.Source = userPhotoSource; // Отображаем фото на главной странице
            }
        }
            private void Button_Clicked(object sender, EventArgs e)
            {

                var student = new Student
                {
                    FullName = FullNameEntry.Text,
                    Gender = GenderPicker.SelectedItem?.ToString(),
                    Age = AgeEntry.Text,
                    NeedsHostel = HostelSwitch.IsToggled,
                    IsLeader = LeaderSwitch.IsToggled,
                    Photo = userPhotoSource // сохраняем фото
                };

                // Добавляем студент в список
                students.Add(student);
                RecordsPicker.ItemsSource = null; // Сбрасываем источник данных
                RecordsPicker.ItemsSource = students.Select(s => s.FullName).ToList(); // Устанавливаем обновленный источник данных

                StatusLabel.Text = "Данные сохранены!";

                // Очищаем все поля
                FullNameEntry.Text = string.Empty;
                GenderPicker.SelectedIndex = -1; // Сбрасываем выбор
                AgeEntry.Text = string.Empty;
                HostelSwitch.IsToggled = false; // Сбрасываем переключатель
                LeaderSwitch.IsToggled = false; // Сбрасываем переключатель
                UserPhoto.Source = null; // Убираем фото
            }
        private async void RecordsPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = RecordsPicker.SelectedItem?.ToString();
            if (selectedItem != null)
            {
                var student = students.FirstOrDefault(s => s.FullName == selectedItem);
                if (student != null)
                {
                    // Передаем данные на DetailsPage
                    await Navigation.PushAsync(new NewPage1(
                        student.FullName,
                        student.Gender,
                        student.Age,
                        student.NeedsHostel,
                        student.IsLeader,
                        student.Photo
                    ));
                }
            }
        }
    }
}
