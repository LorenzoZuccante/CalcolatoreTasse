using System;

public class Contribuente
{
    public string Nome { get; set; }
    public string Cognome { get; set; }
    public DateTime DataNascita { get; set; }
    public string CodiceFiscale { get; set; }
    public char Sesso { get; set; }
    public string ComuneResidenza { get; set; }
    public double RedditoAnnuale { get; set; }

    public Contribuente(string nome, string cognome, DateTime dataNascita,
                        string codiceFiscale, char sesso,
                        string comuneResidenza, double redditoAnnuale)
    {
        Nome = nome;
        Cognome = cognome;
        DataNascita = dataNascita;
        CodiceFiscale = codiceFiscale;
        Sesso = sesso;
        ComuneResidenza = comuneResidenza;
        RedditoAnnuale = redditoAnnuale;
    }

    public double CalcolaImposta()
    {
        if (RedditoAnnuale <= 15000)
        {
            return RedditoAnnuale * 0.23;
        }
        else if (RedditoAnnuale <= 28000)
        {
            return 3450 + (RedditoAnnuale - 15000) * 0.27;
        }
        else if (RedditoAnnuale <= 55000)
        {
            return 6960 + (RedditoAnnuale - 28000) * 0.38;
        }
        else if (RedditoAnnuale <= 75000)
        {
            return 17220 + (RedditoAnnuale - 55000) * 0.41;
        }
        else
        {
            return 25420 + (RedditoAnnuale - 75000) * 0.43;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        string nome = ControllaStringa("Inserisci Nome: ");
        string cognome = ControllaStringa("Inserisci Cognome: ");
        DateTime dataNascita = LeggiData("Inserisci Data di Nascita (formato gg/mm/aaaa): ");
        string codiceFiscale = LeggiCodiceFiscale("Inserisci Codice Fiscale: ");
        char sesso = LeggiSesso("Inserisci Sesso (M/F): ");
        string comuneResidenza = ControllaStringa("Inserisci Comune di Residenza: ");
        double redditoAnnuale = LeggiNumeroPositivo("Inserisci Reddito Annuale: ");

        Contribuente contribuente = new Contribuente(nome, cognome, dataNascita,
                                                     codiceFiscale, sesso,
                                                     comuneResidenza, redditoAnnuale);

        Console.WriteLine("==================================================");
        Console.WriteLine("CALCOLO DELL'IMPOSTA DA VERSARE:");
        Console.WriteLine($"Contribuente: {contribuente.Nome} {contribuente.Cognome},");
        Console.WriteLine($"nato il {contribuente.DataNascita.ToString("dd/MM/yyyy")} ({contribuente.Sesso}),");
        Console.WriteLine($"residente in {contribuente.ComuneResidenza},");
        Console.WriteLine($"codice fiscale: {contribuente.CodiceFiscale}");
        Console.WriteLine($"Reddito dichiarato: euro {contribuente.RedditoAnnuale}");
        Console.WriteLine($"IMPOSTA DA VERSARE: euro {contribuente.CalcolaImposta()}");
    }

    static string ControllaStringa(string messaggio)
    {
        string input;
        do
        {
            Console.Write(messaggio);
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input) || ContieneCifre(input));

        return input;
    }

    static bool ContieneCifre(string str)
    {
        foreach (char c in str)
        {
            if (char.IsDigit(c))
                return true;
        }
        return false;
    }

    static DateTime LeggiData(string messaggio)
    {
        DateTime data;
        string input;
        do
        {
            Console.Write(messaggio);
            input = Console.ReadLine();
        } while (!DateTime.TryParse(input, out data));

        return data;
    }

    static string LeggiCodiceFiscale(string messaggio)
    {
        string input;
        do
        {
            Console.Write(messaggio);
            input = Console.ReadLine();
        } while (input.Length != 16 || !TuttiCaratteriValidi(input));

        return input;
    }

    static bool TuttiCaratteriValidi(string str)
    {
        foreach (var c in str)
        {
            if (!char.IsLetterOrDigit(c))
                return false;
        }
        return true;
    }

    static char LeggiSesso(string messaggio)
    {
        char sesso;
        do
        {
            Console.Write(messaggio);
        } while (!char.TryParse(Console.ReadLine().ToUpper(), out sesso) || (sesso != 'M' && sesso != 'F'));

        return sesso;
    }

    static double LeggiNumeroPositivo(string messaggio)
    {
        double numero;
        string input;
        do
        {
            Console.Write(messaggio);
            input = Console.ReadLine();
        } while (!double.TryParse(input, out numero) || numero < 0);

        return numero;
    }
}
