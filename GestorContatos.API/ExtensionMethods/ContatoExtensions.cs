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
            Telefone = viewModel.Telefone
        };
    }

    public static ContatoViewModel ToViewModel(this ContatoModel model)
    {
        return new ContatoViewModel
        {
            Id = model.Id,
            Email = model.Email,
            Nome = model.Nome,
            Telefone = model.Telefone
        };
    }

    public static IEnumerable<ContatoViewModel> ToViewModel(this IEnumerable<ContatoModel> model)
        => model.Select(model => model.ToViewModel());

}
