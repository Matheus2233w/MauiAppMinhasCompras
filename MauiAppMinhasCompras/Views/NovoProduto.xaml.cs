using MauiAppMinhasCompras.Models;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try 
		{
			Produto produto_anexado = BindingContext as Produto;

			Produto p = new Produto
			{
				Id = produto_anexado.Id,
				Descricao = txt_descricao.Text,
				Quantidade = Convert.ToDouble(txt_quantidade.Text),
				Preco = Convert.ToDouble(txt_preco.Text)

			};

			await App.Db.insert(p);
			await DisplayAlertAsync("Sucesso!", "Registro Atualizado", "Ok");
			await Navigation.PopAsync();

		} catch (Exception ex)
		{
			await DisplayAlertAsync("Ops", ex.Message, "Ok");
		}
    }
}