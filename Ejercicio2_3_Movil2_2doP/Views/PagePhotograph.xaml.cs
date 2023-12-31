﻿using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ejercicio2_3_Movil2_2doP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePhotograph : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile photo = null;
        private string originalImagePath;
        public PagePhotograph()
        {
            InitializeComponent();
            originalImagePath = "Photo.png"; // Establece la ruta de la imagen original aquí
            imgFoto.Source = originalImagePath; // Asigna la imagen original al cargar la página
        }

        public String Getimage64()
        {
            if (photo != null)
            {
                using(MemoryStream memory = new MemoryStream())
                {
                    Stream stream = photo.GetStream();
                    stream.CopyTo(memory);
                    byte[] fotobyte = memory.ToArray();

                    String Base64 = Convert.ToBase64String(fotobyte);

                    return Base64;
                }
            }

            return null;
        }

        public byte[] GetImageBytes()
        {
            if (photo != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = photo.GetStream();
                    stream.CopyTo(memory);
                    byte[] fotobyte = memory.ToArray();
                    return fotobyte;
                }
            }
            return null;
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(descripcion.Text) || imgFoto.Source?.ToString() == originalImagePath)
            {
                await DisplayAlert("Aviso", "Por favor, complete todos los campos", "Ok");
                return;
            }

            var photograp = new Models.Photograph
            {
                Image = GetImageBytes(),
                Description = descripcion.Text
            };

            if (await App.Instancia.AddPhoto(photograp) > 0)
            {
                await DisplayAlert("Aviso", "Foto Agregada Correctamente", "Ok");
                LimpiarCampos();
                imgFoto.Source = originalImagePath; // Restablece la imagen original
            }
            else
            {
                await DisplayAlert("Aviso", "No se ha podido guardar", "Ok");
            }
        }


        private void LimpiarCampos()
        {
            descripcion.Text = string.Empty;
            imgFoto.Source = null;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "MYAPP",
                Name = "Foto.jpg",
                SaveToAlbum = true
            });

            if(photo != null)
            {
                imgFoto.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.PageListaFotos());
        }
    }
}