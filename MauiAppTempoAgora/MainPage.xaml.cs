using System.ComponentModel.Design;
using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {   //  Verifica conexão com a internet
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    await DisplayAlert("Sem conexão", "Verifique sua internet e tente novamente.", "OK");
                    return; // interrompe o método
                }

                //  Verifica se o campo cidade foi preenchido
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do Sol: {t.sunrise} \n" +
                                         $"Por do Sol: {t.sunset} \n" +
                                         $"Temp Máx: {t.temp_max} \n" +
                                         $"Temp Min: {t.temp_min} \n" +
                                         $"Descrição: {t.description} \n" +
                                         $"Velocidade Vento: {t.speed} \n" +
                                         $"Visibilidade: {t.visibility}";

                        lbl_res.Text = dados_previsao;

                    }
                    else
                    {
                        await DisplayAlert("Cidade não encontrada", $"Não localizamos a cidade '{txt_cidade.Text}'.", "OK");
                        lbl_res.Text = ""; //Quando a cidade não for encontrada 
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }

}
