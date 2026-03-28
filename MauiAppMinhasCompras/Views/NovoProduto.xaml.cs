using MauiAppMinhasCompras.Models;
using System.Threading.Tasks;
using System.Linq; 

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

            Produto p = new Produto
            {

                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text),
                Categoria = pck_categoria.SelectedItem.ToString()
            };

            await App.Db.insert(p);
            await DisplayAlertAsync("Sucesso!", "Registro Atualizado", "Ok");
            await Navigation.PopAsync();

        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops", ex.Message, "Ok");
        }
    }

     private async void pck_filtro_SelectedIndexChanged(object sender, EventArgs e)
    {
        string categoriaSelecionada = pck_categoria.SelectedItem.ToString();

        List<Produto> listaCompleta = await App.Db.GetAll();

        if (categoriaSelecionada == "Todos")
        {
            ListaProduto.ItemsSource = listaCompleta;
        }
        else
        {
            var listaFiltrada = listaCompleta.Where(p => p.Categoria == categoriaSelecionada).ToList();
            ListaProduto.ItemsSource = listaFiltrada;
        }
    }


    private async void btn_Somar_Clicked(object sender, EventArgs e)
    {
        
        List<Produto> todosProdutos = await App.Db.GetAll();

        if (todosProdutos.Count == 0)
        {
            await DisplayAlertAsync("Aviso", "Sua lista está vazia!", "OK");
            return;
        }

      
        double totalGeral = todosProdutos.Sum(p => p.Preco * p.Quantidade);

       
        var resumoPorCategoria = todosProdutos
            .GroupBy(p => p.Categoria)
            .Select(g => new {
                Nome = g.Key ?? "Sem Categoria",
                Valor = g.Sum(p => p.Preco * p.Quantidade)
            });

       
        string mensagem = $"TOTAL GERAL: {totalGeral:C}\n\n";
        foreach (var item in resumoPorCategoria)
        {
            mensagem += $"{item.Nome}: {item.Valor:C}\n";
        }

        await DisplayAlertAsync("Relatório de Gastos", mensagem, "Fechar");
    }
}

