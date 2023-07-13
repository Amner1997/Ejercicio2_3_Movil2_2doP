using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ejercicio2_3_Movil2_2doP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListaFotos : ContentPage
    {
        public PageListaFotos()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            listaFotos.ItemsSource = await App.Instancia.GetAll();
        }
    }
}