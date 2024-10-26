using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace rmp8l
{
    public partial class MainPage : ContentPage
    {
      

        public MainPage()
        {
            InitializeComponent();
        }

        private void dateBirth_DateSelected(object sender, DateChangedEventArgs e)
        {
            //Расчитываем возраст
            int ag = DateTime.Now.Year - dateBirth.Date.Year; if (DateTime.Now.Month < dateBirth.Date.Month ||
            (DateTime.Now.Month == dateBirth.Date.Month && DateTime.Now.Day < dateBirth.Date.Day))
                ag--;
            age.Text = "Возраст - "
            + ag.ToString();
        }

       private void addPhone_Clicked(object sender, EventArgs e)
       {
           //Добавляем телефоны в текущую секцию
           EntryCell p = new EntryCell();
           p.Placeholder = "Введите телефон";
           p.Keyboard=Keyboard.Numeric;
           phone.Add(p);
       }

        private async void ph_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Запрос на выбор изображения
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Выберите фото"
                });

                // Получаем поток изображения
                if (result != null)
                {
                    var stream = await result.OpenReadAsync();

                    // Устанавливаем выбранное изображение в элемент Image
                    selectedImage.Source = ImageSource.FromStream(() => stream);
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок, таких как отсутствие доступа к медиафайлам
                await DisplayAlert("Ошибка", "Не удалось выбрать фото: " + ex.Message, "ОК");
            }
        }
    }
    

}
