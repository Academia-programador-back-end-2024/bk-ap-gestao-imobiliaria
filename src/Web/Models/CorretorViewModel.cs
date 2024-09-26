using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCorretor;
using System.ComponentModel.DataAnnotations;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web.Models;

public class CorretorViewModel
{
    public int CorretorId { get; set; }

    public string Nome { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public string Creci { get; set; } = null!;

    public string? Telefone { get; set; }

    public string? Email { get; set; }
}

public class CriarCorretorViewModel
{
    [Required]
    public string Nome { get; set; } = null!;

    [Required]
    [MinLength(11, ErrorMessage = "cpf Minimo 11")]
    [MaxLength(11, ErrorMessage = "cpf Máximo 11")]
    public string Cpf { get; set; } = null!;

    [Required]
    public int Creci { get; set; }

    public string? Telefone { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Email invalido")]
    public string? Email { get; set; }
}


public class EditarCorretorViewModel
{
    [Required]
    public int CorretorId { get; set; }

    [Required]
    public string Nome { get; set; } = null!;

    [Required]
    [MinLength(11, ErrorMessage = "cpf Minimo 11")]
    [MaxLength(11, ErrorMessage = "cpf Máximo 11")]
    public string Cpf { get; set; } = null!;

    [Required]
    public int Creci { get; set; }

    public string? Telefone { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Email invalido")]
    public string? Email { get; set; }
}

public static class CorretorViewModelExtension
{
    public static int SomaDaHora(this DateTime dateTime)
    {
        return dateTime.Hour + dateTime.Minute + dateTime.Second;
    }

    public static CorretorViewModel ToCorretorViewModel(this Corretor corretor)
    {
        //Mapeamento
        return new CorretorViewModel()
        {
            Cpf = corretor.Cpf,
            Email = corretor.Email,
            Telefone = corretor.Telefone,
            Nome = corretor.Nome,
            CorretorId = corretor.CorretorId,
            Creci = corretor.Creci
        };
    }

    public static EditarCorretorViewModel ToEditarCorretorViewModel(this Corretor corretor)
    {
        //Mapeamento
        return new EditarCorretorViewModel()
        {
            Cpf = corretor.Cpf,
            Email = corretor.Email,
            Telefone = corretor.Telefone,
            Nome = corretor.Nome,
            CorretorId = corretor.CorretorId,
            Creci = int.Parse(corretor.Creci)
        };
    }


    public static Corretor ToCorretor(this CorretorViewModel corretorViewModel)
    {
        //Mapeamento
        return new Corretor()
        {
            Cpf = corretorViewModel.Cpf,
            Email = corretorViewModel.Email,
            Telefone = corretorViewModel.Telefone,
            Nome = corretorViewModel.Nome,
            CorretorId = corretorViewModel.CorretorId,
            Creci = corretorViewModel.Creci
        };
    }

    public static Corretor ToCorretor(this CriarCorretorViewModel corretorViewModel)
    {
        //Mapeamento
        return new Corretor()
        {
            Cpf = corretorViewModel.Cpf,
            Email = corretorViewModel.Email,
            Telefone = corretorViewModel.Telefone,
            Nome = corretorViewModel.Nome,
            Creci = corretorViewModel.Creci.ToString()
        };
    }

    public static Corretor ToCorretor(this EditarCorretorViewModel corretorViewModel)
    {
        //Mapeamento
        return new Corretor()
        {
            CorretorId = corretorViewModel.CorretorId,
            Cpf = corretorViewModel.Cpf,
            Email = corretorViewModel.Email,
            Telefone = corretorViewModel.Telefone,
            Nome = corretorViewModel.Nome,
            Creci = corretorViewModel.Creci.ToString()
        };
    }

    public static List<CorretorViewModel> ToCorretoresViewModel(this List<Corretor> corretores)
    {
        var corretoresViewModel = new List<CorretorViewModel>();
        corretores.ForEach(corretor => corretoresViewModel.Add(corretor.ToCorretorViewModel()));
        return corretoresViewModel;
    }
}

