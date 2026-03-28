using MauiAppMinhasCompras.Models;
using MauiAppMinhasCompras.Views;

namespace MauiAppMinhasCompras
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }




        private void ExibirRelatorioPorCategoria()
        {
            var produtos = (List<Produto>)ListaProduto.ItemsSource;

            if (produtos == null || !produtos.Any()) return;

            var resumo = produtos
                .GroupBy(p => p.Categoria)
                .Select(grupo => new {
                    Nome = grupo.Key ?? "Sem Categoria",
                    Total = grupo.Sum(p => p.Preco * p.Quantidade)
                }).ToList();

            string textoRelatorio = "Gastos por Categoria:\n\n";
            foreach (var item in resumo)
            {
                textoRelatorio += $"{item.Nome}: {item.Total:C}\n";
            }

           
            DisplayAlert("Relatório", textoRelatorio, "Fechar");
        }
    }
}
