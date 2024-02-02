using System;

namespace ProgettoCalcolaImposteC_
{
    internal class Contribuente
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime DataNascita { get; set; }
        public string CodiceFiscale { get; set; }
        public char Sesso { get; set; }
        public string ComuneResidenza { get; set; }
        public decimal RedditoAnnuale { get; set; }

        public Contribuente(string nome, string cognome, DateTime dataNascita, string codiceFiscale, char sesso, string comuneResidenza, decimal redditoAnnuale)
        {
            Nome = nome;
            Cognome = cognome;
            DataNascita = dataNascita;
            CodiceFiscale = VerificaCodiceFiscale(codiceFiscale) ? codiceFiscale : throw new ArgumentException("Il codice fiscale non è valido.");
            Sesso = VerificaSesso(sesso) ? char.ToUpper(sesso) : throw new ArgumentException("Il sesso inserito non è valido.");
            ComuneResidenza = comuneResidenza;
            RedditoAnnuale = VerificaReddito(redditoAnnuale) ? redditoAnnuale : throw new ArgumentException("Il reddito inserito non è valido.");
        }

        public decimal CalcolaImposta()
        {
            decimal imposta = 0;

            if (RedditoAnnuale <= 15000)
            {
                imposta = RedditoAnnuale * 0.23m;
            }
            else if (RedditoAnnuale <= 28000)
            {
                imposta = 3450 + (RedditoAnnuale - 15000) * 0.27m;
            }
            else if (RedditoAnnuale <= 55000)
            {
                imposta = 6960 + (RedditoAnnuale - 28000) * 0.38m;
            }
            else if (RedditoAnnuale <= 75000)
            {
                imposta = 17220 + (RedditoAnnuale - 55000) * 0.41m;
            }
            else
            {
                imposta = 25420 + (RedditoAnnuale - 75000) * 0.43m;
            }

            return imposta;
        }

        private bool VerificaCodiceFiscale(string codiceFiscale)
        {
            return !string.IsNullOrEmpty(codiceFiscale) && codiceFiscale.Length == 16;
        }

        private bool VerificaSesso(char sesso)
        {
            return sesso == 'M' || sesso == 'F';
        }

        private bool VerificaReddito(decimal reddito)
        {
            return reddito >= 0;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                Console.WriteLine("Inserisci i dati del contribuente:");

                Console.Write("Nome: ");
                string nome = Console.ReadLine();

                Console.Write("Cognome: ");
                string cognome = Console.ReadLine();

                Console.Write("Data di nascita (formato YYYY-MM-DD): ");
                DateTime dataNascita;
                while (!DateTime.TryParse(Console.ReadLine(), out dataNascita))
                {
                    Console.WriteLine("Formato data non valido. Riprova:");
                }

                Console.Write("Codice Fiscale: ");
                string codiceFiscale = Console.ReadLine();

                Console.Write("Sesso (M/F): ");
                char sesso;
                while (!char.TryParse(Console.ReadLine().ToUpper(), out sesso) || (sesso != 'M' && sesso != 'F'))
                {
                    Console.WriteLine("Valore non valido. Inserisci M o F:");
                }

                Console.Write("Comune di residenza: ");
                string comuneResidenza = Console.ReadLine();

                Console.Write("Reddito Annuale: ");
                decimal redditoAnnuale;
                while (!decimal.TryParse(Console.ReadLine(), out redditoAnnuale) || redditoAnnuale < 0)
                {
                    Console.WriteLine("Valore non valido. Inserisci un numero positivo:");
                }

                Contribuente contribuente = new Contribuente(nome, cognome, dataNascita, codiceFiscale, sesso, comuneResidenza, redditoAnnuale);

                decimal impostaDaVersare = contribuente.CalcolaImposta();

                Console.WriteLine("\nCALCOLO DELL'IMPOSTA DA VERSARE:");
                Console.WriteLine($"Contribuente: {contribuente.Nome} {contribuente.Cognome},");
                Console.WriteLine($"nato il {contribuente.DataNascita.ToString("dd/MM/yyyy")} ({contribuente.Sesso}),");
                Console.WriteLine($"residente in {contribuente.ComuneResidenza},");
                Console.WriteLine($"codice fiscale: {contribuente.CodiceFiscale}");
                Console.WriteLine($"Reddito dichiarato: € {contribuente.RedditoAnnuale:N2}");
                Console.WriteLine($"IMPOSTA DA VERSARE: € {impostaDaVersare:N2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
            }
        }
    }
}
