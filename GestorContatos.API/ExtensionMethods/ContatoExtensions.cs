using GestorContatos.API.ViewModel;
using GestorContatos.Core.Entities;

namespace GestorContatos.API.ExtensionMethods;

public static class ContatoExtensions
{
    public static ContatoModel ToModel(this ContatoViewModel viewModel)
    {
        return new ContatoModel
        {
            Id = viewModel.Id,
            Email = viewModel.Email,
            Nome = viewModel.Nome,
            Telefone = viewModel.Telefone,
            Regiao = new RegiaoModel()
            {
                Id = viewModel.Regiao.Id,
                DDD = viewModel.Regiao.DDD,
                Nome = viewModel.Regiao.Nome
            }
        };
    }

    public static ContatoViewModel ToViewModel(this ContatoModel model)
    {
        return new ContatoViewModel
        {
            Id = model.Id,
            Email = model.Email,
            Nome = model.Nome,
            Telefone = model.Telefone,
            Regiao = new RegiaoViewModel()
            {
                Id = model.Regiao.Id,
                DDD = model.Regiao.DDD,
                Nome = model.Regiao.Nome
            }
        };
    }

    public static IEnumerable<ContatoViewModel> ToViewModel(this IEnumerable<ContatoModel> model)
        => model.Select(model => model.ToViewModel());

}
