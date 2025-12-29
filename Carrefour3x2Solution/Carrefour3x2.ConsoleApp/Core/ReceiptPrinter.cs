namespace Carrefour3x2.ConsoleApp;

using Carrefour3x2.Core;

public static class ReceiptPrinter
{
    public static void Print(SettlementResult result)
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("          CARREFOUR");
        Console.WriteLine("----------------------------------------");

        foreach (var line in result.Lines)
        {
            PrintLine(line.Description, line.Amount);
        }

        Console.WriteLine("----------------------------------------");
        PrintLine("SUBTOTAL", result.OriginalTotal);
        PrintLine("DESCUENTO", -result.Discount);
        PrintLine("TOTAL A PAGAR", result.PayTotal);
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("¡Gracias por su compra!");
    }

    private static void PrintLine(string text, decimal amount)
    {
        Console.WriteLine($"{text,-25}{amount,10:0.00} €");
    }
}
